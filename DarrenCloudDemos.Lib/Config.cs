using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib
{
    public class Config
    {
        private static string _rootPath = null;//调试输出路径
        public static DDSetting DDSetting { get; set; } = new DDSetting();

        public static bool IsDebug
        {
            get
            {
                return DDSetting.IsDebug;
            }
            set
            {
                DDSetting.IsDebug = value;
            }
        }

        public static string RootPath
        {
            get
            {
                if(_rootPath == null)
                {
                    _rootPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                return _rootPath;
            }
            set
            {
                _rootPath = value;
            }
        }

        public static string WebPath { get; set; }
    }
}
