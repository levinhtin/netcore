using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AudioBook.API.Providers
{
    public static class HostingEnvironmentExtensions
    {
        public const string TestEnvironment = "Test";

        public static bool IsTest(this IHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(TestEnvironment);
        }
    }
}
