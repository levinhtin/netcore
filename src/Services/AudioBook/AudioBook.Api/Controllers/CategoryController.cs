using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using AudioBook.Api.Services.Interfaces;
using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Models;
using System.Collections.Generic;
using Mapster;

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

            return this.Ok(data);
        }

        // ToDo: Api get ds category
        [HttpGet("categories")]
        public async Task<ActionResult<PagedData<CategoryDetailResponse>>> Gets(int page = 1, int limit = 10)
        {
            var result = new PagedData<CategoryDetailResponse>(new List<CategoryDetailResponse>(), 0);
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

            await this._categoryService.Insert(model);

            return this.Created("api/category", model);
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
            return Ok(new { success = "You updated successfully" });
           
        }

        // ToDo: Api delete category
        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await this._categoryService.GetById(id);
            if(data == null)
            {
                return NotFound();
            }

            await this._categoryService.Delete(data.Adapt<Category>());
            return new ObjectResult(data);
        }
    }
}