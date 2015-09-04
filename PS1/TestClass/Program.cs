using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClass
{
    class Program
    {
        static void Main(string[] args)
        {
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
