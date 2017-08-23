using System;
using System.Collections.Generic;
using Aura.Tokens;

namespace Aura.Ast.Expressions
{
    public sealed class BinaryOperator : IExpression
    {
        public static readonly Dictionary<TokenType, int> Precedence = new Dictionary<TokenType, int>
        {
            {TokenType.Plus, 1},
            {TokenType.Minus, 1},
            {TokenType.Times, 2},
            {TokenType.Divide, 2},
            {TokenType.Modulo, 2},
        };

        public IAstElement Parent { get; set; }
        public TypeElement Type { get; set; }
        public readonly IExpression Left;
        public readonly TokenType Operator;
        public readonly IExpression Right;

        public BinaryOperator(IExpression left, TokenType @operator, IExpression right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));
            if (!Precedence.ContainsKey(@operator))
                throw new ArgumentException(
                    $"Unsupported Operator '{Enum.GetName(typeof(TokenType), @operator)}'", nameof(@operator));
            if (right == null)
                throw new ArgumentNullException(nameof(right));

            left.Parent = this;
            right.Parent = this;

            Left = left;
            Operator = @operator;
            Right = right;
        }
    }
}