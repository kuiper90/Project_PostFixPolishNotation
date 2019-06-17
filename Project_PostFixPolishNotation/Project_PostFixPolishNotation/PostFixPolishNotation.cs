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
            
            if (expr.Count() < 3)
                throw new Exception("Expression doesn't contain enough elements.");
            return EvalExpr(expr);
        }

        static decimal EvalExpr(string[] expression)
        {
            IEnumerable<decimal> result = expression
            .Aggregate(Enumerable.Empty<decimal>(), (coll, item) =>
            {
                if (decimal.TryParse(item, out decimal number))
                    coll = coll.Append(number);
                else
                {
                    if (coll.Count() < 2)
                        InvalidExpressionException();
                    decimal nrOne = coll.ElementAt(coll.Count() - 2);
                    decimal nrTwo = coll.ElementAt(coll.Count() - 1);
                    coll = coll
                        .Take(coll.Count() - 2).ToList();
                    coll = coll.Append(ApplyOperator(item, nrOne, nrTwo));
                }
                return coll;
            });
            return result.First();
        }

        private static decimal ApplyOperator(string op, decimal nrOne, decimal nrTwo)
        {
            string[] ops = { "+", "-", "*", "/" };

            Func<decimal, decimal, decimal>[] opList = new Func<decimal, decimal, decimal>[] { (a, b) => a + b,
                                                                                               (a, b) => a - b,
                                                                                               (a, b) => a * b,
                                                                                               (a, b) => a / b  };
            int opIndex = Array.IndexOf(ops, op);

            if (opIndex == -1)
                InvalidExpressionException();
            return opList[opIndex](nrOne, nrTwo);
        }

        private static void InvalidExpressionException()
        {
            throw new Exception("Invalid expression.");
        }
    }
}