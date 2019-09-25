using AudioBook.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.ẠudioBookQueries.Paging
{
    public class AudioBookPagingQuery : IRequest<PagedData<AudioBookPagingDto>>
    {
        [FromQuery(Name = "page")]
        public int Page { get; set; }

        [FromQuery(Name = "limit")]
        public int Limit { get; set; }

        [FromQuery(Name = "term")]
        public string Term { get; set; }

        [FromQuery(Name = "category_id")]
        public int? CategoryId { get; set; }
    }
}
