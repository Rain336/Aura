﻿using Aura.Ast.Declarations;
using System.Collections.Generic;

namespace Aura.Ast
{
    public sealed class CompilationUnit
    {
        public readonly List<ImportDirective> Imports = new List<ImportDirective>();
        public readonly List<NamespaceDeclaration> Namespaces = new List<NamespaceDeclaration>();
        public readonly List<FunctionDeclaration> Functions = new List<FunctionDeclaration>();
        public readonly List<ClassDeclaration> Classes = new List<ClassDeclaration>();
        public readonly List<InterfaceDeclaration> Interfaces = new List<InterfaceDeclaration>();
    }
}
