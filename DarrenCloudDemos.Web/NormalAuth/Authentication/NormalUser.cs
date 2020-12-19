using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authentication
{
    public class NormalUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyId { get; set; }


        public IReadOnlyCollection<string> Roles { get; }

        public NormalUser(int id, string userName, string password, string companyId, IReadOnlyCollection<string> roles)
        {
            Id = id;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            CompanyId = companyId ?? throw new ArgumentNullException(nameof(companyId));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }
    }
}
