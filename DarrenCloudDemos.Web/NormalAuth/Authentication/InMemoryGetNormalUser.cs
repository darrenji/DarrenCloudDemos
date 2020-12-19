using DarrenCloudDemos.Web.NormalAuth.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.NormalAuth.Authentication
{
    public class InMemoryGetNormalUser : IGetNormalUser
    {
        private readonly IDictionary<string, NormalUser> _users;
        public InMemoryGetNormalUser()
        {
            var existingUsers = new List<NormalUser>
            {
                new NormalUser(1, "13811111111", "88888888", "1",
                    new List<string>
                    {
                        NormalAuthRole.Admin,
                    }),
                new NormalUser(1, "13822222222", "88888888", "1",
                    new List<string>
                    {
                        NormalAuthRole.Admin,
                    }),

            };

            _users = existingUsers.ToDictionary(x => x.UserName, x => x);
        }
        public Task<NormalUser> Execute(string userName, string password)
        {
            _users.TryGetValue(userName, out var key);

            if(key.Password==password)
            {
                return Task.FromResult(key);
            }
            return null;
        }

        public Task<NormalUser> FindUser(string userName)
        {
            _users.TryGetValue(userName, out var key);
            return Task.FromResult(key);
        }
    }
}
