using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.MessageQueue
{
    public class MessageQueueDictionary : ConcurrentDictionary<string, MessageQueueItem>
    {
        public MessageQueueDictionary() : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
