using System;
using Aura.Ast.Expressions;

namespace Aura.Ast.Definitions
{
    public sealed class ParameterDefinition : IAstElement
    {
        public IAstElement Parent { get; set; }
        public readonly string Name;
        public readonly TypeElement Type;

        public ParameterDefinition(string name, TypeElement type)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if(type == null)
                throw new ArgumentNullException(nameof(type));

            type.Parent = this;
            
            Name = name;
            Type = type;
        }
    }
}