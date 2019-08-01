﻿using AudioBook.Api.Services.Interfaces;
using AudioBook.Core.Constants;
using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using AudioBook.Core.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using AudioBook.Api.Application.Commands.CategoryCreate;
using AudioBook.Api.Application.Commands.CategoryUpdate;
using AudioBook.Api.Application.Queries.CategoryDetail;
using AudioBook.Api.Application.Queries.CategoryPaging;
using AudioBook.Api.Application.Commands.CategoryDelete;
using Microsoft.AspNetCore.Http;
using AudioBook.Api.Filters;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
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
        public async Task<ActionResult<CategoryDetailDTO>> Get(int id)
        {
            var data = await this._mediator.Send(new CategoryDetailQuery() { Id = id });

            if (data == null)
            {
                return this.NoContent();
            }

            var result = new ApiResult<CategoryDetailDTO>()
            {
                Message = ApiMessage.GetOk,
                Data = data
            };

            return this.Ok(result);
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PagedData<CategoryPagingDTO>>> Gets([FromQuery] CategoryPagingQuery query)
        {
            if (query.Page <= 0)
            {
                return this.BadRequest();
            }

            var data = await this._mediator.Send(query);

            var result = new ApiResult<PagedData<CategoryPagingDTO>>()
            {
                Message = ApiMessage.GetOk,
                Data = data
            };

            return this.Ok(result);
        }

        // Post categories
        [HttpPost("categories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand request)
        {
            var id = await this._mediator.Send(request);
            var result = new ApiResult<int>()
            {
                Message = ApiMessage.CreateOk,
                Data = id
            };

            return this.Created("api/categories", result);
        }

        [HttpPut("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryCommand request)
        {
            var data = await this._mediator.Send(request);

            var result = new ApiResult<bool>()
            {
                Message = ApiMessage.UpdateOk,
                Data = data
            };

            return Ok(result);
        }

        [HttpDelete("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await this._mediator.Send(new DeleteCategoryCommand() { Id = id });

            var result = new ApiResult<int>()
            {
                Message = ApiMessage.DeleteOk,
                Data = id
            };

            return Ok(result);
        }
    }
}