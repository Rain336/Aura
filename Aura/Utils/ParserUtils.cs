using System;
using Aura.Ast;
using Aura.Ast.Expressions;
using Aura.Tokens;
using System.Collections.Generic;

namespace Aura.Utils
{
    public static class ParserUtils
    {
        private static readonly Dictionary<TokenType, int> Precedence = new Dictionary<TokenType, int>
        {
            { TokenType.Plus, 10 },
            { TokenType.Minus, 10 },
            { TokenType.Times, 20 },
            { TokenType.Divide, 20 },
            { TokenType.Modulo, 20 },
        };

        public static int GetPrecedence(this Token token)
        {
            return token.IsBinaryOperator() ? Precedence[token.Type] : 0;
        }

        public static bool IsBinaryOperator(this Token token)
        {
            return Precedence.ContainsKey(token.Type);
        }

        public static bool IsModifier(this Token token)
        {
            return token.Type <= TokenType.Static && token.Type >= TokenType.Public;
        }

        public static bool IsAccessModifier(this Modifier modifier)
        {
            return modifier >= Modifier.Public && modifier <= Modifier.Private;
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