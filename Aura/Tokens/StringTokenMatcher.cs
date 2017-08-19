using System;

namespace Aura.Tokens
{
    public sealed class StringTokenMatcher : ITokenMatcher
    {
        public readonly TokenType Type;
        public readonly string Matcher;

        public StringTokenMatcher(TokenType type, string matcher)
        {
            if (string.IsNullOrWhiteSpace(matcher))
                throw new ArgumentNullException(nameof(matcher));

            Type = type;
            Matcher = matcher;
        }

        public bool Match(string input)
        {
            return Matcher.StartsWith(input);
        }

        public bool CreateToken(string buffer, Lexer lexer, out Token token)
        {
            if (buffer == Matcher)
            {
                token = new Token(Type, Matcher);
                return true;
            }

            var c = lexer.Peek();
            if (c == -1)
            {
                token = default(Token);
                return false;
            }

            while (Match(buffer += (char) c))
            {
                lexer.Read();
                c = lexer.Peek();
                if (c != -1) continue;

                token = default(Token);
                return false;
            }

            if (buffer.Length - 1 != Matcher.Length)
            {
                token = default(Token);
                return false;
            }

            token = new Token(Type, Matcher);
            return true;
        }
    }
}