using System.IO;
using Aura.Ast;
using NUnit.Framework;

namespace Aura.Test
{
    [TestFixture]
    public class MathTest
    {
        [Test]
        public void Plus()
        {
            var stack = new TokenStack(new Lexer(new StringReader("5 + 4")));
            TestContext.WriteLine(stack.ToString());
            var parser = new Parser(stack);
            var expr = parser.ParseBinaryOperator();

            Assert.IsInstanceOf<BinaryOperator>(expr);
            var op = (BinaryOperator) expr;
            Assert.AreEqual('+', op.Operator);
            Assert.IsInstanceOf<NumericLiteral>(op.Left);
            Assert.IsInstanceOf<NumericLiteral>(op.Right);
            var result = ((NumericLiteral) op.Left).ToNumber() + ((NumericLiteral) op.Right).ToNumber();
            Assert.AreEqual(9, result);
        }
    }
}