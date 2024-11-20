using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_foundation.Excercise
{
    public class SwitchStatement
    {
        public SwitchStatement()
        {
            Execute();
        }

        public void Execute()
        {
            int employeeLevel = 100;
            string employeeName = "John Smith";
            string title = "";

            switch (employeeLevel){
                case 100:
                    title = "Junior Associate";
                    break;
                case 200:
                    title = "Senior Associate";
                    break;
                default:
                    title = "Associate";
                    break;
            }

            Console.WriteLine($"{employeeName}, {title}");
        }
    }


}