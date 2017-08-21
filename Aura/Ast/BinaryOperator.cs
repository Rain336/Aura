using System;
using System.Collections.Generic;
using Aura.Tokens;

namespace Aura.Ast
{
    public sealed class BinaryOperator : IExpression
    {
        public static readonly Dictionary<TokenType, int> Precedence = new Dictionary<TokenType, int>
        {
            {TokenType.Plus, 1},
            {TokenType.Minus, 1},
            {TokenType.Times, 2},
            {TokenType.Divide, 2},
            {TokenType.Modulo, 2}
        };

        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public char Operator { get; set; }

        public long Calculate()
        {
            long l = 0;
            if (Left is BinaryOperator)
                l = ((BinaryOperator) Left).Calculate();
            else if (Left is NumericLiteral)
                l = ((NumericLiteral) Left).ToNumber();

            long r = 0;
            if (Right is BinaryOperator)
                r = ((BinaryOperator) Right).Calculate();
            else if (Right is NumericLiteral)
                r = ((NumericLiteral) Right).ToNumber();

            switch (Operator)
            {
                case '+':
                    return l + r;

                case '-':
                    return l - r;

                case '*':
                    return l * r;

                case '/':
                    return l / r;

                case '%':
                    return l % r;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}