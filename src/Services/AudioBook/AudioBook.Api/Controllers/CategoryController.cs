using AudioBook.Api.Services.Interfaces;
using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using AudioBook.Core.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<CategoryDetailResponse>> Get(int id)
        {
            var data = await this._categoryService.GetById(id);

            if (data == null)
            {
                return this.NoContent();
            }

            var result = new AppResult<CategoryDetailResponse>()
            {
                Message = "get success",
                Data = data
            };
            return this.Ok(result);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<PagedData<CategoryDetailResponse>>> Gets(int page = 0, int limit = 10, string search = "")
        {
            var data = await this._categoryService.GetAllPagingAsync(page, limit, search);

            var total = await this._categoryService.CountAllAsync(search);

            var result = new PagedData<CategoryDetailResponse>(data, total);

            return this.Ok(result);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> Post([FromBody] CategoryCreateRequest model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                var error = new { Message = "Name is required!" };

                return this.BadRequest(error);
            }

            var id = await this._categoryService.InsertAsync(model);

            var result = new AppResult<int>()
            {
                Message = "Insert success",
                Data = id
            };

            return this.Created("api/categories", result);
        }

        // ToDo: Api update category
        // Them DTO update trong body
        [HttpPut("categories/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryUpdateRequest model)
        {
            var data = await this._categoryService.GetById(id);

            if (data == null)
            {
                return BadRequest(new { error = "Data is not exist" });
            }

            await this._categoryService.Update(model);

            var result = new AppResult<CategoryDetailResponse>()
            {
                Message = "update success",
                Data = null
            };

            return Ok(result);
        }

        // ToDo: Api delete category
        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await this._categoryService.GetById(id);
            if (data == null)
            {
                return NotFound();
            }

            await this._categoryService.Delete(data.Adapt<Category>());
            var result = new AppResult<CategoryDetailResponse>()
            {
                Message = "success",
                Data = null
            };

            return Ok(result);
        }
    }
}