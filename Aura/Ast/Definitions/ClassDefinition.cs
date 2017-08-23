using System;
using System.Collections.Generic;
using Aura.Ast.Expressions;
using Aura.Utils;

namespace Aura.Ast.Definitions
{
    public sealed class ClassDefinition : IAstElement
    {
        public IAstElement Parent { get; set; }
        public readonly AccessModifier AccessModifier;
        public readonly string Name;
        public readonly TypeElement Base;
        private readonly List<Modifier> _modifiers = new List<Modifier>();
        private readonly List<FunctionDefinition> _functions = new List<FunctionDefinition>();
        private readonly List<VariableDefinition> _variables = new List<VariableDefinition>();

        public ClassDefinition(AccessModifier accessModifier, string name, TypeElement @base)
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

        public ClassDefinition WithModifiers(Modifier modifier)
        {
            _modifiers.Add(modifier);
            return this;
        }

        public ClassDefinition WithModifiers(params Modifier[] modifiers)
        {
            _modifiers.AddRange(modifiers);
            return this;
        }

        public ClassDefinition WithFunctions(FunctionDefinition def)
        {
            def.Parent = this;
            _functions.Add(def);
            return this;
        }

        public ClassDefinition WithFunctions(params FunctionDefinition[] defs)
        {
            foreach (var def in defs)
                def.Parent = this;
            _functions.AddRange(defs);
            return this;
        }

        public ClassDefinition WithVariables(VariableDefinition var)
        {
            var.Parent = this;
            _variables.Add(var);
            return this;
        }

        public ClassDefinition WithVariables(params VariableDefinition[] vars)
        {
            foreach (var var in vars)
                var.Parent = this;
            _variables.AddRange(vars);
            return this;
        }
    }
}