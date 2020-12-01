using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.Extensions
{
    public static class ObjectExtensions
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
