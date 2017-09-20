using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aura.Ast.Expressions;

namespace Aura.Test
{
    [TestClass]
    public class WhileTest
    {
        [TestMethod]
        public void SimpleWhile()
        {
            var stmt = TestUtils.CreateParser("while(1 + 1) {}").ParseWhile();
            Assert.AreEqual(0, stmt.Block.Statments.Count);
            Assert.IsInstanceOfType(stmt.Condition, typeof(BinaryOperator));
        }
    }
}
