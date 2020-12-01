using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 时间扩展类
    /// </summary>
    public static class SystemTime
    {
        ///// <summary>
        ///// 返回 Now 方法
        ///// </summary>
        //public static Func<DateTime> GetNow = () => SystemTime.Now;

        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTimeOffset Now => DateTimeOffset.Now;

        /// <summary>
        /// 当前时间的 UTC DateTime 类型
        /// </summary>
        public static DateTime UtcDateTime => DateTimeOffset.Now.UtcDateTime;

        /// <summary>
        /// 当天零点时间，从 SystemTime.Now.Date 获得
        /// </summary>
        public static DateTime Today => Now.Date;
        /// <summary>

        /// 获取当前时间的 Ticks
        /// </summary>
        public static long NowTicks => Now.Ticks;



        /// <summary>
        /// 获取 TimeSpan
        /// </summary>
        /// <param name="compareTime">当前时间 - compareTime</param>
        /// <returns></returns>
        public static TimeSpan NowDiff(DateTimeOffset compareTime)
        {
            return Now - compareTime;
        }

        /// <summary>
        /// 获取 TotalMilliseconds 时间差
        /// </summary>
        /// <param name="compareTime">当前时间 - compareTime</param>
        /// <returns></returns>
        public static double DiffTotalMS(DateTimeOffset compareTime)
        {
            return NowDiff(compareTime).TotalMilliseconds;
        }


        /// <summary>
        /// 获取 TotalMilliseconds 时间差
        /// </summary>
        /// <param name="compareTime">当前时间 - compareTime</param>
        /// <param name="format">对 TotalMilliseconds 结果进行 ToString([format]) 中的参数</param>
        /// <returns></returns>
        public static string DiffTotalMS(DateTimeOffset compareTime, string format)
        {
            return NowDiff(compareTime).TotalMilliseconds.ToString(format);
        }

        /// <summary>
        /// 获取 TimeSpan
        /// </summary>
        /// <param name="compareTime">当前时间 - compareTime</param>
        /// <returns></returns>
        public static TimeSpan NowDiff(DateTime compareTime)
        {
            return Now.DateTime - compareTime;
        }

        /// <summary>
        /// 获取 TotalMilliseconds 时间差
        /// </summary>
        /// <param name="compareTime">当前时间 - compareTime</param>
        /// <returns></returns>
        public static double DiffTotalMS(DateTime compareTime)
        {
            return NowDiff(compareTime).TotalMilliseconds;
        }

        /// <summary>
        /// 获取 TotalMilliseconds 时间差
        /// </summary>
        /// <param name="compareTime">当前时间 - compareTime</param>
        /// <param name="format">对 TotalMilliseconds 结果进行 ToString([format]) 中的参数</param>
        /// <returns></returns>
        public static string DiffTotalMS(DateTime compareTime, string format)
        {
            return NowDiff(compareTime).TotalMilliseconds.ToString(format);
        }

        //TODO：添加更多实用方法

    }
}
