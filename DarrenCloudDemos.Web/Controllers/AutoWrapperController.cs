using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using DarrenCloudDemos.Lib.ApiResponses;
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
          
            try
            {

                int i = 0;
                int j = 5 / i;
                return new ApiResponse("New record has been created in the database", 1, StatusCodes.Status201Created);
            }
            catch (Exception)
            {

                throw;
            }

            //var data = new { Id = 1, Name = "name", Age = 21 };

            //return new ApiResponse("this is message", data, StatusCodes.Status200OK);

           

        }

        [HttpPost]
        [Route("customException")]
        public ApiResponse Post1(AutoWrapperDemo model)
        {
            if(!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }
            return new ApiResponse("New record has been created in the database", 1, StatusCodes.Status201Created);
        }

        [HttpGet]
        [Route("get2")]
        public ApiResponse Get2()
        {
            throw new ApiException("doesnt exist", StatusCodes.Status404NotFound);
        }

        [HttpGet]
        [Route("get3")]
        public MyCustomApiResponse Get3()
        {
            var data = new { name="name", age=12};
            return new MyCustomApiResponse(DateTime.UtcNow, data, new Pagination { CurrentPage = 1, PageSize = 10, TotalItemsCount = 200, TotalPages = 20 });
        }

        [HttpPost]
        [Route("post2")]
        public MyCustomApiResponse Post2(AutoWrapperDemo model)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }

            var data = new { name = "name", age = 12 };
            return new MyCustomApiResponse(DateTime.UtcNow, data, new Pagination { CurrentPage = 1, PageSize = 10, TotalItemsCount = 200, TotalPages = 20 });
        }
    }
}
