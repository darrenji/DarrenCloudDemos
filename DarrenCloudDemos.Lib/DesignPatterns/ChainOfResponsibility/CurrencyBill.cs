using System;
using System.Collections.Generic;
using System.Text;

namespace DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility
{
    public class CurrencyBill
    {
        private static readonly CurrencyBill Zero;//下一个
        private CurrencyBill next = CurrencyBill.Zero; //sets default handler instead of null object本次

        public int Denomination { get; }
        public int Quantity { get; }

        //A static constructor that initializes static Zero property
        //This property is used as default next handler instead of a null object
        static CurrencyBill()//静态构造函数的用法
        {
            Zero = new ZeroCurrencyBill();
        }

        //CurrencyBill constructor that set the denomination value and quantity
        public CurrencyBill(int denomination, int quantity)
        {
            Denomination = denomination;
            Quantity = quantity;
        }

        //Method that processes the request or passes it to the next handler
        public virtual bool DispenseRequest(int amount)
        {
            if (amount >= Denomination)
            {
                var num = Quantity;
                var remainder = amount;
                while (remainder >= Denomination && num > 0)
                {
                    remainder -= Denomination;
                    num--;
                }

                if (remainder != 0)
                {
                    return next.DispenseRequest(remainder);
                }

                return true;
            }
            else
            {
                return next.DispenseRequest(amount);
            }

        }

        //Method that set next handler in the pipeline
        public CurrencyBill RegisterNext(CurrencyBill currencyBill)
        {
            next = currencyBill;
            return next;
        }

        //Use to set static Zero property
        //Will always return false at it cannot process any given amount.
        public class ZeroCurrencyBill : CurrencyBill
        {
            public ZeroCurrencyBill() : base(0, 0)
            {
            }

            public override bool DispenseRequest(int amount)
            {
                return false;
            }
        }
    }
}
