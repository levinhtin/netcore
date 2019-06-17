using AudioBook.Api.Services.Interfaces;
using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using AudioBook.Core.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AudioBook.Core.Constants;
using MediatR;
using AudioBook.Api.Application.Commands.CategoryCreate;
using AudioBook.Api.Application.Commands.CategoryUpdate;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMediator mediator, ICategoryService categoryService)
        {
            this._mediator = mediator;
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

            var result = new ApiResult<CategoryDetailResponse>()
            {
                Message = "Get success",
                Data = data
            };

            return this.Ok(result);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<PagedData<CategoryDetailResponse>>> Gets(int page = 1, int limit = 10, string search = "")
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var data = await this._categoryService.GetAllPagingAsync(page, limit, search);

            var total = await this._categoryService.CountAllAsync(search);
            var result = new ApiResult<PagedData<CategoryDetailResponse>>()
            {
                Message = ApiMessage.GetOk,
                Data = new PagedData<CategoryDetailResponse>(data, total)
            };

            return this.Ok(result);
        }

		// Post categories
        [HttpPost("categories")]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand request)
        {
            var id = await this._mediator.Send(request);
            var result = new ApiResult<int>()
            {
                Message = "Insert success",
                Data = id
            };

            return this.Created("api/categories", result);
        }

        // ToDo: Api update category
        // Them DTO update trong body
        [HttpPut("categories/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryCommand request)
        {
            await this._mediator.Send(request);

            var result = new ApiResult<CategoryDetailResponse>()
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

            var result = new ApiResult<CategoryDetailResponse>()
            {
                Message = "success",
                Data = null
            };

            return Ok(result);
        }
    }
}