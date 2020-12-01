using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.Trace
{
    public class LogOption
    {
        public string Folder { get; set; }
        public string FileName { get; set; }

        public string LogStr { get; set; }

        public LogLevel LogLevel { get; set; }

        public LogOption(string folder, string fileName, string logStr, LogLevel logLevel)
        {
            Folder = folder;
            FileName = fileName;
            LogStr = logStr;
            LogLevel = logLevel;
        }
    }

    public enum LogLevel
    {
        Debug=0,
        Info=1,
        Warning=2,
        Error=3
    }
}
