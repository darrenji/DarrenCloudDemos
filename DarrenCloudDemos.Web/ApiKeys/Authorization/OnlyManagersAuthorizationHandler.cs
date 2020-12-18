using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.ApiKeys.Authorization
{
    public class OnlyManagersAuthorizationHandler : AuthorizationHandler<OnlyManagersRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyManagersRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Manager))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
