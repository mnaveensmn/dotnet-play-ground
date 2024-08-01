using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class CodeReadability
    {
        public void Execute()
        {
            string message = "The quick brown fox jumps over the lazy dog.";

            char[] messageCharacters = message.ToCharArray();
            Array.Reverse(messageCharacters);
            
            int count = 0;
            foreach (char i in messageCharacters)
            {
                if (i == 'o') { count++; }
            }

            string reversedMessage = new string(messageCharacters);
            
            Console.WriteLine(reversedMessage);
            Console.WriteLine($"'o' appears {count} times.");
        }
    }
}