using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DarrenCloudDemos.Lib.Threads
{
    /// <summary>
    /// 管理线程
    /// </summary>
    public static class ThreadMananger
    {
        public static Dictionary<string, Thread> AsyncThreadCollection = new Dictionary<string, Thread>();
        private static object AsyncThreadCollectionLock = new object();

        public static void RegisterThreads()
        {
            lock(AsyncThreadCollectionLock)
            {
                if(AsyncThreadCollection.Count==0)
                {
                    {
                        MessageQueueManager messageQueueManager = new MessageQueueManager();
                        Thread messageQueueThread = new Thread(messageQueueManager.Run) { Name = "DDMessageQueue" };
                        AsyncThreadCollection.Add(messageQueueThread.Name, messageQueueThread);
                    }

                    AsyncThreadCollection.Values.ToList().ForEach(z => {
                        z.IsBackground = true;
                        z.Start();
                    });
                }
            }
        }
    }
}
