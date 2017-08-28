using System;
using System.Collections.Generic;
using Aura.Ast.Definitions;

namespace Aura.Ast
{
    public class FunctionSignature : IAstElement
    {
        public IAstElement Parent { get; set; }
        public readonly string Name;
        private readonly List<ParameterDefinition> _parameters = new List<ParameterDefinition>();

        public FunctionSignature(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            
            Name = name;
        }
                
        public FunctionSignature WithParameters(ParameterDefinition parameter)
        {
            parameter.Parent = this;
            _parameters.Add(parameter);
            return this;
        }
        
        public FunctionSignature WithParameters(params ParameterDefinition[] parameters)
        {
            foreach (var parameter in parameters)
                parameter.Parent = this;
            _parameters.AddRange(parameters);
            return this;
        }
    }
}