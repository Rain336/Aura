using System;
using System.Text.RegularExpressions;

namespace Aura.Tokens
{
    public sealed class RegexTokenMatcher : ITokenMatcher
    {
        public readonly TokenType Type;
        public readonly Regex Regex;

        public RegexTokenMatcher(TokenType type, string regex)
        {
            if (string.IsNullOrEmpty(regex))
                throw new ArgumentNullException(nameof(regex));

            Type = type;
            Regex = new Regex(regex);
        }

        public bool Match(string input)
        {
            return Regex.Match(input).Length == input.Length;
        }

        public bool CreateToken(string buffer, Lexer lexer, out Token token)
        {
            var c = lexer.Peek();
            if (c == -1)
            {
                token = new Token(Type, Regex.Match(buffer).Value);
                return true;
            }
            buffer += (char) c;
            while (Match(buffer))
            {
                lexer.Read();
                c = lexer.Peek();
                if (c == -1)
                {
                    token = new Token(Type, Regex.Match(buffer).Value);
                    return true;
                }
                buffer += (char) c;
            }
            token = new Token(Type, Regex.Match(buffer).Value);
            return true;
        }
    }
}