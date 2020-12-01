using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.Exceptions
{
    /// <summary>
    /// 基类异常
    /// </summary>
    public class BaseException : ApplicationException
    {

        /// <summary>
        /// 是否记录异常日志，不带内部异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="logged">是否记录日志</param>
        public BaseException(string message, bool logged=false) : this(message, null, logged)
        {

        }

        /// <summary>
        /// 是否记录异常日志，包括内部异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="inner">内部异常</param>
        /// <param name="logged">是否记录日志</param>
        public BaseException(string message, Exception inner, bool logged = false) : base(message, inner)
        {
            if(!logged)
            {
                //记录日志异常
            }
        }
    }
}
