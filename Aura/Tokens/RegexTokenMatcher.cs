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

        public Token CreateToken(string input)
        {
            return new Token(Type, Regex.Match(input).Value);
        }
    }
}