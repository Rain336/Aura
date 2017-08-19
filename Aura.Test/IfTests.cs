using System;
using System.IO;
using Aura.Ast;
using Aura.Parsers;

namespace Aura.Test
{
    public static class IfTests
    {
        [Test("if(1 + 1) {}")]
        public static void SimpleIf()
        {
            var expr = Parse("if(1 + 1) {}");
            Assert.IsType<BinaryOperator>(expr.Condition);
            Assert.AreEqual(0, expr.Block.Statements.Count);
            Assert.AreEqual(null, expr.Else);
            Assert.AreEqual(null, expr.ElseIf);
        }

        [Test("if(1 + 1) {} else {}")]
        public static void IfAndElse()
        {
            var expr = Parse("if(1 + 1) {} else {}");
            Assert.IsType<BinaryOperator>(expr.Condition);
            Assert.AreEqual(0, expr.Block.Statements.Count);
            Assert.AreEqual(0, expr.Else.Statements.Count);
            Assert.AreEqual(null, expr.ElseIf);
        }

        [Test("if(1 + 1) {} else if(2 + 2) {}")]
        public static void IfAndElseIf()
        {
            var expr = Parse("if(1 + 1) {} else if(2 + 2) {}");
            Assert.IsType<BinaryOperator>(expr.Condition);
            Assert.AreEqual(0, expr.Block.Statements.Count);
            Assert.AreEqual(null, expr.Else);
            Assert.IsType<BinaryOperator>(expr.ElseIf.Condition);
            Assert.AreEqual(null, expr.ElseIf.Else);
            Assert.AreEqual(null, expr.ElseIf.ElseIf);
        }

        [Test("if(1 + 1) {} else if(2 + 2) {} else {}")]
        public static void IfElseAndElseIf()
        {
            var expr = Parse("if(1 + 1) {} else if(2 + 2) {} else {}");
            Assert.IsType<BinaryOperator>(expr.Condition);
            Assert.AreEqual(0, expr.Block.Statements.Count);
            Assert.AreEqual(null, expr.Else);
            Assert.IsType<BinaryOperator>(expr.ElseIf.Condition);
            Assert.AreEqual(0, expr.ElseIf.Else.Statements.Count);
            Assert.AreEqual(null, expr.ElseIf.ElseIf);
        }

        private static IfExpression Parse(string input)
        {
            var stack = new TokenStack(new Lexer(new StringReader(input)));
            Console.WriteLine(stack.ToString());
            return new Parser(stack).ParseIf();
        }
    }
}