using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.CategoryQueries.Detail
{
    public class CategoryDetailQuery : IRequest<CategoryDetailDto>
    {
        public int Id { get; set; }
    }
}
