using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryCommands.Create
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
