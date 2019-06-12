using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioBook.API.Filters;
using AudioBook.Core.Models;
using AudioBook.Core.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AudioBookController : ControllerBase
    {
        // GET api/values
        [HttpGet("category/{categoryId}/audiobooks")]
        [Audit]
        public async Task<ActionResult<PagedData<AudioBookModel>>> Get()
        {
            var result = new PagedData<AudioBookModel>(new List<AudioBookModel>(), 0);

            return this.Ok(result);
        }
    }
}
