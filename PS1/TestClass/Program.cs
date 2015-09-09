using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestClass
{
    class Program
    {
        static void Main(string[] args)
        {
            string test2 = "   \t\t   2\t3";
            Console.WriteLine(test2);
            test2 = Regex.Replace(test2, @"\s+", "");
            Console.WriteLine(test2);
            string test = "*";
            double result;
            switch (test)
            {
                case "*":
                case "/":
                    Console.WriteLine("Success");
                    break;
               // case double.TryParse(test, out result):
                default:
                    Console.WriteLine("Failure");
                    break;
            }
        }
    }
}
