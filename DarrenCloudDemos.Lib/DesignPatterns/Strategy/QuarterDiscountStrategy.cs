using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class QuarterDiscountStrategy : IOfferStrategy
    {
        public string Name => nameof(QuarterDiscountStrategy);

        public double GetDiscountPercentage()
        {
            return 0.25;
        }
    }
}
