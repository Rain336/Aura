using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Declarations
{
    public sealed class NamespaceDeclaration
    {
        public readonly List<FunctionDeclaration> Functions = new List<FunctionDeclaration>();
        public readonly List<ClassDeclaration> Classes = new List<ClassDeclaration>();
        public readonly List<InterfaceDeclaration> Interfaces = new List<InterfaceDeclaration>();
    }
}
