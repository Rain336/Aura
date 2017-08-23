using System;
using System.IO;
using Aura.Ast;
using Aura.Ast.Expressions;
using Aura.Parsers;

namespace Aura.Test
{
    public static class MathTests
    {
        [Test("4 + 5")]
        public static void Plus()
        {
            var expr = Parse("4 + 5");
            Assert.IsType<BinaryOperator>(expr);
            var op = (BinaryOperator) expr;
            Assert.IsType<NumericLiteral>(op.Left);
            Assert.IsType<NumericLiteral>(op.Right);
            var result = ((NumericLiteral) op.Right).ToNumber() + ((NumericLiteral) op.Left).ToNumber();
            Assert.AreEqual(9, result);
        }

        [Test("4 + 5 * 9")]
        public static void Precedence()
        {
            var expr = Parse("4 + 5 * 9");
            Assert.IsType<BinaryOperator>(expr);
            var op = (BinaryOperator) expr;
            Assert.IsType<NumericLiteral>(op.Left);
            Assert.IsType<BinaryOperator>(op.Right);
            var rop = (BinaryOperator) op.Right;
            Assert.IsType<NumericLiteral>(rop.Left);
            Assert.IsType<NumericLiteral>(rop.Right);
        }
        
        [Test("(4 + 5) * 9")]
        public static void Precedence2()
        {
            var expr = Parse("(4 + 5) * 9");
            Assert.IsType<BinaryOperator>(expr);
            var op = (BinaryOperator) expr;
            Assert.IsType<BinaryOperator>(op.Left);
            Assert.IsType<NumericLiteral>(op.Right);
            var lop = (BinaryOperator) op.Left;
            Assert.IsType<NumericLiteral>(lop.Left);
            Assert.IsType<NumericLiteral>(lop.Right);
        }

        private static IExpression Parse(string input)
        {
            var stack = new TokenStack(new Lexer(new StringReader(input)));
            Console.WriteLine(stack.ToString());
            return new Parser(stack).ParseExpression();
        }
    }
}