using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        [HttpGet("token")]
        public IActionResult Token()
        {
            var claims = new[] {
                new Claim("Id", "1"),
                new Claim("Role", "Host")
            };

            var token = this._tokenService.GenerateJWT(claims);

            return this.Ok(token);
        }
    }
}
