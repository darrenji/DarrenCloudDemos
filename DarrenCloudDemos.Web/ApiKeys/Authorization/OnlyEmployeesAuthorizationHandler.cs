using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.ApiKeys.Authorization
{
    public class OnlyEmployeesAuthorizationHandler : AuthorizationHandler<OnlyEmployeesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyEmployeesRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Employee))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
