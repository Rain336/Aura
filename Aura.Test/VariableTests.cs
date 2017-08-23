using System;
using System.IO;
using Aura.Ast;
using Aura.Ast.Expressions;
using Aura.Ast.Statements;
using Aura.Parsers;

namespace Aura.Test
{
    public static class VariableTests
    {
        [Test("var x: Test")]
        public static void NoAsign()
        {
            var stmt = Parse("var x: Test");
            Assert.IsType<VariableStatement>(stmt);
            var variable = (VariableStatement) stmt;
            Assert.AreEqual(false, variable.Immutable);
            Assert.AreEqual("x", variable.Name);
            Assert.AreEqual("Test", variable.Type);
            Assert.AreEqual(null, variable.Assignment);
        }

        [Test("var x = 4 * 5")]
        public static void NoType()
        {
            var stmt = Parse("var x = 4 * 5");
            Assert.IsType<VariableStatement>(stmt);
            var variable = (VariableStatement) stmt;
            Assert.AreEqual(false, variable.Immutable);
            Assert.AreEqual("x", variable.Name);
            Assert.AreEqual(null, variable.Type);
            Assert.IsType<BinaryOperator>(variable.Assignment);
        }

        [Test("var x: int = 4 * 5")]
        public static void Full()
        {
            var stmt = Parse("var x: int = 4 * 5");
            Assert.IsType<VariableStatement>(stmt);
            var variable = (VariableStatement) stmt;
            Assert.AreEqual(false, variable.Immutable);
            Assert.AreEqual("x", variable.Name);
            Assert.AreEqual("int", variable.Type);
            Assert.IsType<BinaryOperator>(variable.Assignment);
        }

        private static IStatement Parse(string input)
        {
            var stack = new TokenStack(new Lexer(new StringReader(input)));
            Console.WriteLine(stack.ToString());
            return new Parser(stack).ParseStatement();
        }
    }
}