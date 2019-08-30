using AudioBook.API.Providers;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.AuthorCommands.Create
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly ClaimsPrincipal _user;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepo, ClaimsPrincipal user)
        {
            this._authorRepo = authorRepo;
            this._user = user;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var username = this._user.GetUsername();
                var entity = new Author(request.Name, request.Description, request.DateOfBirth);

                // Set audit
                entity.CreatedBy = username;
                entity.CreatedAt = DateTime.UtcNow;

                return await this._authorRepo.InsertAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
