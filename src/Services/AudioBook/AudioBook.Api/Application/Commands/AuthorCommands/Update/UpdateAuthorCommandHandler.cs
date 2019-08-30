using AudioBook.API.Providers;
using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.AuthorCommands.Update
{
    /// <summary>
    /// Delete author command handler
    /// </summary>
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly ClaimsPrincipal _user;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepo, ClaimsPrincipal user)
        {
            this._authorRepo = authorRepo;
            this._user = user;
        }

        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var username = this._user.GetUsername();
                var data = await this._authorRepo.GetAsync(request.Id);

                data.Name = request.Name;
                data.Description = request.Description;
                data.ModifiedAt = DateTime.UtcNow;
                data.ModifiedBy = username;

                var result = await this._authorRepo.UpdateAsync(data);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
