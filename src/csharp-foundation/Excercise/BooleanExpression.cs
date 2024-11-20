using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class BooleanExpression
    {
        public void Execute()
        {
            // Console.WriteLine("a" == "a");
            // Console.WriteLine("a" == "A");
            // Console.WriteLine(1 == 2);

            // string myValue = "a";
            // Console.WriteLine(myValue == "a");

            // string value1 = "a";
            // string value2= "     A   ";
            // bool match = value1.Trim().ToLower() == value2.Trim().ToLower();
            // Console.WriteLine(match);

            // Console.WriteLine("a" != "a");
            // Console.WriteLine("a" != "A");
            // Console.WriteLine(1 != 2);
            // string myValue = "a";
            // Console.WriteLine(myValue != "a");

            // Console.WriteLine(1 > 2);
            // Console.WriteLine(1 < 2);
            // Console.WriteLine(1 >= 1);
            // Console.WriteLine(1 <= 1);

            string pangram = "The quick brown fox jumps over the dog";
            Console.WriteLine(pangram.Contains("fox"));
            Console.WriteLine(pangram.Contains("dog"));

            Console.WriteLine(pangram.Contains("fox") == false);
            Console.WriteLine(!pangram.Contains("fox"));
        }
    }
}