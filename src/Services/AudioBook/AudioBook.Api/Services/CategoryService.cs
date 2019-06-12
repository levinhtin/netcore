using AudioBook.Api.Services.Interfaces;
using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AudioBook.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<CategoryDetailResponse> GetById(int id)
        {
            var entity = await this._categoryRepo.GetAsync(id);

            var dto = entity.Adapt<CategoryDetailResponse>();

            return dto;
        }

        public async Task Insert(CategoryCreateRequest dto)
        {
            var entity = new Category()
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = "tinlvv"
            };

            var entity2 = dto.Adapt<Category>();

            await this._categoryRepo.InsertAsync(entity);
        }
    }
}
