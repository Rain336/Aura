using System;
using System.IO;
using Aura.Ast;
using Aura.Parsers;

namespace Aura.Test
{
    public static class ForTests
    {
        [Test("for(var i = 0; i + 1; i + 2) {}")]
        public static void SimpleFor()
        {
            var result = Parse("for(var i = 0; i + 1; i + 2) {}");
            Assert.IsType<ForStatement>(result);
            var stmt = (ForStatement) result;
            Assert.IsType<VariableStatement>(stmt.Section1);
            Assert.IsType<ExpressionStatement>(stmt.Section2);
            Assert.IsType<ExpressionStatement>(stmt.Section3);
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