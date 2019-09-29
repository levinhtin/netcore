using System;
using System.Threading.Tasks;
using AudioBook.Api.Application.Commands.AudioBookCommands.Create;
using AudioBook.Api.Application.Queries.ẠudioBookQueries.Detail;
using AudioBook.Api.Application.Queries.ẠudioBookQueries.Paging;
using AudioBook.Api.Filters;
using AudioBook.Core.Constants;
using AudioBook.Core.Models;
using AudioBook.Core.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]

    public class AudioBookController : AppBaseController
    {
        [HttpGet("audiobooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[Audit]
        [AddHeader("version", "1.0")]
        public async Task<ActionResult<PagedData<AudioBookPagingDto>>> Gets([FromQuery] AudioBookPagingQuery query)
        {
            try
            {
                var data = await this.Mediator.Send(query);

                var result = new ApiResult<PagedData<AudioBookPagingDto>>()
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

        [HttpGet("audiobooks/{id}")]
        //[Audit]
        public async Task<ActionResult<AudioBookDetailDto>> Get([FromRoute]int id)
        {
            try
            {
                var data = await this.Mediator.Send(new AudioBookDetailQuery(id));

                var result = new ApiResult<AudioBookDetailDto>()
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

        [HttpPost("audiobooks")]
        [ProducesResponseType(typeof(AudioBookModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]CreateAudioBookCommand command)
        {
            var result = await this.Mediator.Send(command);

            return this.Created("audiobooks", result);
        }
    }
}
