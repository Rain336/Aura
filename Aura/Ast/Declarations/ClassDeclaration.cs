using Aura.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Declarations
{
    public sealed class ClassDeclaration
    {
        public bool IsActor { get; set; }
        public string Name { get; set; }
        public IType BaseType { get; set; }
        public Modifier AccessModifier { get; set; }
        public readonly List<Modifier> Modifiers = new List<Modifier>();
        public readonly List<FunctionDeclaration> Functions = new List<FunctionDeclaration>();
        public readonly List<VariableDeclaration> Variables = new List<VariableDeclaration>();
    }
}
