using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarrenCloudDemos.Lib.MessageQueue
{
    public class MessageQueue
    {
        public static MessageQueueDictionary MessageQueueDictionary = new MessageQueueDictionary();

        private static object MessageQueueSyncLock = new object();
        private static object FlushCacheLock = new object();

        public static string GenerateKey(string name, Type senderType, string identityKey, string actionName)
        {
            return $"Name@{name}||Type@{senderType}||Key@{identityKey}||ActionName{actionName}";
        }

        public string GetCurrentKey()
        {
            lock(MessageQueueSyncLock)
            {
                var value = MessageQueueDictionary.Values.OrderBy(t => t.AddTime).FirstOrDefault();
                if(value==null)
                {
                    return null;
                }
                return value.Key;
            }
        }

        public MessageQueueItem GetItem(string key)
        {
            lock(MessageQueueSyncLock)
            {
                if(MessageQueueDictionary.ContainsKey(key))
                {
                    return MessageQueueDictionary[key];
                }
                return null;
            }
        }

        public MessageQueueItem Add(string key, Action action)
        {
            lock(MessageQueueSyncLock)
            {
                var mqItem = new MessageQueueItem(key, action);
                MessageQueueDictionary.TryAdd(key, mqItem);
                return mqItem;
            }
        }

        public bool Remove(string key, out MessageQueueItem value)
        {
            lock(MessageQueueSyncLock)
            {
                if(MessageQueueDictionary.ContainsKey(key))
                {
                    return MessageQueueDictionary.TryRemove(key, out value);
                }
                else
                {
                    value = null;
                    return true;
                }
            }
        }

        public int GetCount()
        {
            lock(MessageQueueSyncLock)
            {
                return MessageQueueDictionary.Count();
            }
        }

        public static void OperateQueue()
        {
            lock(FlushCacheLock)
            {
                var mq = new MessageQueue();
                var key = mq.GetCurrentKey();
                while(!string.IsNullOrEmpty(key))
                {
                    var mqItem = mq.GetItem(key);
                    mqItem.Action();
                    mq.Remove(key, out MessageQueueItem value);
                    key = mq.GetCurrentKey();
                }
            }
        }
    }
}
