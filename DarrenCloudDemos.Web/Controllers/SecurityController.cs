using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using DarrenCloudDemos.Web.NormalAuth;
using DarrenCloudDemos.Web.NormalAuth.Authentication;
using DarrenCloudDemos.Web.NormalAuth.Authorization;
using DarrenCloudDemos.Web.NormalAuth.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DarrenCloudDemos.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IGetNormalUser _getNormalUser;
        private readonly TokenHelper _tokenHelper;

        public SecurityController(IMemoryCache cache, IGetNormalUser getNormalUser, TokenHelper tokenHelper)
        {
            _cache = cache;
            _getNormalUser = getNormalUser;
            _tokenHelper = tokenHelper;
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("shortCode")]
        public ApiResponse ShortCode([FromBody]UserRegister model)
        {
            string shortCode = "1234";

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(300)
            };
            _cache.Set(model.Mobile, shortCode, options);

            var result = new UserRegisterResponse
            {
                ShortCode = shortCode
            };

            return new ApiResponse("success", result, StatusCodes.Status200OK);
        }

        [HttpPost]
        [Route("token")]
        public async Task<ApiResponse> Token([FromBody] UserLogin user)
        {
            var currentUser = await _getNormalUser.Execute(user.UserName, user.Password);
            if(currentUser!=null)
            {
                var token = _tokenHelper.GenerateToken(currentUser.UserName, currentUser.CompanyId, currentUser.Roles);
                return new ApiResponse("success", token, StatusCodes.Status200OK);
            }
            return new ApiResponse("fail", null, StatusCodes.Status200OK);
        }

        [HttpGet("only-admin")]
        [Authorize(Policy = NormalAuthPolicies.OnlyAdmin)]
        public ApiResponse OnlyAdmin()
        {
            var message = "Hello from Admin}";
            return new ApiResponse("success", message, StatusCodes.Status200OK);
        }
    }
}
