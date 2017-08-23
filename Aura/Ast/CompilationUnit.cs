using System;
using System.Collections.Generic;
using Aura.Ast.Definitions;

namespace Aura.Ast
{
    public sealed class CompilationUnit : IAstElement
    {
        public IAstElement Parent
        {
            get => throw new InvalidOperationException("Root Element.");
            set => throw new InvalidOperationException("Root Element.");
        }

        private readonly List<ImportDirective> _imports = new List<ImportDirective>();
        private readonly List<FunctionDefinition> _functions = new List<FunctionDefinition>();
        private readonly List<ClassDefinition> _classes = new List<ClassDefinition>();

        public CompilationUnit WithImports(ImportDirective directive)
        {
            directive.Parent = this;
            _imports.Add(directive);
            return this;
        }

        public CompilationUnit WithImports(params ImportDirective[] directives)
        {
            foreach (var directive in directives)
                directive.Parent = this;
            _imports.AddRange(directives);
            return this;
        }

        public CompilationUnit WithFunctions(FunctionDefinition def)
        {
            def.Parent = this;
            _functions.Add(def);
            return this;
        }

        public CompilationUnit WithFunctions(params FunctionDefinition[] defs)
        {
            foreach (var def in defs)
                def.Parent = this;
            _functions.AddRange(defs);
            return this;
        }

        public CompilationUnit WithClasses(ClassDefinition cls)
        {
            cls.Parent = this;
            _classes.Add(cls);
            return this;
        }

        public CompilationUnit WithClasses(params ClassDefinition[] classes)
        {
            foreach (var cls in classes)
                cls.Parent = this;
            _classes.AddRange(classes);
            return this;
        }
    }
}