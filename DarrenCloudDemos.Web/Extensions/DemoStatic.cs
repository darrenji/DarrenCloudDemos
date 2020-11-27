using DarrenCloudDemos.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.Extensions
{
    public static class DemoStatic
    {
        public static void SomeMethod()
        {
            string msg = string.Empty;

            using(var serviceScope = ServiceActivator.GetScope())
            {
               // var s = serviceScope.ServiceProvider.GetService<SymmetricEncryptDecrypt>();
            }
        }
    }
}
