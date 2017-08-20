using System;

namespace Aura.Tokens
{
    public struct Token : IEquatable<Token>
    {
        public readonly TokenType Type;
        public readonly string Data;
        public int Line { get; set; }
        public int Column { get; set; }

        public Token(TokenType type, string data)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));
            
            Type = type;
            Data = data;
            Line = -1;
            Column = -1;
        }

        public bool Equals(Token other)
        {
            return Type == other.Type && Data == other.Data;
        }

        public override bool Equals(object obj)
        {
            return obj is Token && Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type * 397) ^ Data.GetHashCode();
            }
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(TokenType), Type) + '[' + Data + ']';
        }

        public static bool operator ==(Token l, Token r)
        {
            return l.Equals(r);
        }
        
        public static bool operator !=(Token l, Token r)
        {
            return !l.Equals(r);
        }
    }
}