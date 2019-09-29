using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AudioBook.Api.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("perf", Name = "Test Performance")]
        public async Task<IActionResult> Get()
        {
            return this.Ok("Ok");
        }
    }
}
