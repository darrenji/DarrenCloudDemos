using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authentication
{
    public static class NormalAuthBuilderExtensions
    {
        public static AuthenticationBuilder AddNormalAuthSupport(this AuthenticationBuilder authenticationBuilder, Action<NormalAuthAuthenticationOptions> options)
        {
            return authenticationBuilder.AddScheme<NormalAuthAuthenticationOptions, NormalAuthAuthenticationHandler>(NormalAuthAuthenticationOptions.DefaultScheme, options); 
        }
    }
}
