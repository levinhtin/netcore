using AudioBook.Core.Models;
using AudioBook.Infrastructure.Repositories.Interfaces;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.AuthorQueries.Paging
{
    public class AuthorPagingQueryHandler : IRequestHandler<AuthorPagingQuery, PagedData<AuthorPagingDto>>
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorPagingQueryHandler(IAuthorRepository authorRepo)
        {
            this._authorRepo = authorRepo;
        }

        public async Task<PagedData<AuthorPagingDto>> Handle(AuthorPagingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Get data
                var data = await this._authorRepo.GetAllPagingAsync(request.Page, request.Limit, request.Term);

                // Convert to dto
                var dataDto = data.Adapt<IEnumerable<AuthorPagingDto>>();

                // Count total
                var total = await this._authorRepo.CountAllAsync(request.Term);

                var result = new PagedData<AuthorPagingDto>(dataDto, total);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
