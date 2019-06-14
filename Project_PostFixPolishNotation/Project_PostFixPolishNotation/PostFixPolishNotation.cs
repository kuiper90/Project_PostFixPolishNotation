using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_PostFixPolishNotation
{
    public static class PostFixPolishNotation
    {
        public static decimal EvaluateExpression(string expression)
        {
            string[] expr = expression.Split(' ');
            int exprLen = expr.Count();

            if (exprLen > 2)
                return EvaluateExpr(expr);
            throw new Exception("Expression doesn't contain enough elments.");
        }

        private static decimal EvaluateExpr(string[] expression)
        {
            decimal number;
            Stack<decimal> rez = expression
            .Aggregate(new Stack<decimal>(), (acc, elem) =>
            {

                if (decimal.TryParse(elem, out number))
                {
                    acc.Push(number);
                }
                else
                {
                    switch (elem)
                    {
                        case "*":
                            {
                                acc.Push(acc.Pop() * acc.Pop());
                                break;
                            }
                        case "/":
                            {
                                number = acc.Pop();
                                acc.Push(acc.Pop() / number);
                                break;
                            }
                        case "+":
                            {
                                acc.Push(acc.Pop() + acc.Pop());
                                break;
                            }
                        case "-":
                            {
                                number = acc.Pop();
                                acc.Push(acc.Pop() - number);
                                break;
                            }
                        default:
                            throw new Exception("Invalid expression.");
                    }
                }
                return acc;
            });
            return rez.Pop();
        }
    }
}