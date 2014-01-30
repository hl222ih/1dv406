using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labb1_2
{
    public class Reciept
    {
        decimal subtotal;

        public decimal DiscountRate { get; private set; }
        public decimal MoneyOff { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal Total { get; private set; }

        public Reciept(decimal subtotal)
        {
            if (subtotal > 0)
            {
                this.subtotal = subtotal;
                Calculate();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Värdet måste vara större än 0");
            }
        }

        private void Calculate()
        {
            Subtotal = subtotal;
            DiscountRate = CalculateDiscountRate(Subtotal);
            MoneyOff = Subtotal * DiscountRate / 100;
            Total = Subtotal - MoneyOff;
        }

        private static decimal CalculateDiscountRate(decimal subtotal)
        {
            var discountRate = 0.0m;
            var amountNoDecimals = Math.Round(subtotal);
            if (amountNoDecimals <= 499)
            {
                discountRate = 0.0m;
            }
            else if (amountNoDecimals <= 999)
            {
                discountRate = 5.0m;
            }
            else if (amountNoDecimals <= 4999)
            {
                discountRate = 10.0m;
            }
            else
            {
                discountRate = 15.0m;
            }

            return discountRate;
        }
    }
}