using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class NoDiscountStrategy : IOfferStrategy
    {
        public string Name => nameof(NoDiscountStrategy);

        public double GetDiscountPercentage()
        {
            return 0;
        }
    }
}
