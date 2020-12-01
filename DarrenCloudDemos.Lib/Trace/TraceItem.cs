using DarrenCloudDemos.Lib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DarrenCloudDemos.Lib.Trace
{
    public class TraceItem : IDisposable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Folder { get; set; } = "App";
        public string FileName { get; set; } 
        public int ThreadId { get; set; } = Thread.CurrentThread.GetHashCode();
        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        private Action<TraceItem> _logEndAction;

        public TraceItem(Action<TraceItem> logEndAction, LogLevel logLevel, string title = null, string content = null, string folder=null, string fileName=null) : this(logEndAction, title, content)
        {
            Folder = folder;
            FileName = fileName;
            LogLevel = logLevel;
        }

        public TraceItem(Action<TraceItem> logEndAction, string title=null, string content=null)
        {
            _logEndAction = logEndAction;
            Title = title;
            Content = content;
            DateTime = SystemTime.Now;
        }

        public void Log(string message)
        {
            if(Content!=null)
            {
                Content +=System.Environment.NewLine;
            }
            Content += $"\t{message}";
        }

        public void Log(string messageFormat, params object[] param)
        {
            Log(messageFormat.FormatWith(param));
        }

        /// <summary>
        /// 获取日志的详情
        /// </summary>
        /// <returns></returns>
        public string GetFullLog()
        {
            string logStr = $@"[[[{GetLogLevelStr(LogLevel)}]]][[{Title}]][{DateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff")}][线程：{ThreadId}]{Content}

";
            return logStr;
        }

        public void Dispose()
        {
            _logEndAction?.Invoke(this);
        }

        private string GetLogLevelStr(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Info:
                    return "Info";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Error:
                    return "Error";
                default:
                    return "Debug";
            }
        }
    }
}
