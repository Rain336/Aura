using System;

namespace Aura.Tokens
{
    public sealed class StringTokenMatcher : ITokenMatcher
    {
        public readonly TokenType Type;
        public readonly string Matcher;

        public StringTokenMatcher(TokenType type, string matcher)
        {
            if(string.IsNullOrWhiteSpace(matcher))
                throw new ArgumentNullException(nameof(matcher));
            
            Type = type;
            Matcher = matcher;
        }

        public bool Match(string input)
        {
            return Matcher == input;
        }

        public Token CreateToken(string input)
        {
            return new Token(Type, Matcher);
        }
    }
}