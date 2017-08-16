using System;
using Aura.Ast;
using Aura.Tokens;
using Aura.Utils;

namespace Aura
{
    public sealed class Parser
    {
        public readonly TokenStack Stack;

        public Parser(TokenStack stack)
        {
            if (stack == null)
                throw new ArgumentNullException(nameof(stack));

            Stack = stack;
        }

        public IExpression ParseExpression()
        {
            Stack.PushCursor();
            
            IExpression expr = ParseUnaryOperator();
            if (expr != null)
            {
                Stack.ForgetCursor();
                return expr;
            }
            Stack.ApplyCursor();
            
            expr = ParseNumericLiteral();
            if (expr != null)
            {
                Stack.ForgetCursor();
                return expr;
            }
            Stack.ApplyCursor();
            
            expr = ParseStringLiteral();
            if (expr != null)
            {
                Stack.ForgetCursor();
                return expr;
            }
            Stack.ApplyCursor();
            
            throw new Exception(); //TODO: THROW EXCEPTION!
        }

        public IExpression ParseBinaryOperator()
        {
            return ParseBinaryOperator(ParseExpression());
        }

        public IExpression ParseBinaryOperator(IExpression left, int min = 0)
        {
            var token = Stack.Peek();
            if (!token.IsBinaryOperator())
                return null;

            while (token.GetPrecedence() >= min)
            {
                var op = token;
                Stack.Next();

                var right = ParseExpression();

                token = Stack.Peek();
                if (!token.IsBinaryOperator())
                    return null;

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
            Stack.PushCursor();
            var token = Stack.Next();
            if (token.Type != TokenType.Plus && token.Type != TokenType.Minus)
            {
                Stack.PopCursor();
                return null;
            }

            var number = ParseExpression();
            if (number == null)
            {
                Stack.PopCursor();
                return null;
            }

            Stack.ForgetCursor();
            return new UnaryOperator
            {
                Operator = token.Data[0],
                Number = number
            };
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