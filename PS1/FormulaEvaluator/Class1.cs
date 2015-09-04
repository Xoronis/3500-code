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
        
        private static Stack<double> values = new Stack<double>();
        private static Stack<string> operators = new Stack<string>();

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
        public static double applyMath()
        {
            double right = values.Pop();
            double left = values.Pop();
            string oper = operators.Pop();
            if (oper.Equals("+"))
            {
                return left + right;
            }
            else
            {
                return left - right;
            }
        }

        public static double applyMath(double expressionValue)
        {
            double left = values.Pop();
            double right = expressionValue;
            string oper = operators.Pop();
            
            if (oper.Equals("*"))
            {
                return left * right;
            }
            else
            {
                return left / right;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="variableEvaluator"></param>
        /// <returns></returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            
            string[] expression = splitter(exp);
            if (expression.Length == 1)
            {
                double result;
                if (double.TryParse(expression[0], out result))
                {
                    return (int)result;
                }
                else
                {
                    //TODO: Error
                }
            }
            for(int i = 0; i<expression.Length; i++)
            {
                //TODO: Errors
                switch (expression[i])
                {
                    //case Double.
                    case "/":
                    case "*":
                    case "(":
                        operators.Push(expression[i]);
                        break;
                    case "+":
                    case "-":
                        if (operators.Peek().Equals("+")|| operators.Peek().Equals("-"))
                        {
                            values.Push(applyMath());
                        }
                        operators.Push(expression[i]);
                        break;
                    case ")":
                        if (operators.Peek().Equals("+") || operators.Peek().Equals("-"))
                        {
                            values.Push(applyMath());
                        }
                        string close = operators.Pop();
                        if (operators.Peek().Equals("*") || operators.Peek().Equals("/"))
                        {
                            values.Push(applyMath(values.Pop()));
                        }
                        break;
                    default:
                        double result;
                        if (double.TryParse(expression[i], out result))
                        {
                            if (operators.Peek().Equals("*") || operators.Peek().Equals("/"))
                            {
                                values.Push(applyMath(result));
                            }
                            else
                            {
                                values.Push(result);
                            }
                        }
                        else
                        {
                            //TODO: Error
                        }
                        break;
                }

            }
            

            return 0;
        }

    }
}
