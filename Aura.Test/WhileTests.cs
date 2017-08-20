using System;
using System.IO;
using Aura.Ast;
using Aura.Parsers;

namespace Aura.Test
{
    public static class WhileTests
    {
        [Test("while(1 + 1) {}")]
        public static void SimpleWhile()
        {
            var stmt = Parse("while(1 + 1) {}");
            Assert.IsType<BinaryOperator>(stmt.Expression);
            Assert.AreEqual(0, stmt.Block.Statements.Count);
        }

        private static WhileStatment Parse(string input)
        {
            var stack = new TokenStack(new Lexer(new StringReader(input)));
            Console.WriteLine(stack.ToString());
            return new Parser(stack).ParseWhile();
        }
    }
}