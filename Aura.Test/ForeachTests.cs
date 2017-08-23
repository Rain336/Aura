using System;
using System.IO;
using Aura.Ast;
using Aura.Ast.Expressions;
using Aura.Ast.Statements;
using Aura.Parsers;

namespace Aura.Test
{
    public static class ForeachTests
    {
        [Test("for(i in 1 + 1) {}")]
        public static void SimpleForeach()
        {
            var result = Parse("for(i in 1 + 1) {}");
            Assert.IsType<ForeachStatement>(result);
            var stmt = (ForeachStatement) result;
            Assert.AreEqual("i", stmt.Variable);
            Assert.IsType<BinaryOperator>(stmt.Expression);
            Assert.AreEqual(0, stmt.Block.Statements.Count);
        }

        private static IStatement Parse(string input)
        {
            var stack = new TokenStack(new Lexer(new StringReader(input)));
            Console.WriteLine(stack.ToString());
            return new Parser(stack).ParseFor();
        }
    }
}