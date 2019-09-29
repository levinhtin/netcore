using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AudioBook.Api.Application;
using MediatR;

namespace AudioBook.Api.Configs.Mediator
{
    public class CommonPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ClaimsPrincipal _user;

        public CommonPipelineBehavior(ClaimsPrincipal user)
        {
            this._user = user;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Handling request
            if (request is RequestAudit)
            {
                var requestAudit = request as RequestAudit;
                requestAudit.RequestAt = DateTime.UtcNow;
                requestAudit.RequestBy = this._user.Identity.Name ?? "System";
            }

            var response = await next();
            // Handling response

            return response;
        }
    }
}
