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

    public class AudioBookTrackController : ControllerBase
    {
    }
}
