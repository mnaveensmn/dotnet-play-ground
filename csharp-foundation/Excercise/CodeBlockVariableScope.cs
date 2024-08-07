using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class CodeBlockVariableScope
    {
        public CodeBlockVariableScope()
        {
            Execute();
            Challenge();
        }

        private void Execute()
        {
            // Code sample 1
            int value = 11;

            if (value == 10)
            {
                Console.WriteLine($"{value}");
            }
            else
                Console.WriteLine($"Inside the code block: {value}");

            Console.WriteLine($"Outside the code block: {value}");
        }

        private void Challenge()
        {
            int[] numbers = { 4, 8, 15, 16, 23, 42 };
            int total = 0;
            bool found = false;

            foreach (int number in numbers)
            {
                total += number;

                if (number == 42)
                    found = true;
            }

            if (found)
                Console.WriteLine("Set contains 42");

            Console.WriteLine($"Total: {total}");
        }
    }
}