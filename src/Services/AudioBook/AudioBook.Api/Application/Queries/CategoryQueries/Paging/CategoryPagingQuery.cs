using AudioBook.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.Api.Application.Queries.CategoryQueries.Paging
{
    public class CategoryPagingQuery : RequestAudit, IRequest<PagedData<CategoryPagingDto>>
    {
        [FromQuery(Name = "search")]
        public string Search { get; set; }

        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;

        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 10;
    }
}
