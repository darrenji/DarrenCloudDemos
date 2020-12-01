using AutoWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.Models
{
    public class MapResponseObject
    {
        [AutoWrapperPropertyMap(Prop.Result)]
        public object Data { get; set; }

        [AutoWrapperPropertyMap(Prop.StatusCode)]
        public int Code { get; set; }
    }
}
