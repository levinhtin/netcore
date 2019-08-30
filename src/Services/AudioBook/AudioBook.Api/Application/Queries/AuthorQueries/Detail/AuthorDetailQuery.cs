using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.AuthorQueries.Detail
{
    public class AuthorDetailQuery : IRequest<AuthorDetailDto>
    {
        public int Id { get; set; }

        public AuthorDetailQuery(int id)
        {
            this.Id = id;
        }
    }
}
