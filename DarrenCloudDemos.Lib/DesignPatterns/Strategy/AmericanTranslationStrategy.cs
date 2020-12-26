using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class AmericanTranslationStrategy : ITranslationStrategy
    {
        public string Translate(string phrase)
        {
            return phrase + ", bro";
        }
    }
}
