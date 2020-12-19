using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth
{
    public class UserRegister
    {
        public string Mobile { get; set; }
    }

    public class UserRegisterResponse
    {
        public string ShortCode { get; set; }
    }
}
