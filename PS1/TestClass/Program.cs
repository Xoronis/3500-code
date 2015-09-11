using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FormulaEvaluator;

namespace TestClass
{
    class Program
    {
        private static Dictionary<string, int> variables;

        static void Main(string[] args)
        {
            variables = new Dictionary<string, int>();
            variables.Add("X6", 6);
            variables.Add("D5", 5);
            variables.Add("SDFEWER95475", 1);
            variables.Add("T0", 9);
            variables.Add("VV78", 2);

            Evaluator.Lookup lookup = variableEvaluator;
            //Console.WriteLine(Evaluator.Evaluate("2 * 3 * )", variableEvaluator));
            Console.WriteLine(Evaluator.Evaluate("( 2 - SDFEWER95475 - T0 + ( 3 - X6 ) * 2)", variableEvaluator));
            Console.WriteLine((2 - 1 - 9 + (3 - 6) * 2));
            Console.WriteLine(Evaluator.Evaluate("( 20 * VV78 + T0 / 3 + X6 ) / 2", variableEvaluator));
            Console.WriteLine((20 * 2 + 9 / 3 + 6) / 2);
            
            Console.Read();
        }

        public static int variableEvaluator(String s)
        {
            if (variables.ContainsKey(s))
            {
                return variables[s];
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
