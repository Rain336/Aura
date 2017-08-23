using System;
using System.Collections.Generic;
using Aura.Ast.Expressions;
using Aura.Utils;

namespace Aura.Ast.Definitions
{
    public sealed class FunctionDefinition : IAstElement
    {
        public IAstElement Parent { get; set; }
        public readonly AccessModifier AccessModifier;
        public readonly string Name;
        public readonly Block Block;
        private readonly List<Modifier> _modifiers = new List<Modifier>();
        private readonly List<ParameterDefinition> _parameters = new List<ParameterDefinition>();

        public FunctionDefinition(AccessModifier accessModifier, string name, Block block)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if(block == null)
                throw new ArgumentNullException(nameof(block));

            block.Parent = this;
            
            AccessModifier = accessModifier;
            Name = name;
            Block = block;
        }

        public FunctionDefinition WithModifiers(Modifier modifier)
        {
            _modifiers.Add(modifier);
            return this;
        }
        
        public FunctionDefinition WithModifiers(params Modifier[] modifiers)
        {
            _modifiers.AddRange(modifiers);
            return this;
        }
        
        public FunctionDefinition WithParameters(ParameterDefinition parameter)
        {
            parameter.Parent = this;
            _parameters.Add(parameter);
            return this;
        }
        
        public FunctionDefinition WithParameters(params ParameterDefinition[] parameters)
        {
            foreach (var parameter in parameters)
                parameter.Parent = this;
            _parameters.AddRange(parameters);
            return this;
        }
    }
}