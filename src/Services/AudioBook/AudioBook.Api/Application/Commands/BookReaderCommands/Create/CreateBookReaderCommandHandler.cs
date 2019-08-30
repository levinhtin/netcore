using System.Threading;
using System.Threading.Tasks;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories;
using MediatR;

namespace AudioBook.Api.Application.Commands.BookReaderCommands.Create
{
    public class CreateBookReaderCommandHandler : IRequestHandler<CreateBookReaderCommand, int>
    {
        private readonly BookReaderRepository _bookReaderRepo;

        public CreateBookReaderCommandHandler(BookReaderRepository bookReaderRepo)
        {
            this._bookReaderRepo = bookReaderRepo;
        }
        
        public async Task<int> Handle(CreateBookReaderCommand request, CancellationToken cancellationToken)
        {
            var reader = new BookReader(firstName: request.FirstName, lastName: request.LastName, displayName: request.DisplayName);
            return 1;
        }
    }
}