using System;
using Aura.Ast;
using Aura.Ast.Expressions;
using Aura.Tokens;

namespace Aura.Utils
{
    public static class ParserUtils
    {
        public static int GetPrecedence(this Token token)
        {
            return token.IsBinaryOperator() ? BinaryOperator.Precedence[token.Type] : 0;
        }

        public static bool IsBinaryOperator(this Token token)
        {
            return BinaryOperator.Precedence.ContainsKey(token.Type);
        }

        public static bool IsModifier(this Token token)
        {
            return token.Type <= TokenType.Static && token.Type >= TokenType.Public;
        }

        public static bool IsAccessModifier(this Modifier modifier)
        {
            return modifier >= Modifier.Public && modifier <= Modifier.Internal;
        }

        public static Modifier ToModifier(this Token token)
        {
            switch (token.Type)
            {
                case TokenType.Public:
                    return Modifier.Public;

                case TokenType.Protected:
                    return Modifier.Protected;

                case TokenType.Private:
                    return Modifier.Private;

                case TokenType.Internal:
                    return Modifier.Internal;

                case TokenType.Extern:
                    return Modifier.Extern;

                case TokenType.Unsafe:
                    return Modifier.Unsafe;

                case TokenType.Static:
                    return Modifier.Static;

                default:
                    throw new ArgumentOutOfRangeException(nameof(token), token.Type, "Token is not a Modifier");
            }
        }
    }
}