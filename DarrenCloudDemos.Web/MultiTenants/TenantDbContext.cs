using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.MultiTenants
{
    public class TenantDbContext : DbContext
    {
        private readonly Tenant _tenant;
        public DbSet<TenantConfig> TenantConfig { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            if(httpContextAccessor.HttpContext!=null)
            {
                _tenant = (Tenant)httpContextAccessor.HttpContext.Items["TENANT"];
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_tenant.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
