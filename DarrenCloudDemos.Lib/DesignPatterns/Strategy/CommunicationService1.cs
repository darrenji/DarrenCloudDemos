using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class CommunicationService1
    {
        private Func<string, string> communcationMeans;
        public void SetCommuncationMeans(Func<string, string> communcationMeans)
        {
            this.communcationMeans = communcationMeans;
        }
        public void Communicate(string destination)
        {
            var communicate = communcationMeans(destination);
            Console.WriteLine(communicate);
        }
    }
}
