using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_PostFixPolishNotation
{
    public static class PostFixPolishNotation
    {
        static readonly string[] ops = { "+", "-", "*", "/" };

        public static decimal EvaluateExpression(string expression)
        {
            string[] expr = expression.Split(' ');
            
            if (expr.Count() > 2)
                return EvalExpr(expr);
            throw new Exception("Expression doesn't contain enough elements.");
        }

        static decimal EvalExpr(string[] expression)
        {
            List<decimal> result = expression
                .Aggregate(new List<decimal>(), (coll, item) => {
                    if (Array.IndexOf(ops, item) == -1)
                    {
                        if (decimal.TryParse(item, out decimal number) == false)
                            throw new Exception("Invalid expression.");
                        coll.Add(number);
                    }
                    else
                    {
                        if (coll.Count() < 2)
                            throw new Exception("Invalid expression.");
                        decimal nrOne = coll[coll.Count - 2];
                        decimal nrTwo = coll[coll.Count - 1];                        
                        coll = coll
                            .Take(coll.Count - 2).ToList();
                        coll.Add(ApplyOperator(item, nrOne, nrTwo));
                    }
                    return coll;
                });
            return result[0];
        }

        private static decimal ApplyOperator(string op, decimal nrOne, decimal nrTwo)
        {
            Func<decimal, decimal, decimal>[] opList = new Func<decimal, decimal, decimal>[] { (a, b) => a + b,
                                                                                               (a, b) => a - b,
                                                                                               (a, b) => a * b,
                                                                                               (a, b) => a / b  };
            int opIndex = Array.IndexOf(ops, op);

            return opList[opIndex](nrOne, nrTwo);
        }
    }
}