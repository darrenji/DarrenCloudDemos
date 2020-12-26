using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class CommunicateViaEmail : ICommunicateInterface
    {
        public string Communicate(string destination)
        {
            return "communicating " + destination + " via Email..";
        }
    }
}
