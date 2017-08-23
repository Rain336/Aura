using System;
using Aura.Tokens;

namespace Aura.Ast.Expressions
{
    public sealed class UnaryOperator : IExpression
    {
        public IAstElement Parent { get; set; }
        public TypeElement Type { get; set; }
        public readonly IExpression Right;
        public readonly TokenType Operator;

        public UnaryOperator(IExpression right, TokenType @operator)
        {
            if (right == null)
                throw new ArgumentNullException(nameof(right));
            if (@operator != TokenType.Plus && @operator != TokenType.Minus)
                throw new ArgumentException("Only Plus and Minus are valid!", nameof(@operator));

            right.Parent = this;

            Right = right;
            Operator = @operator;
        }
    }
}