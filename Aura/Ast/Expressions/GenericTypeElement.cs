using System.Collections.Generic;

namespace Aura.Ast.Expressions
{
    public sealed class GenericTypeElement : TypeElement
    {
        private readonly List<TypeElement> _generics = new List<TypeElement>();

        public GenericTypeElement(string type) : base(type)
        {
        }

        public GenericTypeElement WithGenerics(TypeElement elem)
        {
            elem.Parent = this;
            _generics.Add(elem);
            return this;
        }

        public GenericTypeElement WithGenerics(params TypeElement[] elems)
        {
            foreach (var elem in elems)
                elem.Parent = this;
            _generics.AddRange(elems);
            return this;
        }
    }
}