using AudioBook.Infrastructure.Repositories.Interfaces;
using Mapster;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.CategoryQueries.Detail
{
    public class CategoryDetailQueryHandler : IRequestHandler<CategoryDetailQuery, CategoryDetailDto>
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryDetailQueryHandler(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<CategoryDetailDto> Handle(CategoryDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await this._categoryRepo.GetAsync(request.Id);

                var model = item.Adapt<CategoryDetailDto>();

                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
