using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authentication
{
    public class NormalAuthAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "API Auth";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
