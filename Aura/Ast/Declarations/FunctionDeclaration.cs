using Aura.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Declarations
{
    public sealed class FunctionDeclaration
    {
        public string Name { get; set; }
        public Modifier AccessModifier { get; set; }
        public IType ReturnType { get; set; }
        public Block Block { get; set; }
        public readonly List<ParameterDeclaration> Parameters = new List<ParameterDeclaration>();
        public readonly List<Modifier> Modifiers = new List<Modifier>();
    }
}
