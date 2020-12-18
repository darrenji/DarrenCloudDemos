using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.ApiKeys.Authorization
{
    public class OnlyThirdPartiesAuthorizationHandler : AuthorizationHandler<OnlyThirdPartiesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyThirdPartiesRequirement requirement)
        {
            if (context.User.IsInRole(Roles.ThirdParty))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
