using System;
using Aura.Ast.Expressions;
using Aura.Ast.Statements;

namespace Aura.Ast.Definitions
{
    public sealed class VariableDefinition : IStatement
    {
        public IAstElement Parent { get; set; }
        public readonly bool Immutable;
        public readonly string Name;
        public readonly TypeElement Type;
        public readonly IExpression Expression;

        public VariableDefinition(bool immutable, string name, TypeElement type, IExpression expr)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (expr != null)
                expr.Parent = this;

            type.Parent = this;

            Immutable = immutable;
            Name = name;
            Type = type;
            Expression = expr;
        }
    }
}