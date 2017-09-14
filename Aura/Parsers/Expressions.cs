using Aura.Ast;
using Aura.Ast.Expressions;
using Aura.Ast.Statments;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public VariableAccessExpression ParseVariable()
        {
            return new VariableAccessExpression
            {
                Name = ReadType(TokenType.Identifier).Data,
                //ResultType = 
            };
        }

        public IType ParseType()
        {
            var type = ReadType(TokenType.Identifier).Data;
            if (Stack.Peek().Type != TokenType.GreaterThan) return new SimpleType { Text = type };
            Stack.Cursor++;

            var result = new GenericType
            {
                Text = type
            };

            while (Stack.Peek().Type != TokenType.LessThan)
            {
                result.Prameters.Add(ParseType());
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

            while (Stack.Peek().Type != TokenType.CloseBrace)
            {
                result.Statments.Add(ParseStatement());
            }

            if (result.Statments.Count > 0)
            {
                var last = result.Statments[result.Statments.Count - 1];
                if (last is ExpressionStatment)
                {
                    result.ResultType = ((ExpressionStatment)last).Expression.ResultType;
                }
                else result.ResultType = SimpleType.Void;
            }
            else result.ResultType = SimpleType.Void;

            Stack.Cursor++;
            return result;
        }

        public IfExpression ParseIf()
        {
            var result = new IfExpression();
            ReadType(TokenType.If);
            ReadType(TokenType.OpenParentheses);
            result.Condition = ParseExpression();
            ReadType(TokenType.CloseParentheses);
            result.Block = ParseBlock();
            if (Stack.Peek().Type != TokenType.Else) return result;

            Stack.Cursor++;
            var token = Stack.Peek();
            if (token.Type == TokenType.If)
            {
                result.ElseIf = ParseIf();
                if (result.ResultType != SimpleType.Void && result.ElseIf.ResultType != result.ResultType)
                    throw new ParserException("Types in If and ElseIf do not match", token);
            }
            else if (token.Type == TokenType.OpenBrace)
            {
                result.ElseBlock = ParseBlock();
                if (result.ResultType != SimpleType.Void && result.ElseBlock.ResultType != result.ResultType)
                    throw new ParserException("Types in If-Block and Else-Block do not match", token);
            }
            else
                throw new ParserException("Expected 'if' or '{'", token);
            return result;
        }
    }
}