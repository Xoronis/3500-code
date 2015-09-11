using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/// <summary>
/// Author: Jordan Hendley, u0500250
/// </summary>
namespace FormulaEvaluator
{
    /// <summary>
    /// Evaluates arithmetic expressions written using standard infix notation.
    /// For example: 3 * (4 + 5)
    /// </summary>
    public static class Evaluator
    {
        //The different stacks used to hold the values and operators
        private static Stack<double> values = new Stack<double>();
        private static Stack<string> operators = new Stack<string>();
        
        /// <summary>
        /// A delegate used for looking up the value of variables.
        /// </summary>
        /// <param name="v">A string containing a variable name</param>
        /// <returns>The value of the variable</returns>
        public delegate int Lookup(String v);

        /// <summary>
        /// Removes the white space from a string, and then separates the string
        /// into an array of strings. Designed for basic math equations, as it
        /// splits based off of the following operators: * / + - ( )
        /// </summary>
        /// <param name="s">A string containing a simple math equation</param>
        /// <returns>A string array containing the individual operators numbers, and variables present</returns>
        public static string[] splitter (String s)
        {
           s = Regex.Replace(s, @"\s+", "");
           string[] substrings = Regex.Split(s, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
           return substrings;
        }

        /// <summary>
        /// Adds or subtracts the top two values in the values stack depending on the top operator of the operators stack
        /// </summary>
        /// <returns>The result of the math</returns>
        public static double applyMath()
        {
            //In all instances, using the + or - operand requires
            //both values to be pulled from the stack
            if (values.Count < 2)
            {
                throw new ArgumentException();
            }
            //Pop the needed elements from their respective stacks
            double right = values.Pop();
            double left = values.Pop();
            string oper = operators.Pop();
            //The operator will either be a + or a -
            if (oper.Equals("+"))
            {
                return left + right;
            }
            else
            {
                return left - right;
            }
        }
        /// <summary>
        /// Multiplies or divides the top value in the values stack with a number passed in depending on the top operator of the operators stack
        /// </summary>
        /// <param name="expressionValue">One of the values used in the math</param>
        /// <returns>The result of the math</returns>
        public static double applyMath(double expressionValue)
        {
            //Some instances require only one, while others require two.
            //To compensate, expressionValue will have values.Pop as its
            //passed in value whenever necessary
            if (values.Count < 1)
            {
                throw new ArgumentException();
            }
            //Pop and assign elements as necessary
            double left = values.Pop();
            double right = expressionValue;
            string oper = operators.Pop();
            //The operator will be either a * or a /
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
        /// Evaluates a simple mathematical equation that may contain the following operators: * / + - ( ).
        /// </summary>
        /// <param name="exp">The equation to be evaluated</param>
        /// <param name="variableEvaluator">A function used to determine the value of any variables present in the equation</param>
        /// <returns></returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            //Split the mathematical expression into an array
            string[] expression = splitter(exp);
            //Iterate through the expression array
            for(int i = 0; i<expression.Length; i++)
            {
                //Take the element and store it in a new string variable for readability
                string token = expression[i];
                //TODO: Errors
                switch (token)
                {
                    //If the token is a null, ignore
                    case "":
                        break;
                    //If the token is a /, *, or a (, push onto the operators stack
                    case "/":
                    case "*":
                    case "(":
                        operators.Push(token);
                        break;
                    //If the token is a + or -, check to see if there
                    //is an unresolved prior + or - operation
                    //and then push the token onto the operators stack
                    case "+":
                    case "-":
                        //Check to make sure the stack isn't empty before peeking
                        if (operators.Count != 0) {
                           if (operators.Peek().Equals("+") || operators.Peek().Equals("-"))
                           {
                               values.Push(applyMath());
                            }
                        }
                        operators.Push(token);
                        break;
                    //Resolve the inside of the parenthesis, and then pop the left parenthesis
                    //Also resolve any * or / that may require the value of the operations inside
                    case ")":
                        //Check to make sure the stack isn't empty before peeking
                        if (operators.Count != 0)
                        {//Resolve the inside
                            if (operators.Peek().Equals("+") || operators.Peek().Equals("-"))
                            {
                                values.Push(applyMath());
                            }
                        }
                        //If there is no operators on the stack, throw an exception
                        if (operators.Count == 0)
                        {
                            throw new ArgumentException();
                        }
                        //If there is an operator but it's not the left parenthesis,
                        //Throw an exception
                        if (!operators.Pop().Equals("("))
                        {
                            throw new ArgumentException();
                        }
                        //Check to make sure the stack isn't empty before peeking
                        if (operators.Count != 0)
                        {//Resolve any * or / modifying the result of the parenthesis
                            if (operators.Peek().Equals("*") || operators.Peek().Equals("/"))
                            {
                                if (values.Count() < 1)
                                {
                                    throw new ArgumentException();
                                }
                                values.Push(applyMath(values.Pop()));
                            }
                        }
                        break;
                    //If the token is not an operand
                    default:
                        double result;

                        //Check to see if the token is a number. If so,
                        //resolve any outlying * or / operators
                        if (double.TryParse(token, out result))
                        {//Check to make sure the stack isn't empty before peeking
                            if (operators.Count != 0)
                            {//Resolve any outlying * or / operators
                                if (operators.Peek().Equals("*") || operators.Peek().Equals("/"))
                                {
                                    values.Push(applyMath(result));
                                }
                                else
                                {
                                    values.Push(result);
                                }
                            }
                            //It is the first number, so there are no operators
                            //It is simply pushed to values
                            else
                            {
                                values.Push(result);
                            }
                        }
                        //Check for variable
                        else if (Char.IsLetter(token.First())&&token.Length>1)
                        { 
                            //Indicates if we have found where the letters in a variable name have changed to numbers
                            bool foundNumber = false;
                            //Iterate through the token starting at 1 (since we already looked at the first element)
                            for (int count = 1; count<token.Length; count++)
                            {
                                //If we haven't found a number
                                if (!foundNumber)
                                {
                                    //Check to see if the element is a number
                                    if (Char.IsNumber(token[count]))
                                    {
                                        foundNumber = true;
                                    }//Check to see if the element is a letter
                                    else if (!Char.IsLetter(token[count]))
                                    {//Throw error if the element is neither a letter or a number
                                        throw new ArgumentException();
                                    }
                                    else
                                    {
                                        //Continue iterating
                                    }
                                }//We have started using numbers instead of letters
                                else if (Char.IsNumber(token[count]))
                                {
                                    //Continue iterating
                                }
                                else
                                {//Throw error if the element wasn't a number
                                    throw new ArgumentException();
                                }
                            }//Retrieve the value of the variable
                            result = variableEvaluator(token);
                            //Continue as if it was a number
                            //Check to make sure the stack isn't empty before peeking
                            if (operators.Count != 0)
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
                                values.Push(result);
                            }
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                        break;
                }

            }
            //All the math has been calculated, and the result is the last value
            //in the values stack
            if (values.Count == 1 && operators.Count == 0)
            {
                return (int)values.Pop();
            }
            //There is a left over + or - operation that still needs to be calculated
            else if (values.Count == 2 && operators.Count == 1)
            {
                switch (operators.Peek())
                {//Resolve the operation and return the result
                    case "+":
                    case "-":
                        return (int)applyMath();
                    //The remaining operator wasn't a + or -, so throw an exception
                    default:
                        throw new ArgumentException();
                }
            }
            //There are extra operators or values where there shouldn't be
            //Throw an exception
            else
            {
                throw new ArgumentException();
            }
        }

    }
}
