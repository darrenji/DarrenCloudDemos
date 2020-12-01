using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.MessageQueue
{
    public class MessageQueueItem
    {
        public string Key { get; set; }
        public Action Action { get; set; }
        public DateTimeOffset AddTime { get; set; }
        public string Description { get; set; }

        public MessageQueueItem(string key, Action action, string description=null)
        {
            Key = key;
            Action = action;
            Description = description;
            AddTime = SystemTime.Now;
        }
    }
}
