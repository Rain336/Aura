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
    }
}