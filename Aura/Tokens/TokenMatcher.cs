using System;

namespace Aura.Tokens
{
    public sealed class TokenMatcher<T> : IEquatable<TokenMatcher<T>>
    {
        public readonly TokenType Type;
        public readonly T Value;

        public TokenMatcher(TokenType type, T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Type = type;
            Value = value;
        }

        public bool Equals(TokenMatcher<T> other)
        {
            if (ReferenceEquals(other, null)) return false;
            return Type == other.Type && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TokenMatcher<T>);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type * 379) ^ Value.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"TokenMatcher[{Enum.GetName(typeof(TokenType), Type)} -> {Value}]";
        }

        public static bool operator ==(TokenMatcher<T> l, TokenMatcher<T> r)
        {
            return ReferenceEquals(l, null) ? ReferenceEquals(r, null) : l.Equals(r);
        }

        public static bool operator !=(TokenMatcher<T> l, TokenMatcher<T> r)
        {
            return ReferenceEquals(l, null) ? !ReferenceEquals(r, null) : !l.Equals(r);
        }
    }
}