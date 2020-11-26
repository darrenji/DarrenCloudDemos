using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using DarrenCloudDemos.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DarrenCloudDemos.Web.Controllers
{
    /// <summary>
    /// AutoWrapper的测试
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutoWrapperController : ControllerBase
    {
        /// <summary>
        /// ApiResponse作为返回类型
        /// </summary>
        /// <returns></returns>
        [Route("get1")]
        [HttpGet]
        public ApiResponse Get1()
        {
            return new ApiResponse("New record has been created in the database", 1, StatusCodes.Status201Created);
        }

        //public ApiResponse Post1(AutoWrapperDemo model)
        //{

        //}
    }
}
