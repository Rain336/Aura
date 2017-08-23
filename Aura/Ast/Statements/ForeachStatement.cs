using System;
using Aura.Ast.Expressions;

namespace Aura.Ast.Statements
{
    public sealed class ForeachStatement : IStatement
    {
        public IAstElement Parent { get; set; }
        public readonly string Variable;
        public readonly IExpression Expression;
        public readonly Block Block;

        public ForeachStatement(string variable, IExpression expression, Block block)
        {
            if(string.IsNullOrEmpty(variable))
                throw new ArgumentNullException(nameof(variable));
            if(expression == null)
                throw new ArgumentNullException(nameof(expression));
            if(block == null)
                throw new ArgumentNullException(nameof(block));

            expression.Parent = this;
            block.Parent = this;
            
            Variable = variable;
            Expression = expression;
            Block = block;
        }
    }
}