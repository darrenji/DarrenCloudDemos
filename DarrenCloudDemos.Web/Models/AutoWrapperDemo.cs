using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.Models
{
    public class AutoWrapperDemo
    {
        [Required(ErrorMessage ="必填")]
        [MaxLength(5, ErrorMessage ="最大长度5")]
        public string Name { get; set; }


        [Required(ErrorMessage = "必填")]
        [MaxLength(5, ErrorMessage = "最大长度5")]
        public string Location { get; set; }
    }
}
