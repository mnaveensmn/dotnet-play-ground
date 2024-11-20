using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class ConditionalOperator
    {
        public ConditionalOperator() {
            Execute();
        }

        private void Execute() {

            int purchasedAmount = 500;
            int discount = purchasedAmount > 1000 ? 100 : 50;
            Console.WriteLine($"Discount: {discount}");

        }
    }
}