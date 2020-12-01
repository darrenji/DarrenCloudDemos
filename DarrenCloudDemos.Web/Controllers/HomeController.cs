using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DarrenCloudDemos.Web.Models;
using DarrenCloudDemos.Lib.Trace;

namespace DarrenCloudDemos.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_logger.LogDebug
            //_logger.LogInformation
            //_logger.LogWarning
            //_logger.LogError
        }

        public IActionResult Index()
        {
            //TraceHelper.LogInfo(nameof(HomeController), "this is content");
            //TraceHelper.LogWarning(nameof(HomeController), "this is warning");
            //TraceHelper.LogError(nameof(HomeController), "this is error", "error", "error");
            //TraceHelper.SendCustomLog(nameof(HomeController), "THIS IS CONTENT");

            try
            {
                int i = 0;
                int j = 2 / i;
            }
            catch (Exception ex)
            {
                TraceHelper.BaseExceptionLog(ex, nameof(HomeController));
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
