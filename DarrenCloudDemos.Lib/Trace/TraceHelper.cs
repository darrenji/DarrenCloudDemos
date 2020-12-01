using DarrenCloudDemos.Lib.Exceptions;
using DarrenCloudDemos.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DarrenCloudDemos.Lib.Trace
{
    public class TraceHelper
    {
        /// <summary>
        /// 日志锁的名称
        /// </summary>
        //const string LockName = "DDTraceLock";

        /// <summary>
        /// 记录BaseException日志需要执行的动作
        /// </summary>
        public static Action<BaseException> OnBaseExceptionFunc;

        /// <summary>
        /// 记录所有日志后要执行的动作
        /// </summary>
        public static Action OnLogFunc;


        /// <summary>
        /// 当日志文件被占用，如果true表示启用GC操作，如果false会抛出异常
        /// </summary>
        public static bool AutoUnlockLogFile { get; set; } = true;

        protected static Action<string> _queue = async (logStr) =>
        {
            string logDir = Path.Combine(Config.RootPath, "App_Data", "DDTraceLog");

            if(!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            string logFile = Path.Combine(logDir, $"DDTrace-{SystemTime.Now.ToString("yyyyMMdd")}.log");

            if(AutoUnlockLogFile)//处理文件被占用的情况，不抛异常
            {
                const int maxRetryTimes = 3;//重试3次
                const int retryDelayTimeMilliSeconds = 100;//重试等待100毫秒

                for(int i=0;i<maxRetryTimes;i++)
                {
                    if(FileHelper.FileInUse(logFile))
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        var dt = SystemTime.Now;
                        if (i < maxRetryTimes - 1)
                        {
                            while (SystemTime.NowDiff(dt).TotalMilliseconds < retryDelayTimeMilliSeconds)
                            {
                                //如果不是最后一次尝试，则等待一段时间再进入下一步
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }


            try
            {
                using (var fs = new FileStream(logFile, FileMode.OpenOrCreate))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        fs.Seek(0, SeekOrigin.End);
                        await sw.WriteAsync(logStr);
                        await sw.FlushAsync();
                    }
                }
            }
            catch (Exception)
            {

            }

            if(OnLogFunc!=null)
            {
                try
                {
                    OnLogFunc();
                }
                catch (Exception)
                {
                }
            }
        };


        protected static Action<LogOption> _queue1 = async (logOption) =>
        {
            string logDir = Path.Combine(Config.WebPath, "Logs", logOption.Folder);

            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            string logFile = string.Empty;

            if(string.IsNullOrEmpty(logOption.FileName))
            {
                logFile = Path.Combine(logDir, $"{SystemTime.Now.ToString("yyyyMMdd")}.log");
            }
            else
            {
                logFile = Path.Combine(logDir, $"{logOption.FileName}-{SystemTime.Now.ToString("yyyyMMdd")}.log");
            }
            

            if (AutoUnlockLogFile)//处理文件被占用的情况，不抛异常
            {
                const int maxRetryTimes = 3;//重试3次
                const int retryDelayTimeMilliSeconds = 100;//重试等待100毫秒

                for (int i = 0; i < maxRetryTimes; i++)
                {
                    if (FileHelper.FileInUse(logFile))
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        var dt = SystemTime.Now;
                        if (i < maxRetryTimes - 1)
                        {
                            while (SystemTime.NowDiff(dt).TotalMilliseconds < retryDelayTimeMilliSeconds)
                            {
                                //如果不是最后一次尝试，则等待一段时间再进入下一步
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }


            try
            {
                using (var fs = new FileStream(logFile, FileMode.OpenOrCreate))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        fs.Seek(0, SeekOrigin.End);
                        await sw.WriteAsync(logOption.LogStr);
                        await sw.FlushAsync();
                    }
                }
            }
            catch (Exception)
            {

            }

            if (OnLogFunc != null)
            {
                try
                {
                    OnLogFunc();
                }
                catch (Exception)
                {
                }
            }
        };


        protected static Action<TraceItem> _logEndAction = (traceItem) =>
        {
            var logStr = traceItem.GetFullLog();
            MessageQueue.MessageQueue messageQueue = new MessageQueue.MessageQueue();
            var key = $"{SystemTime.Now.Ticks.ToString()}{traceItem.ThreadId.ToString()}{logStr.Length.ToString()}";//确保全局唯一
            messageQueue.Add(key, () => _queue(logStr));
        };

        protected static Action<TraceItem> _logEndAction1 = (traceItem) =>
        {
            var logStr = traceItem.GetFullLog();
            MessageQueue.MessageQueue messageQueue = new MessageQueue.MessageQueue();
            var key = $"{SystemTime.Now.Ticks.ToString()}{traceItem.ThreadId.ToString()}{logStr.Length.ToString()}";//确保全局唯一
            var logOption = new LogOption(traceItem.Folder, traceItem.FileName, logStr, traceItem.LogLevel);
            messageQueue.Add(key, () => _queue1(logOption));
        };

        public static void LogInfo(string typeName, string content, string folder=null, string fileName=null)
        {
            string tempFolder = string.IsNullOrEmpty(folder) ? "App" : folder;
            using (var traceItem = new TraceItem(_logEndAction1, LogLevel.Info, typeName, content, tempFolder, fileName))
            {
                //traceItem.Log(content);
            }
        }

        public static void LogWarning(string typeName, string content, string folder = null, string fileName = null)
        {
            string tempFolder = string.IsNullOrEmpty(folder) ? "App" : folder;
            using (var traceItem = new TraceItem(_logEndAction1, LogLevel.Warning, typeName, content, tempFolder, fileName))
            {
                //traceItem.Log(content);
            }
        }

        public static void LogError(string typeName, string content, string folder = null, string fileName = null)
        {
            string tempFolder = string.IsNullOrEmpty(folder) ? "App" : folder;
            using (var traceItem = new TraceItem(_logEndAction1, LogLevel.Error, typeName, content, tempFolder, fileName))
            {
                //traceItem.Log(content);
            }
        }


        public static void SendCustomLog(string typeName, string content)
        {
            if(!Config.IsDebug)
            {
                return;
            }

            using(var traceItem = new TraceItem (_logEndAction, typeName, content))
            {
                //traceItem.Log(content);
            }
        }

        public static void SendApiLog(string url, string returnText)
        {
            if(!Config.IsDebug)
            {
                return;
            }
            using(var traceItem = new TraceItem(_logEndAction, "接口调用"))
            {
                traceItem.Log($"URL:{url}");
                traceItem.Log($"Result:\r\n{returnText}");
            }
        }

        public static void SendApiPostDataLog(string url, string data)
        {
            if(!Config.IsDebug)
            {
                return;
            }

            using(var traceItem = new TraceItem(_logEndAction, "接口调用"))
            {
                traceItem.Log($"URL:{url}");
                traceItem.Log($"Post Data:\r\n{data}");
            }
        }

        public static void BaseExceptionLog(Exception ex, string typeName)
        {
            BaseExceptionLog(new BaseException(ex.Message, ex), typeName);
        }

        public static void BaseExceptionLog(BaseException ex, string typeName)
        {
            if (!Config.IsDebug)
            {
                return;
            }


            using (var traceItem = new TraceItem(_logEndAction, typeName))
            {
                traceItem.Log(ex.GetType().Name);
                traceItem.Log("Message：{0}", ex.Message);
                traceItem.Log("StackTrace：{0}", ex.StackTrace);

                if (ex.InnerException != null)
                {
                    traceItem.Log("InnerException：{0}", ex.InnerException.Message);
                    traceItem.Log("InnerException.StackTrace：{0}", ex.InnerException.StackTrace);
                }

                if (OnBaseExceptionFunc != null)
                {
                    try
                    {
                        OnBaseExceptionFunc(ex);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
