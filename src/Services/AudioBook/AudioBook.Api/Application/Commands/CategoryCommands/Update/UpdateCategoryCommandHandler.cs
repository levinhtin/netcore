using AudioBook.API.Providers;
using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryCommands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly ClaimsPrincipal _user;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepo, ClaimsPrincipal user)
        {
            this._categoryRepo = categoryRepo;
            this._user = user;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await this._categoryRepo.GetAsync(request.Id);

                data.Name = request.Name;
                data.Description = request.Description;

                data.ModifiedBy = this._user.GetUsername();
                data.ModifiedAt = DateTime.UtcNow;

                return await this._categoryRepo.UpdateAsync(data);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
