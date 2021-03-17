using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [UnicornWebHook(Id = "teleported")]
        public async Task<IActionResult> Teleported(object data)
        {
            ///do something with the data
            return Ok();
        }

        [UnicornWebHook(Id = "fallen-asleep")]
        public async Task<IActionResult> FallenAsleep( object data)
        {
            ///do something with the data
            return Ok();
        }

        [UnicornWebHook(Id = "woken-up")]
        public async Task<IActionResult> WokenUp( object data)
        {
            ///do something with the data
            return Ok();
        }
    }
}
