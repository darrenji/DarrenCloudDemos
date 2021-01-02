using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.MultiTenants
{
    public class Tenant
    {
        [Key]
        public Guid Guid { get; set; }
        public string ConnectionString { get; set; }
        public string Name { get; set; }
    }
}
