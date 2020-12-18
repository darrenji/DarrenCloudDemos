using DarrenCloudDemos.Web.ApiKeys.Authorization;
using DarrenCloudDemos.Web.ApiKeys.Json;
using DarrenCloudDemos.Web.ApiKeys.Shared;
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

namespace DarrenCloudDemos.Web.ApiKeys.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ProblemDetailsContentType = "application/problem+json";
        private readonly IGetApiKeyQuery _getApiKeyQuery;


        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IGetApiKeyQuery getApiKeyQuery) : base(options, logger, encoder, clock)
        {
            _getApiKeyQuery = getApiKeyQuery ?? throw new ArgumentNullException(nameof(getApiKeyQuery));
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //请求头中是否包含X-Api-Key
            if (!Request.Headers.TryGetValue(ApiKeyConstants.HeaderName, out var apiKeyHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }
            //获取到X-Api-Key的值
            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return AuthenticateResult.NoResult();
            }

            //和数据库中或内存中的比对
            var existingApiKey = await _getApiKeyQuery.Execute(providedApiKey);

            if (existingApiKey != null)
            {
                var claims = new List<Claim>
                {
                    //把用户的Name放入Claim
                    new Claim(ClaimTypes.Name, existingApiKey.Owner)//Owner代表唯一性的编号
                };

                //把用户的Role(可能不止一个)放入Claim
                claims.AddRange(existingApiKey.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

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

            return AuthenticateResult.Fail("Invalid API Key provided.");
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
