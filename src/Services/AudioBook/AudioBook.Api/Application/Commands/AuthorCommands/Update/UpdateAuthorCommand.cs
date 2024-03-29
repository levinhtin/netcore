﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.AuthorCommands.Update
{
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
