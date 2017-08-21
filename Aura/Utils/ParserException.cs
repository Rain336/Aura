using System;
using Aura.Tokens;

namespace Aura.Utils
{
    public sealed class ParserException : Exception
    {
        public const string Message = @"
Parsing Error: {0}
Line:    {1}
Column:  {2}

Expected:     {3}
Found:        {4} -> {5}
";

        public readonly string Expected;
        public readonly Token Token;

        public ParserException(string expected, Token token, string message = "")
            : base(string.Format(Message, message, token.Line, token.Column, expected,
                Enum.GetName(typeof(TokenType), token.Type), token.Data))
        {
            Expected = expected;
            Token = token;
        }
    }
}