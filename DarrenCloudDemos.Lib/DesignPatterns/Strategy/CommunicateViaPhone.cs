using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class CommunicateViaPhone : ICommunicateInterface
    {
        public string Communicate(string destination)
        {
            return "communicating " + destination + " via Phone..";
        }
    }
}
