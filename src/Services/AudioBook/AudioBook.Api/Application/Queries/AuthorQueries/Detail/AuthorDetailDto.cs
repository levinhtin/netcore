using AudioBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.AuthorQueries.Detail
{
    public class AuthorDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public static AuthorDetailDto FromEntity(Author entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new AuthorDetailDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DateOfBirth = entity.DateOfBirth
            };
        }
    }
}
