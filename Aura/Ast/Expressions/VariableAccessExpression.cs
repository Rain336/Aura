using System;

namespace Aura.Ast.Expressions
{
    public sealed class VariableAccessExpression : IExpression
    {
        public IAstElement Parent { get; set; }
        public TypeElement Type { get; set; }
        public readonly string Name;

        public VariableAccessExpression(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            
            Name = name;
        }
    }
}