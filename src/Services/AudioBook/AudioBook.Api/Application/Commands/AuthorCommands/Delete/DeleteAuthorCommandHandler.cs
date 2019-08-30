using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.AuthorCommands.Delete
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepo;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepo)
        {
            this._authorRepo = authorRepo;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await this._authorRepo.GetAsync(request.Id);

                var result = await this._authorRepo.DeleteAsync(author);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
