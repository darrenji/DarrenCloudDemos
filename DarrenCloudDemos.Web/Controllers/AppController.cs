using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DarrenCloudDemos.Web.Controllers
{
    /// <summary>
    /// 热加载配置数据
    /// </summary>
    [Route("api/app")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IOptionsSnapshot<App> _options;

        public AppController(IOptionsSnapshot<App> options)
        {
            _options = options;
        }

        [Route("title")]
        [HttpGet]
        public IActionResult Title()
        {
            return Content(_options.Value.Title);
        }
    }
}
