using System;
using Aura.Ast.Expressions;

namespace Aura.Ast.Statements
{
    public sealed class ExpressionStatement : IStatement
    {
        public IAstElement Parent { get; set; }
        public readonly IExpression Expression;

        public ExpressionStatement(IExpression expression)
        {
            if(expression == null)
                throw new ArgumentNullException(nameof(expression));

            expression.Parent = this;
            
            Expression = expression;
        }
    }
}