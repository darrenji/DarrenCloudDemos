using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class CommunicationService
    {
        private ICommunicateInterface communcationMeans;
        public void SetCommuncationMeans(ICommunicateInterface communcationMeans)
        {
            this.communcationMeans = communcationMeans;
        }
        public void Communicate(string destination)
        {
            var communicate = communcationMeans.Communicate(destination);
            Console.WriteLine(communicate);
        }
    }
}
