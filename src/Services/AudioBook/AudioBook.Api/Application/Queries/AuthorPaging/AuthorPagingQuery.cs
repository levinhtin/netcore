﻿using AudioBook.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.Api.Application.Queries.AuthorPaging
{
    public class AuthorPagingQuery : IRequest<PagedData<AuthorPagingDTO>>
    {
        [FromQuery(Name = "Search")]
        public string Search { get; set; }

        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;

        [FromQuery(Name = "Limit")]
        public int Limit { get; set; } = 10;
    }
}