using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class MaxNumber
    {
        public void Execute()
        {
            int firstValue = 500;
            int secondValue = 600;
            int largerValue = Math.Max(firstValue, secondValue);

            Console.WriteLine(largerValue);
        }
    }
}