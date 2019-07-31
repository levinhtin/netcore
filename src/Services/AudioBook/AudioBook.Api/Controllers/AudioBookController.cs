using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioBook.Api.Criteria;
using AudioBook.Api.Filters;
using AudioBook.API.Filters;
using AudioBook.Core.Models;
using AudioBook.Core.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]

    public class AudioBookController : ControllerBase
    {
        [HttpGet("category/{categoryId}/audiobooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Audit]
        [AddHeader("version", "1.1")]
        public async Task<ActionResult<PagedData<AudioBookModel>>> Gets(int categoryId, int page = 1)
        {
            var result = new PagedData<AudioBookModel>(new List<AudioBookModel>(), 0);

            return this.Ok(result);
        }

        [HttpGet("audiobooks")]
        //[Audit]
        [AddHeader("version", "1.0")]
        public async Task<ActionResult<PagedData<AudioBookModel>>> Gets([FromQuery] GetAllAudioBookCriteria filter)
        {
            var result = new PagedData<AudioBookModel>(new List<AudioBookModel>(), 0);

            return this.Ok(result);
        }

        [HttpGet("audiobooks/{id}")]
        //[Audit]
        public async Task<ActionResult<PagedData<AudioBookModel>>> Get(int id)
        {
            return this.Ok(id);
        }

        [HttpPost("audiobooks")]
        [ProducesResponseType(typeof(AudioBookModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]AudioBookModel model)
        {
            return this.Ok(model);
        }
    }
}
