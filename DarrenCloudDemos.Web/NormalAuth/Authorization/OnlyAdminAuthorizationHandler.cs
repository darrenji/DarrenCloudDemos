using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authorization
{
    public class OnlyAdminAuthorizationHandler : AuthorizationHandler<OnlyAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyAdminRequirement requirement)
        {
            if (context.User.IsInRole(NormalAuthRole.Admin))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
