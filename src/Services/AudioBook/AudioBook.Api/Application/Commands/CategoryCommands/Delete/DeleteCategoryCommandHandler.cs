using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryCommands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepo;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await this._categoryRepo.GetAsync(request.Id);
                if (data != null)
                {
                    return await this._categoryRepo.DeleteAsync(data);
                }

                return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
