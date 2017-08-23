using Aura.Ast.Literals;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public ILiteral ParseLiteral()
        {
            switch (Stack.Peek().Type)
            {
                case TokenType.String:
                    return ParseStringLiteral();

                case TokenType.Decimal:
                case TokenType.Hexadecimal:
                    return ParseNumericLiteral();

                default:
                    return null;
            }
        }

        public StringLiteral ParseStringLiteral()
        {
            return new StringLiteral(ReadType(TokenType.String).Data);
        }

        public NumberLiteral ParseNumericLiteral()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Decimal && token.Type != TokenType.Hexadecimal)
                throw new ParserException("Numeric Literal", token);

            return new NumberLiteral(token.Data, token.Type == TokenType.Hexadecimal);
        }
    }
}