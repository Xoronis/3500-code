using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    /// <summary>
    /// Evaluates arithmetic expressions written using standard infix notation.
    /// For example: 3 * (4 + 5)
    /// </summary>
    public static class Evaluator
    {
        private static Stack<char> values = new Stack<char>();
        private static Stack<char> operators = new Stack<char>();

        public delegate int Lookup(String v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] splitter (String s)
        {
           string[] substrings = Regex.Split(s, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
           return substrings;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="variableEvaluator"></param>
        /// <returns></returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            
            

            return 0;
        }

    }
}
