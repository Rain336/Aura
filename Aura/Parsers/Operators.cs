using Aura.Ast;
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
                return null;

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
                        return null;
                }

                left = new BinaryOperator
                {
                    Left = left,
                    Operator = op.Data[0],
                    Right = right
                };
            }

            return left;
        }

        public UnaryOperator ParseUnaryOperator()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Plus && token.Type != TokenType.Minus)
            {
                return null;
            }

            var number = ParseExpression();
            if (number == null)
            {
                return null;
            }

            return new UnaryOperator
            {
                Operator = token.Data[0],
                Number = number
            };
        }
    }
}