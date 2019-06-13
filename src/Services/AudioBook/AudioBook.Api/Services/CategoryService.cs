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

        public async Task Delete(Category category)
        {
            var data = await this._categoryRepo.DeleteAsync(category);
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
                CreatedBy = "havi"
            };

            var entity2 = dto.Adapt<Category>();
            await this._categoryRepo.InsertAsync(entity);
        }

        public async Task Update(CategoryUpdateRequest dto)
        {
            var data = await this._categoryRepo.GetAsync(dto.Id);

            data.Name = dto.Name;
            data.Description = dto.Description;

            // var entity2 = dto.Adapt<Category>();
            var result = await this._categoryRepo.UpdateAsync(data);
        }
    }
}
