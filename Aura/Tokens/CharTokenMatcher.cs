using System;

namespace Aura.Tokens
{
    public sealed class CharTokenMatcher : ITokenMatcher
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

        public bool Match(string input)
        {
            return input.Length == 1 && input[0] == Char;
        }

        public bool CreateToken(string buffer, Lexer lexer, out Token token)
        {
            if (buffer.Length == 1 && buffer[0] == Char)
            {
                token = new Token(Type, Char.ToString());
                return true;
            }
            token = default(Token);
            return false;
        }
    }
}