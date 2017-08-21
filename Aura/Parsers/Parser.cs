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
            Stack = stack ?? throw new ArgumentNullException(nameof(stack));
        }

        private Token ReadType(TokenType type)
        {
            var token = Stack.Next();
            if (token.Type != type)
                throw new ParserException(Enum.GetName(typeof(TokenType), type), token);
            return token;
        }

        public IStatement ParseStatement()
        {
            switch (Stack.Peek().Type)
            {
                case TokenType.Val:
                case TokenType.Var:
                    return ParseVariableDefinition();

                case TokenType.For:
                    return ParseFor();

                case TokenType.While:
                    return ParseWhile();

                default:
                    return new ExpressionStatement
                    {
                        Expression = ParseExpression()
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
                    ReadType(TokenType.CloseParentheses);
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
                    throw new ParserException("An expression", Stack.Peek());
            }
            return Stack.Peek().IsBinaryOperator() ? ParseBinaryOperator(result) : result;
        }
    }
}