using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.AuthorQueries.Detail
{
    public class AuthorDetailQueryHandler : IRequestHandler<AuthorDetailQuery, AuthorDetailDto>
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorDetailQueryHandler(IAuthorRepository authorRepo)
        {
            this._authorRepo = authorRepo;
        }

        public async Task<AuthorDetailDto> Handle(AuthorDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await this._authorRepo.GetAsync(request.Id);
                var result = AuthorDetailDto.FromEntity(data);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
