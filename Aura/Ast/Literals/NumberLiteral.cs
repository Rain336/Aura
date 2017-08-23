using System;
using Aura.Ast.Expressions;

namespace Aura.Ast.Literals
{
    public sealed class NumberLiteral : ILiteral
    {
        public IAstElement Parent { get; set; }
        public TypeElement Type { get; set; }
        public string Value { get; }
        public readonly bool IsHexadecimal;

        public NumberLiteral(string value, bool hexadecimal)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
            IsHexadecimal = hexadecimal;
        }
    }
}