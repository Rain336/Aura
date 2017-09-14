using Aura.Ast.Expressions;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public IExpression ParseBinaryOperator(IExpression left, int min = 1)
        {
            var token = Stack.Peek();
            if (!token.IsBinaryOperator())
                throw new ParserException("Expected Operator", token);

            while (token.GetPrecedence() >= min)
            {
                var op = token;
                Stack.Cursor++;

                var right = ParseExpression();

                token = Stack.Peek();

                while (token.GetPrecedence() > op.GetPrecedence())
                {
                    right = ParseBinaryOperator(right, token.GetPrecedence());
                    token = Stack.Peek();
                    if (!token.IsBinaryOperator())
                        throw new ParserException("Expected Operator", token);
                }

                left = new BinaryOperator
                {
                    Left = left,
                    Operator = op.Type,
                    Right = right
                };
            }

            return left;
        }

        public UnaryOperator ParseUnaryOperator()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Plus && token.Type != TokenType.Minus)
                throw new ParserException("Plus or Minus", token);

            return new UnaryOperator
            {
                Expression = ParseExpression(),
                Type = token.Type
            };
        }
    }
}