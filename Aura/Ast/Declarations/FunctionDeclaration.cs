using System.Collections.Generic;
using Aura.Ast.Expressions;
using Aura.Utils;

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
