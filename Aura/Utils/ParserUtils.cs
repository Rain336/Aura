using Aura.Ast;
using Aura.Tokens;

namespace Aura.Utils
{
    public static class ParserUtils
    {
        public static int GetPrecedence(this Token token)
        {
            return token.IsBinaryOperator() ? BinaryOperator.Precedence[token.Data[0]] : 0;
        }

        public static bool IsBinaryOperator(this Token token)
        {
            return token.Type >= TokenType.Plus || token.Type <= TokenType.Modulo;
        }
    }
}