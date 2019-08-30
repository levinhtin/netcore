using AudioBook.Core.Models;
using AudioBook.Infrastructure.Repositories.Interfaces;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.CategoryQueries.Paging
{
    public class CategoryPagingQueryHandler : IRequestHandler<CategoryPagingQuery, PagedData<CategoryPagingDto>>
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryPagingQueryHandler(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<PagedData<CategoryPagingDto>> Handle(CategoryPagingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await this._categoryRepo.GetAllPagingAsync(request.Page, request.Limit, request.Search);
                var dataDto = data.Adapt<IEnumerable<CategoryPagingDto>>();

                var total = await this._categoryRepo.CountAllAsync(request.Search);

                var result = new PagedData<CategoryPagingDto>(dataDto, total);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
