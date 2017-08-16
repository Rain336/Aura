using System.Collections.Generic;
using System.Linq;
using Aura.Tokens;

namespace Aura
{
    public abstract class LexerBase
    {
        public static readonly HashSet<ITokenMatcher> Matchers = new HashSet<ITokenMatcher>
        {
            new CharTokenMatcher(TokenType.Plus, '+'),
            new CharTokenMatcher(TokenType.Minus, '-'),
            new CharTokenMatcher(TokenType.Times, '*'),
            new CharTokenMatcher(TokenType.Divide, '/'),
            new CharTokenMatcher(TokenType.Modulo, '%'),

            new CharTokenMatcher(TokenType.OpenParentheses, '('),
            new CharTokenMatcher(TokenType.CloseParentheses, ')'),

            new RegexTokenMatcher(TokenType.Decimal, "^[0-9]+"),
            new RegexTokenMatcher(TokenType.Hexadecimal, "^0x[0-9A-Fa-f]+"),
            new RegexTokenMatcher(TokenType.String, "^\".*?\"")
        };

        public int Line;
        public int Column;

        public static bool IsIgnored(char c)
        {
            return " \t\r".Contains(c);
        }

        public abstract List<Token> Lex();
    }
}