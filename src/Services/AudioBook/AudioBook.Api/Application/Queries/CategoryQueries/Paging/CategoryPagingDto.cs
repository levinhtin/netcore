using System;

namespace AudioBook.Api.Application.Queries.CategoryQueries.Paging
{
    public class CategoryPagingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
