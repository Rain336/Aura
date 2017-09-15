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

        public static readonly SimpleType Float = new SimpleType { Text = "Aura.Float" };
        public static readonly SimpleType Double = new SimpleType { Text = "Aura.Double" };

        public static readonly SimpleType Byte = new SimpleType { Text = "Aura.Byte" };
        public static readonly SimpleType SByte = new SimpleType { Text = "Aura.SByte" };
        public static readonly SimpleType Short = new SimpleType { Text = "Aura.Short" };
        public static readonly SimpleType UShort = new SimpleType { Text = "Aura.UShort" };
        public static readonly SimpleType Int = new SimpleType { Text = "Aura.Int" };
        public static readonly SimpleType UInt = new SimpleType { Text = "Aura.UInt" };
        public static readonly SimpleType Long = new SimpleType { Text = "Aura.Long" };
        public static readonly SimpleType ULong = new SimpleType { Text = "Aura.ULong" };
        public static readonly SimpleType Size = new SimpleType { Text = "Aura.Size" };
        public static readonly SimpleType USize = new SimpleType { Text = "Aura.USize" };
    }

    public sealed class GenericType : IType
    {
        public string Text { get; set; }
        public readonly List<IType> Prameters = new List<IType>();
    }
}
