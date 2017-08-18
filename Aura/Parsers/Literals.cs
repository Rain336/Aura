using Aura.Ast;
using Aura.Tokens;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public ILiteral ParseLiteral()
        {
            var token = Stack.Peek();
            if (token.Type == TokenType.String)
                return ParseStringLiteral();
            if (token.Type == TokenType.Decimal || token.Type == TokenType.Hexadecimal)
                return ParseNumericLiteral();
            return null;
        }
        
        public StringLiteral ParseStringLiteral()
        {
            var token = Stack.Next();
            return token.Type != TokenType.String
                ? null
                : new StringLiteral
                {
                    Value = token.Data
                };
        }

        public NumericLiteral ParseNumericLiteral()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Decimal && token.Type != TokenType.Hexadecimal)
                return null;
            return new NumericLiteral
            {
                Value = token.Data,
                Hexadecimal = token.Type == TokenType.Hexadecimal
            };
        }
    }
}