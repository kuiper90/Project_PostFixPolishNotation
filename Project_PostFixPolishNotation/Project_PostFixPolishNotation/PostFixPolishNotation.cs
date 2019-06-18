using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_PostFixPolishNotation
{
    public static class PostFixPolishNotation
    {
        public static double EvaluateExpression(string expression)
        {
            string[] expr = expression.Split(' ');
            
            if (expr.Count() < 3)
                throw new Exception("Expression doesn't contain enough elements.");
            return EvalExpr(expr);
        }

        static double EvalExpr(string[] expression)
        {
            IEnumerable<double> result = expression
                .Aggregate(Enumerable.Empty<double>(), (coll, item) =>
                 {
                     return double.TryParse(item, out double number)
                         ? (new[] { number }).Concat(coll)
                         : (new[] { ApplyOperator(item, coll.Take(2)) })
                             .Concat(coll.Skip(2));
                 });
            return result.First();
        }

        private static double ApplyOperator(string op, IEnumerable<double> items)
        {
            var opList = new Dictionary<string, Func<double, double, double>>()
            {
                { "+", (a, b) => a + b },
                { "-", (a, b) => a - b },
                { "*", (a, b) => a * b },
                { "/", (a, b) => a / b }
            };
            bool flag = opList.TryGetValue(op, out Func<double, double, double> valueFunc);
            ValidateItemsAndOperator(items, flag);
            ValidateOperation(items, op);
            return valueFunc(items.Skip(1).First(), items.First());
        }

        private static void ValidateItemsAndOperator(IEnumerable<double> items, bool flag)
        {
            if (!flag || items.Count() != 2)
                throw new Exception("Invalid expression.");
        }

        private static void ValidateOperation(IEnumerable<double> items, string op)
        {
            if (op == "/" && items.First() == 0)
                throw new Exception("Division by zero.");
        }
    }
}