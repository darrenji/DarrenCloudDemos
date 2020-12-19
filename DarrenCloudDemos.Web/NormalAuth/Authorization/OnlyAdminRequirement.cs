using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authorization
{
    public class OnlyAdminRequirement : IAuthorizationRequirement
    {
    }
}
