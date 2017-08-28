using System;
using System.Collections.Generic;
using Aura.Ast.Expressions;
using Aura.Utils;

namespace Aura.Ast.Definitions
{
    public sealed class FunctionDefinition : FunctionSignature
    {
        public readonly Modifier AccessModifier;
        public readonly Block Block;
        private readonly List<Modifier> _modifiers = new List<Modifier>();

        public FunctionDefinition(Modifier accessModifier, string name, Block block) : base(name)
        {
            if(block == null)
                throw new ArgumentNullException(nameof(block));

            block.Parent = this;
            
            AccessModifier = accessModifier;
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
    }
}