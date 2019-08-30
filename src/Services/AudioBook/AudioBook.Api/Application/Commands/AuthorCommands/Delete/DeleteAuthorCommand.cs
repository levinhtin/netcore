using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.AuthorCommands.Delete
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAuthorCommand(int id)
        {
            this.Id = id;
        }
    }
}
