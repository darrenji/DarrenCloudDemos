using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.PublishSubscribe
{
    public class Pub3
    {
        public event EventHandler<MyEventArgs> OnChange = delegate { };

        public void Raise()
        {
            //Invoke OnChange Action
            //Lets pass MyEventArgs object with some random value
            OnChange(this, new MyEventArgs(33));
        }
    }

    public class MyEventArgs : EventArgs
    {
        public int Value { get; set; }

        public MyEventArgs(int value)
        {
            Value = value;
        }
    }
}
