using DarrenCloudDemos.Web.ApiKeys.Authentication;
using DarrenCloudDemos.Web.ApiKeys.Authorization;
using DarrenCloudDemos.Web.ApiKeys.Json;
using DarrenCloudDemos.Web.NormalAuth.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authentication
{
    public class NormalAuthAuthenticationHandler : AuthenticationHandler<NormalAuthAuthenticationOptions>
    {
        private const string ProblemDetailsContentType = "application/problem+json";
        private readonly IGetNormalUser _getNormalUser;
        private readonly TokenHelper _tokenHelper;

        public NormalAuthAuthenticationHandler(
            IOptionsMonitor<NormalAuthAuthenticationOptions> options,
            ILoggerFactory logger,
            TokenHelper tokenHelper,
            UrlEncoder encoder,
            ISystemClock clock,
            IGetNormalUser getNormalUser) : base(options, logger, encoder, clock)
        {
            _getNormalUser = getNormalUser ?? throw new ArgumentNullException(nameof(getNormalUser));
            _tokenHelper = tokenHelper;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //请求头中是否包含X-Api-Key
            if (!Request.Headers.TryGetValue(NormalAuthConstants.HeaderName, out var tokens))
            {
                return AuthenticateResult.NoResult();
            }
            

            if (tokens.Count == 0 || string.IsNullOrWhiteSpace(tokens))
            {
                return AuthenticateResult.NoResult();
            }

            //获取到X-Api-Auth的值
            var token = tokens.FirstOrDefault();
            if(! _tokenHelper.ValidateCurrentToken(token))//验证token是否通过
            {
                return AuthenticateResult.NoResult();
            }

            string username = _tokenHelper.GetClaim(token, "unique_name");
            string companyId = _tokenHelper.GetClaim(token, "CompanyId");

            //和数据库中或内存中的比对
            var currentUser = await _getNormalUser.FindUser(username);

            if (currentUser != null)
            {
                var claims = new List<Claim>
                {
                    //把用户的Name放入Claim
                    new Claim(ClaimTypes.Name, currentUser.UserName),//Owner代表唯一性的编号
                    new Claim("CompanyId", companyId)
                };

                //把用户的Role(可能不止一个)放入Claim
                claims.AddRange(currentUser.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

                //ClaimIdentity
                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                //ClaimIdentity的集合
                var identities = new List<ClaimsIdentity> { identity };
                //ClaimsPrincipal
                var principal = new ClaimsPrincipal(identities);
                //AuthenticationTicket
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid User provided.");
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new UnauthorizedProblemDetails();

            await Response.WriteAsync(JsonSerializer.Serialize(problemDetails, DefaultJsonSerializerOptions.Options));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 403;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new ForbiddenProblemDetails();

            await Response.WriteAsync(JsonSerializer.Serialize(problemDetails, DefaultJsonSerializerOptions.Options));
        }
    }
}
