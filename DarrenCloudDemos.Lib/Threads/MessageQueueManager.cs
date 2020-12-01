using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DarrenCloudDemos.Lib.Threads
{
    public class MessageQueueManager
    {
        private readonly int _sleepMilliSeconds;

        public MessageQueueManager(int sleepMilliSeconds = 500)
        {
            _sleepMilliSeconds = sleepMilliSeconds;
        }
        
        ~MessageQueueManager()
        {
            try
            {
                MessageQueue.MessageQueue.OperateQueue();
            }
            catch (Exception)
            {
            }
        }

        public void Run()
        {
            do
            {
                MessageQueue.MessageQueue.OperateQueue();
                Thread.Sleep(_sleepMilliSeconds);
            } while (true);
        }
    }
}
