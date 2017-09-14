using Aura.Ast.Expressions;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public StringLiteral ParseStringLiteral()
        {
            return new StringLiteral
            {
                Text = ReadType(TokenType.String).Data
            };
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