using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.PublishSubscribe
{
    /// <summary>
    /// Publisher
    /// </summary>
    public class Pub
    {
        /// <summary>
        /// 像一个钩子，提供给Subscriber
        /// </summary>
        public Action OnChange { get; set; }

        public void Raise()
        {
            if(OnChange!=null)
            {
                OnChange();
            }
        }
    }
}
