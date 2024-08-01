using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class WarehouseInventory
    {
        public void Validate()
        {
            string[] names = { "Rowena", "Robin", "Bao" };
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }

            int[] inventory = { 200, 450, 700, 175, 250 };
            int sum = 0;
            int bin = 0;
            foreach (int items in inventory)
            {
                sum += items;
                bin++;
                Console.WriteLine($"Bin {bin} = {items} items (Running total: {sum})");
            }
            Console.WriteLine($"We have {sum} items in inventory.");

        }
    }
}