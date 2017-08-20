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
            Stack.PushCursor();
            IStatement result;

            switch (Stack.Peek().Type)
            {
                case TokenType.Val:
                case TokenType.Var:
                    result = ParseVariableDefinition();
                    break;

                case TokenType.For:
                    result = ParseFor();
                    break;

                case TokenType.While:
                    result = ParseWhile();
                    break;

                default:
                    var expr = ParseExpression();
                    if (expr == null)
                        result = null;
                    else
                        result = new ExpressionStatement
                        {
                            Expression = expr
                        };
                    break;
            }

            if (result == null)
                Stack.PopCursor();
            else
                Stack.ForgetCursor();

            return result;
        }

        public IExpression ParseExpression()
        {
            Stack.PushCursor();
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

                case TokenType.If:
                    result = ParseIf();
                    break;

                case TokenType.OpenBrace:
                    result = ParseBlock();
                    break;

                case TokenType.Identifier:
                    result = ParseVariable();
                    break;

                default:
                    return null;
            }

            if (result == null)
            {
                Stack.PopCursor();
                return null;
            }
            Stack.ForgetCursor();
            return Stack.Peek().IsBinaryOperator() ? ParseBinaryOperator(result) : result;
        }
    }
}