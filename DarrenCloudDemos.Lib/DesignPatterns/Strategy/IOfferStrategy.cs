using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.Strategy
{
    public interface IOfferStrategy
    {
        string Name { get; }
        double GetDiscountPercentage();
    }
}
