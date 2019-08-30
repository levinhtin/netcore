using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioBook.Api.Criteria;
using AudioBook.Api.Filters;
using AudioBook.API.Filters;
using AudioBook.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]

    public class AppBaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
