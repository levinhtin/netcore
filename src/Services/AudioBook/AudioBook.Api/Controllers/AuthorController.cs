using AudioBook.Api.Application.Commands.AuthorCommands.Create;
using AudioBook.Api.Application.Commands.AuthorCommands.Delete;
using AudioBook.Api.Application.Commands.AuthorCommands.Update;
using AudioBook.Api.Application.Queries.AuthorQueries.Detail;
using AudioBook.Api.Application.Queries.AuthorQueries.Paging;
using AudioBook.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorController : AppBaseController
    {
        [HttpGet("authors/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AuthorDetailDto>> Get(int id)
        {
            var data = await this.Mediator.Send(new AuthorDetailQuery(id));

            if (data == null)
            {
                return this.NoContent();
            }

            var result = new ApiResult<AuthorDetailDto>()
            {
                Message = "Get success",
                Data = data
            };

            return this.Ok(result);
        }

        [HttpGet("authors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Gets([FromQuery] AuthorPagingQuery query)
        {
            var data = await this.Mediator.Send(query);

            var result = new ApiResult<PagedData<AuthorPagingDto>>()
            {
                Message = "Get success",
                Data = data
            };

            return this.Ok(result);
        }

        [HttpPost("authors")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] CreateAuthorCommand command)
        {
            var id = await this.Mediator.Send(command);

            var result = new ApiResult<int>()
            {
                Message = "Create success",
                Data = id
            };

            return this.Created("api/authors", result);
        }

        [HttpPut("authors/{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UpdateAuthorCommand command)
        {
            var isSuccess = await this.Mediator.Send(command);

            var result = new ApiResult<bool>()
            {
                Message = "Update success",
                Data = isSuccess
            };

            return Ok(result);
        }

        [HttpDelete("authors/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await this.Mediator.Send(new DeleteAuthorCommand(id));
            
            var result = new ApiResult<bool>()
            {
                Message = "Deleted",
                Data = isSuccess
            };

            return Ok(result);
        }
    }
}