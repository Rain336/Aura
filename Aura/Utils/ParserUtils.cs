using Aura.Ast;
using Aura.Tokens;

namespace Aura.Utils
{
    public static class ParserUtils
    {
        public static int GetPrecedence(this Token token)
        {
            return IsBinaryOperator() ? BinaryOperator.Precedence[token.Type] : 0;
        }

        public static bool IsBinaryOperator(this Token token)
        {
            return BinaryOperator.Precedence.ContainsKey(token.Type);
        }
    }
}