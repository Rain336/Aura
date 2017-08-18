using System;

namespace Aura.Test
{
    public static class Assert
    {
        public static void AreEqual<T>(T l, T r)
        {
            if (l == null && r == null) return;
            if (!l?.Equals(r) ?? true)
                throw new AssertException($"Expected: '{l}' Got: '{r}'");
        }

        public static void IsType<T>(object obj)
        {
            if (obj == null)
                throw new AssertException($"Expected Type: {typeof(T)} Got: null");
            if (typeof(T) != obj.GetType())
                throw new AssertException($"Expected Type: {typeof(T)} Got Type: {obj.GetType()}");
        }

        public sealed class AssertException : Exception
        {
            public AssertException()
            {
            }

            public AssertException(string message) : base(message)
            {
            }

            public AssertException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}