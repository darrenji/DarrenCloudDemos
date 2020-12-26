using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.PublishSubscribe
{
    public class Pub1
    {
        public event Action OnChange = delegate { };

        public void Raise()
        {
            OnChange();
        }
    }
}
