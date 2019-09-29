using AudioBook.API.Providers;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryCommands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly ClaimsPrincipal _user;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepo, ClaimsPrincipal user)
        {
            this._categoryRepo = categoryRepo;
            this._user = user;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Category() { Name = request.Name, Description = request.Description };

                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedBy = this._user.GetUsername();

                return await this._categoryRepo.InsertAsync(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
