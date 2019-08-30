using AudioBook.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.Api.Application.Queries.AuthorQueries.Paging
{
    public class AuthorPagingQuery : IRequest<PagedData<AuthorPagingDto>>
    {
        [FromQuery(Name = "page")]
        public int Page { get; set; }

        [FromQuery(Name = "limit")]
        public int Limit { get; set; }

        [FromQuery(Name = "term")]
        public string Term { get; set; }
    }
}
