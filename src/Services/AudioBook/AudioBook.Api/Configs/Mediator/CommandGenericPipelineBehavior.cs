using AudioBook.Api.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Configs.Mediator
{
    public class CommandGenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
    {
        private readonly ClaimsPrincipal _user;

        public CommandGenericPipelineBehavior(ClaimsPrincipal user)
        {
            _user = user;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Handling request
            request.RequestAt = DateTime.UtcNow;
            request.RequestBy = _user.Identity.Name ?? "System";

            var response = await next();
            // Handling response

            return response;
        }
    }
}
