using Aura.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Declarations
{
    public sealed class InterfaceDeclaration
    {
        public string Name { get; set; }
        public IType BaseType { get; set; }
        public Modifier AccessModifier { get; set; }
        public readonly List<Modifier> Modifiers = new List<Modifier>();
        public readonly List<Entry> Entries = new List<Entry>();

        public sealed class Entry
        {
            public string Name { get; set; }
            public IType ReturnType { get; set; }
            public readonly List<ParameterDeclaration> Parameters = new List<ParameterDeclaration>();
        }
    }
}
