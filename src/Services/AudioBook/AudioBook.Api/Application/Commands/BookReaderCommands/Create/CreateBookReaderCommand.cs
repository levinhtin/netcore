using MediatR;

namespace AudioBook.Api.Application.Commands.BookReaderCommands.Create
{
    public class CreateBookReaderCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
    }
}