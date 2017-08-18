using System;
using System.Collections.Generic;

namespace Aura.Ast
{
    public sealed class BinaryOperator : IExpression
    {
        public static readonly Dictionary<char, int> Precedence = new Dictionary<char, int>
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2},
            {'%', 2}
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