using System;
using System.Threading.Tasks;
using AudioBook.Api.Application.Commands.CategoryCommands.Create;
using AudioBook.Api.Application.Commands.CategoryCommands.Delete;
using AudioBook.Api.Application.Commands.CategoryCommands.Update;
using AudioBook.Api.Application.Queries.CategoryQueries.Detail;
using AudioBook.Api.Application.Queries.CategoryQueries.Paging;
using AudioBook.Api.Filters;
using AudioBook.Core.Constants;
using AudioBook.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : AppBaseController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[Audit]
        [AddHeader("version", "1.0")]
        public async Task<ActionResult<CategoryDetailDto>> Get([FromRoute]int id)
        {
            try
            {
                var data = await this._mediator.Send(new CategoryDetailQuery(id));

                if (data == null)
                {
                    return this.NoContent();
                }

                var result = new ApiResult<CategoryDetailDto>()
                {
                    Message = ApiMessage.GetOk,
                    Data = data
                };

                return this.Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedData<CategoryPagingDto>>> Gets([FromQuery] CategoryPagingQuery query)
        {
            try
            {
                if (query.Page <= 0)
                {
                    return this.BadRequest();
                }

                var data = await this.Mediator.Send(query);

                var result = new ApiResult<PagedData<CategoryPagingDto>>()
                {
                    Message = ApiMessage.GetOk,
                    Data = data
                };

                return this.Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Post categories
        [HttpPost("categories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand request)
        {
            try
            {
                var id = await this.Mediator.Send(request);
                var result = new ApiResult<int>()
                {
                    Message = ApiMessage.CreateOk,
                    Data = id
                };

                return this.Created("api/categories", result);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryCommand request)
        {
            try
            {
                var data = await this._mediator.Send(request);

                var result = new ApiResult<bool>()
                {
                    Message = ApiMessage.UpdateOk,
                    Data = data
                };

                return this.Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = await this._mediator.Send(new DeleteCategoryCommand(id));

                var result = new ApiResult<int>()
                {
                    Message = ApiMessage.DeleteOk,
                    Data = id
                };

                return this.Ok(result);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
