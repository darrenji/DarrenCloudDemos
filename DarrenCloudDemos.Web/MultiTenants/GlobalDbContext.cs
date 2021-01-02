using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DarrenCloudDemos.Web.MultiTenants
{
    public class GlobalDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options)
        {

        }
    }
}
