using System;
using Aura.Ast.Expressions;

namespace Aura.Ast.Literals
{
    public sealed class StringLiteral : ILiteral
    {
        public IAstElement Parent { get; set; }
        public string Value { get; }

        public TypeElement Type
        {
            get => TypeElement.String(),
            set => throw new InvalidOperationException("Type already set.");
        }

        public StringLiteral(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }
    }
}