using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarrenCloudDemos.Web.ApiKeys.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DarrenCloudDemos.Web.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("anyone")]
        public IActionResult Anyone()
        {
            var message = $"Hello from {nameof(Anyone)}";
            return new ObjectResult(message);
        }

        [HttpGet("only-authenticated")]
        [Authorize]
        public IActionResult OnlyAuthenticated()
        {
            var message = $"Hello from {nameof(OnlyAuthenticated)}";
            return new ObjectResult(message);
        }

        [HttpGet("only-employees")]
        [Authorize(Policy = Policies.OnlyEmployees)]
        public IActionResult OnlyEmployees()
        {
            var message = $"Hello from {nameof(OnlyEmployees)}";
            return new ObjectResult(message);
        }

        [HttpGet("only-managers")]
        [Authorize(Policy = Policies.OnlyManagers)]
        public IActionResult OnlyManagers()
        {
            var message = $"Hello from {nameof(OnlyManagers)}";
            return new ObjectResult(message);
        }

        [HttpGet("only-third-parties")]
        [Authorize(Policy = Policies.OnlyThirdParties)]
        public IActionResult OnlyThirdParties()
        {
            var message = $"Hello from {nameof(OnlyThirdParties)}";
            return new ObjectResult(message);
        }
    }
}
