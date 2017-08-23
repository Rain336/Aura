using System;
using System.Collections.Generic;

namespace Aura.Ast.Expressions
{
    public class TypeElement : IAstElement
    {
        public static readonly HashSet<string> BuildIns = new HashSet<string>
        {
            "void",
            "byte",
            "char",
            "short",
            "int",
            "long",
            "float",
            "double",
            "any",
            "string"
        };

        public IAstElement Parent { get; set; }
        public bool IsBuiltin { get; private set; }
        public readonly string Type;

        public TypeElement(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));

            IsBuiltin = false;
            Type = type;
        }

        public static TypeElement FromString(string str)
        {
            return new TypeElement(str)
            {
                IsBuiltin = BuildIns.Contains(str)
            };
        }

        public static TypeElement Void()
        {
            return new TypeElement("void")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Byte()
        {
            return new TypeElement("byte")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Char()
        {
            return new TypeElement("char")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Short()
        {
            return new TypeElement("short")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Int()
        {
            return new TypeElement("int")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Long()
        {
            return new TypeElement("long")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Float()
        {
            return new TypeElement("float")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Double()
        {
            return new TypeElement("double")
            {
                IsBuiltin = true
            };
        }

        public static TypeElement Any()
        {
            return new TypeElement("any")
            {
                IsBuiltin = true
            };
        }
        
        public static TypeElement String()
        {
            return new TypeElement("string")
            {
                IsBuiltin = true
            };
        }
    }
}