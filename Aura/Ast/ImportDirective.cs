using System;

namespace Aura.Ast
{
    public sealed class ImportDirective : IAstElement
    {
        public IAstElement Parent { get; set; }
        public readonly string Namespace;

        public ImportDirective(string ns)
        {
            if(string.IsNullOrEmpty(ns))
                throw new ArgumentNullException(nameof(ns));
            
            Namespace = ns;
        }
    }
}