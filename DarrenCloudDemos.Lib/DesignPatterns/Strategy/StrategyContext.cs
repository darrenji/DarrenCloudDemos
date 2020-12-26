using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public class StrategyContext
    {
        double price; // price for some item or air ticket etc.

        /// <summary>
        /// 把策略放在字典集里
        /// </summary>
        Dictionary<string, IOfferStrategy> strategyContext
            = new Dictionary<string, IOfferStrategy>();
        public StrategyContext(double price)
        {
            this.price = price;
            strategyContext.Add(nameof(NoDiscountStrategy),
                    new NoDiscountStrategy());
            strategyContext.Add(nameof(QuarterDiscountStrategy),
                    new QuarterDiscountStrategy());
        }

        public void ApplyStrategy(IOfferStrategy strategy)
        {
            /*
            Currently applyStrategy has simple implementation. 
            You can Context for populating some more information,
            which is required to call a particular operation
            */
            Console.WriteLine("Price before offer :" + price);
            double finalPrice
                = price - (price * strategy.GetDiscountPercentage());
            Console.WriteLine("Price after offer:" + finalPrice);
        }

        public IOfferStrategy GetStrategy(int monthNo)
        {
            /*
            In absence of this Context method, client has to import 
            relevant concrete Strategies everywhere.
            Context acts as single point of contact for the Client 
            to get relevant Strategy
            */
            if (monthNo < 6)
            {
                return strategyContext[nameof(NoDiscountStrategy)];
            }
            else
            {
                return strategyContext[nameof(QuarterDiscountStrategy)];
            }
        }
    }
}
