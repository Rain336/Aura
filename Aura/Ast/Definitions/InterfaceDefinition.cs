using System;
using System.Collections.Generic;
using Aura.Ast.Expressions;
using Aura.Utils;

namespace Aura.Ast.Definitions
{
    public sealed class InterfaceDefinition : IAstElement
    {
        public IAstElement Parent { get; set; }
        public readonly Modifier AccessModifier;
        public readonly string Name;
        public readonly TypeElement Base;
        private readonly List<FunctionSignature> _signatures = new List<FunctionSignature>();
        private readonly List<Modifier> _modifiers = new List<Modifier>();

        public InterfaceDefinition(Modifier accessModifier, string name, TypeElement @base)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (@base == null)
                throw new ArgumentNullException(nameof(@base));

            @base.Parent = this;

            AccessModifier = accessModifier;
            Name = name;
            Base = @base;
        }

        public InterfaceDefinition WithSignatures(FunctionSignature signature)
        {
            signature.Parent = this;
            _signatures.Add(signature);
            return this;
        }

        public InterfaceDefinition WithSignatures(params FunctionSignature[] signatures)
        {
            foreach (var signature in signatures)
                signature.Parent = this;
            _signatures.AddRange(signatures);
            return this;
        }
        
        public InterfaceDefinition WithModifiers(Modifier modifier)
        {
            _modifiers.Add(modifier);
            return this;
        }

        public InterfaceDefinition WithModifiers(params Modifier[] modifiers)
        {
            _modifiers.AddRange(modifiers);
            return this;
        }
    }
}