﻿using Aura.Ast.Expressions;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public VariableAccessExpression ParseVariable()
        {
            return new VariableAccessExpression(ReadType(TokenType.Identifier).Data);
        }

        public TypeElement ParseType()
        {
            var type = ReadType(TokenType.Identifier).Data;
            if (Stack.Peek().Type != TokenType.GreaterThan) return TypeElement.FromString(type);
            Stack.Cursor++;

            var result = new GenericTypeElement(type);

            while (Stack.Peek().Type != TokenType.LessThan)
            {
                result.WithGenerics(ParseType());
                if (Stack.Peek().Type == TokenType.Comma)
                    Stack.Cursor++;
            }
            Stack.Cursor++;
            return result;
        }

        public Block ParseBlock()
        {
            ReadType(TokenType.OpenBrace);
            var result = new Block();

            while (true)
            {
                if (Stack.Peek().Type == TokenType.CloseBrace)
                    break;
                result.WithStatments(ParseStatement());
            }

            Stack.Cursor++;
            return result;
        }

        public IfExpression ParseIf()
        {
            ReadType(TokenType.If);
            ReadType(TokenType.OpenParentheses);
            var condition = ParseExpression();
            ReadType(TokenType.CloseParentheses);
            var block = ParseBlock();
            if (Stack.Peek().Type != TokenType.Else) return new IfExpression(condition, block, null, null);

            Stack.Cursor++;
            if (Stack.Peek().Type == TokenType.If)
                return new IfExpression(condition, block, ParseIf(), null);
            if (Stack.Peek().Type == TokenType.OpenBrace)
                return new IfExpression(condition, block, null, ParseBlock());

            throw new ParserException("Expected 'if' or '{'", Stack.Peek());
        }
    }
}