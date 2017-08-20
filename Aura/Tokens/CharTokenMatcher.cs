using System;

namespace Aura.Tokens
{
    public sealed class CharTokenMatcher
    {
        public readonly char Char;
        public readonly TokenType Type;

        public CharTokenMatcher(TokenType type, string c)
        {
            if (c == null)
                throw new ArgumentNullException(nameof(c));
            if (c.Length != 1)
                throw new ArgumentException("The Argument must have a lenght of 1.", nameof(c));

            Type = type;
            Char = c[0];
        }

        public CharTokenMatcher(TokenType type, char c)
        {
            Type = type;
            Char = c;
        }
    }
}