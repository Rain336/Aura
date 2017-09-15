using System;

namespace Aura.Ast.Expressions
{
    public sealed class NumberLiteral : IExpression
    {
        public string Text { get; }
        public IType ResultType { get; }

        public NumberLiteral(string data, bool hexa)
        {
            Text = data;
            ResultType = GetType(data, hexa);
        }

        public static IType GetType(string data, bool hexa)
        {
            if (hexa)
            {
                switch (Convert.ToInt64(data.Substring(2), 16))
                {
                    case long i when i < byte.MaxValue:
                        return SimpleType.Byte;

                    case long i when i < short.MaxValue:
                        return SimpleType.Short;

                    case long i when i < int.MaxValue:
                        return SimpleType.Int;

                    default:
                        return SimpleType.Long;
                }
            }

            if (data.Contains("."))
            {
                var f = Convert.ToDouble(data);
                if (f < float.MaxValue)
                    return SimpleType.Float;
                else
                    return SimpleType.Double;
            }

            switch (Convert.ToInt64(data))
            {
                case long i when i < byte.MaxValue:
                    return SimpleType.Byte;

                case long i when i < short.MaxValue:
                    return SimpleType.Short;

                case long i when i < int.MaxValue:
                    return SimpleType.Int;

                default:
                    return SimpleType.Long;
            }
        }
    }
}
