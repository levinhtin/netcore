﻿using AudioBook.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryCommands.Update
{
    public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepo;

        public UpdateCategoryValidation(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;

            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Id).MustAsync((request, id, token) => CheckIdExist(id)).WithMessage("Data not exists");
        }

        private async Task<bool> CheckIdExist(int id)
        {
            var data = await this._categoryRepo.GetAsync(id);

            return data != null;
        }
    }
}
