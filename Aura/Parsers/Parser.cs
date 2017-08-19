using System;
using Aura.Ast;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public readonly TokenStack Stack;

        public Parser(TokenStack stack)
        {
            if (stack == null)
                throw new ArgumentNullException(nameof(stack));

            Stack = stack;
        }

        public IStatement ParseStatement()
        {
            switch (Stack.Peek().Type)
            {
                case TokenType.Val:
                case TokenType.Var:
                    return ParseVariable();

                default:
                    var expr = ParseExpression();
                    if (expr == null)
                        return null;
                    return new ExpressionStatement
                    {
                        Expression = expr
                    };
            }
        }

        public IExpression ParseExpression()
        {
            IExpression result;
            switch (Stack.Peek().Type)
            {
                case TokenType.Decimal:
                case TokenType.Hexadecimal:
                    result = ParseNumericLiteral();
                    break;

                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.Times:
                case TokenType.Divide:
                    result = ParseUnaryOperator();
                    break;

                case TokenType.String:
                    result = ParseStringLiteral();
                    break;

                case TokenType.OpenParentheses:
                    Stack.Cursor++;
                    result = ParseExpression();
                    if (Stack.Peek().Type == TokenType.CloseParentheses)
                        Stack.Cursor++;
                    break;

                default:
                    return null;
            }
            return Stack.Peek().IsBinaryOperator() ? ParseBinaryOperator(result) : result;
        }
    }
}