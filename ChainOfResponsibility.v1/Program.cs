using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility.v1
{
    class Program
    {
        static string ConvertToGrade(int testResult)
        {
            if (testResult == 100)
            {
                return "A+";
            }
            else if (testResult >= 90)
            {
                return "A";
            }
            else if (testResult >= 80)
            {
                return "B";
            }
            else if (testResult >= 70)
            {
                return "C";
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        static void Main(string[] args)
        {
            var testResult = 85;

            Console.WriteLine("Your grade is: {0}", ConvertToGrade(testResult));
        }
    }
}
