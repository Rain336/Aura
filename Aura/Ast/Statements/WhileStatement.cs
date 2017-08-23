using System;
using Aura.Ast.Expressions;

namespace Aura.Ast.Statements
{
    public sealed class WhileStatement : IStatement
    {
        public IAstElement Parent { get; set; }
        public readonly IExpression Expression;
        public readonly Block Block;

        public WhileStatement(IExpression expression, Block block)
        {
            if(expression == null)
                throw new ArgumentNullException(nameof(expression));
            if(block == null)
                throw new ArgumentNullException(nameof(block));

            expression.Parent = this;
            block.Parent = this;
            
            Expression = expression;
            Block = block;
        }
    }
}