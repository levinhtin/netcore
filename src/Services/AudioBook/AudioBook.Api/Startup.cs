using System;
using System.Security.Claims;
using System.Text.Json;
using AudioBook.Api.Configs.Mediator;
using AudioBook.Api.Configs.Swagger;
using AudioBook.Api.Providers;
using AudioBook.API;
using AudioBook.API.Providers;
using AudioBook.Core.Constants;
using AudioBook.Infrastructure;
using AudioBook.Infrastructure.Repositories;
using AudioBook.Infrastructure.Repositories.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AudioBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            MigrationProvider.Register(Configuration.GetSection("ConnectionStrings:DbContext").Value);
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddOptions();

            // Add Cors service.
            services.AddCors(options =>
            {
                options
                    .AddPolicy("CorsPolicy", p => p
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    );
            });

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.AddRouting(options => options.LowercaseUrls = true);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<AppConnections>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<IConfiguration>(Configuration);

            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);
            services.AddSingleton<AppSettings>(appSettings);

            var appConnections = new AppConnections();
            Configuration.Bind("ConnectionStrings", appConnections);
            services.AddSingleton<AppConnections>(appConnections);

            var authenticationCfg = new AuthenticationCfg();
            Configuration.Bind("Authentication", authenticationCfg);
            services.AddSingleton<AuthenticationCfg>(authenticationCfg);

            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(x =>
            {
                IHttpContextAccessor currentContext = x.GetService<IHttpContextAccessor>();
                return currentContext.HttpContext.User;
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAudioBookRepository, AudioBookRepository>();
            services.AddScoped<IAudioBookTrackRepository, AudioBookTrackRepository>();
            services.AddScoped<IBookReaderRepository, BookReaderRepository>();

            // Mediator
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommonPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            System.Reflection.Assembly assembly = AppDomain.CurrentDomain.Load("AudioBook.Api");
            services.AddMediatR(assembly);

            services
                .AddControllers(options =>
                {
                    options.RespectBrowserAcceptHeader = true; // false by default
                })
                .AddJsonOptions(options =>
                {
                    // Use the default property (Pascal) casing.
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                    //options.JsonSerializerOptions.IgnoreNullValues = true;
                    // Configure a custom converter.
                    //options.SerializerOptions.Converters.Add(new MyCustomJsonConverter());
                })
                //.AddJsonFormatters()
                //.AddJsonOptions(option =>
                //{
                //    option.SerializerSettings.ContractResolver = new DefaultContractResolver
                //    {
                //        NamingStrategy = new SnakeCaseNamingStrategy()
                //    };
                //})
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(option => 
                { 
                    option.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    option.RegisterValidatorsFromAssembly(assembly);
                });

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
                options.ClientErrorMapping[404].Link =
                    "https://httpstatuses.com/404";
            });

            services
                .AddApiVersioning(options =>
                {
                    options.ApiVersionReader = new MediaTypeApiVersionReader();
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                    options.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            ////.AddJwtBearer(o =>
            ////{
            ////    o.Authority = "http://localhost:5000";
            ////    o.Audience = "audiobooks";
            ////    o.RequireHttpsMetadata = false;
            ////});
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = authenticationCfg.Authority;
            //    options.ApiName = authenticationCfg.Audience;
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //});

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment() || /*Customize*/ env.IsTest())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseStatusCodePages();
                app.UseAppExceptionHandler();
            }
            else
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(
                options =>
                {
                    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }

                    //options.OAuthClientId("audiobookapp.ro.client");
                    //options.OAuthClientSecret("audiobookapp.secret");
                });


            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseWelcomePage(new WelcomePageOptions() { Path = "/" });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
