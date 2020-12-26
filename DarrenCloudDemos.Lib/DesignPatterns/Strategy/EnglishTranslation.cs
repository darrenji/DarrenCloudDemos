using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class EnglishTranslation
    {
        public static string Translate(string phrase,
                ITranslationStrategy strategy)
        {
            return strategy.Translate(phrase);
        }
    }
}
