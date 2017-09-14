using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast
{
    public interface IType
    {
    }

    public sealed class SimpleType : IType
    {
        public string Text { get; set; }

        public static readonly SimpleType Any = new SimpleType { Text = "Aura.Any" };
        public static readonly SimpleType Void = new SimpleType { Text = "Aura.Void" };
        public static readonly SimpleType String = new SimpleType { Text = "Aura.String" };
    }

    public sealed class GenericType : IType
    {
        public string Text { get; set; }
        public readonly List<IType> Prameters = new List<IType>();
    }
}
