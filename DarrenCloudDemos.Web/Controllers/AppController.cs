using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarrenCloudDemos.Web.Extensions;
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

        /*
         IOptionsSnapshot<TOptions>下一次请求时生效
         IOptionsMonitor<TOptions>访问CurrentValue的时候生效
         IOptions<TOptions>适合常量
         背后IChangeToken
         */

        private readonly IOptionsSnapshot<App> _options;
        //private readonly IOptionsSnapshot<SourceSetting> _dicSettings;
        private readonly SourceSetting _sourceSetting;

        public AppController(IOptionsSnapshot<App> options, SourceSetting sourceSetting)
        {
            _options = options;
            _sourceSetting = sourceSetting;
            //_dicSettings = dicSettings;

            //_dicSettings.OnChange((option, value) => Console.WriteLine("changed"));
        }

        [Route("title")]
        [HttpGet]
        public IActionResult Title()
        {
            //return Content(_options.Value.Title);

            //没有热更新
            List<string> items = new List<string>();
            foreach (var item in _sourceSetting)
            {
                items.Add($"key:{item.Key},value:{item.Value}");
            }
            return Content(string.Join(',', items));
        }
    }
}
