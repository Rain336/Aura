using System;

namespace Aura.Ast
{
    public sealed class NumericLiteral : ILiteral
    {
        public string Value { get; set; }
        public bool Hexadecimal { get; set; }

        public long ToNumber()
        {
            return Hexadecimal ? Convert.ToInt64(Value, 16) : Convert.ToInt64(Value);
        }
    }
}