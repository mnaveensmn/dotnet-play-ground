using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class CoinFlip
    {
        public CoinFlip()
        {
            Execute();
        }

        private void Execute()
        {
            var random = new Random();
            int number = random.Next(0, 2);
            Console.WriteLine("Coin flipped!");
            Console.WriteLine($"It is {(number == 0 ? "Head" : "Tail")}!!");
        }
    }
}