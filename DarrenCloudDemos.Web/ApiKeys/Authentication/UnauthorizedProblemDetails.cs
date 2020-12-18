using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.ApiKeys.Authentication
{
    public class UnauthorizedProblemDetails : ProblemDetails
    {
        public UnauthorizedProblemDetails(string details = null)
        {
            Title = "Unauthorized";
            Detail = details;
            Status = 401;
            Type = "https://httpstatuses.com/401";
        }
    }
}
