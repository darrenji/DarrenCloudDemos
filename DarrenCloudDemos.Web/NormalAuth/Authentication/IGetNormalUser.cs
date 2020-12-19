using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authentication
{
    public interface IGetNormalUser
    {
        Task<NormalUser> Execute(string userName, string password);
        Task<NormalUser> FindUser(string userName);
    }
}
