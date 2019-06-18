using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.AuthorUpdate
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepo;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepo)
        {
            this._authorRepo = authorRepo;
        }

        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var data = await this._authorRepo.GetAsync(request.Id);

            data.Name = request.Name;
            data.Description = request.Description;
            data.DateOfBirth = request.DateOfBirth;

            return await this._authorRepo.UpdateAsync(data);
        }
    }
}
