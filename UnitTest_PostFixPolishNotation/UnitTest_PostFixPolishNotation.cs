using System;
using Xunit;
using static Project_PostFixPolishNotation.PostFixPolishNotation;

namespace UnitTest_PostFixPolishNotation
{
    public class UnitTest_PostFixPolishNotation
    {
        [Fact]
        public void ValidPostFixExpr1_ShouldReturn_Value()
        {
            string expression = "9 5 4 * - 2 +";
            Assert.True(EvaluateExpression(expression) == -9);
        }

        [Fact]
        public void ValidPostFixExpr2_ShouldReturn_Value()
        {
            string expression = "7 8 + 3 2 + /";
        Assert.True(EvaluateExpression(expression) == 3);
        }

        [Fact]
        public void ValidPostFixExpr3_ShouldReturn_Value()
        {
            string expression = "7 8 + 2 2 + /";
            Assert.True(EvaluateExpression(expression) == 3.75);
        }

        [Fact]
        public void ValidPostFixExpr4_ShouldReturn_Value()
        {
            string expression = "9 5 - 2 7 + *";
            Assert.True(EvaluateExpression(expression) == 36);
        }

        [Fact]
        public void ValidPostFixExpr5_ShouldReturn_Value()
        {
            string expression = "9 5 * 2 7 * +";
            Assert.True(EvaluateExpression(expression) == 59);
        }

        [Fact]
        public void ValidPostFixExpr6_ShouldReturn_Value()
        {
            string expression = "9 5 + 2 + 7 +";
            Assert.True(EvaluateExpression(expression) == 23);
        }

        [Fact]
        public void InvalidPostFixExpr1_ShouldReturn_Value()
        {
            string expression = "9 +";
            var exception = Record.Exception(() => EvaluateExpression(expression));
            Assert.IsType(typeof(Exception), exception);
            Assert.True(exception.Message == "Expression doesn't contain enough elements.");
        }

        [Fact]
        public void InvalidPostFixExpr2_ShouldReturn_Error()
        {
            string expression = "9 7";
            var exception = Record.Exception(() => EvaluateExpression(expression));
            Assert.IsType(typeof(Exception), exception);
            Assert.True(exception.Message == "Expression doesn't contain enough elements.");
        }

        [Fact]
        public void InvalidPostFixExpr3_ShouldReturn_Error()
        {
            string expression = "9";
            var exception = Record.Exception(() => EvaluateExpression(expression));
            Assert.IsType(typeof(Exception), exception);
            Assert.True(exception.Message == "Expression doesn't contain enough elements.");
        }

        [Fact]
        public void InvalidPostFixExpr4_ShouldReturn_Error()
        {
            string expression = "+";
            var exception = Record.Exception(() => EvaluateExpression(expression));
            Assert.IsType(typeof(Exception), exception);
            Assert.True(exception.Message == "Expression doesn't contain enough elements.");
        }

        [Fact]
        public void InvalidPostFixExpr5_ShouldReturn_Error()
        {
            string expression = "";
            var exception = Record.Exception(() => EvaluateExpression(expression));
            Assert.IsType(typeof(Exception), exception);
            Assert.True(exception.Message == "Expression doesn't contain enough elements.");
        }

        [Fact]
        public void InvalidPostFixExpr6_ShouldReturn_Error()
        {
            string expression = "1 2 b 4 a";
            var exception = Record.Exception(() => EvaluateExpression(expression));
            Assert.IsType(typeof(Exception), exception);
            Assert.True(exception.Message == "Invalid expression.");
        }

        [Fact]
        public void InvalidPostFixExpr7_ShouldReturn_Value()
        {
            string expression = "7 8 + 2 2 - /";
            var exception = Record.Exception(() => EvaluateExpression(expression));
            Assert.IsType(typeof(Exception), exception);
            Assert.True(exception.Message == "Division by zero.");
        }
    }
}
