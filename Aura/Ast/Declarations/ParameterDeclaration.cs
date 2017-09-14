using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Declarations
{
    public sealed class ParameterDeclaration
    {
        public string Name { get; set; }
        public IType Type { get; set; }
    }
}
