using Aura.Ast;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public WhileStatment ParseWhile()
        {
            ReadType(TokenType.While);
            ReadType(TokenType.OpenParentheses);

            var result = new WhileStatment
            {
                Expression = ParseExpression(),
            };

            ReadType(TokenType.CloseParentheses);
            result.Block = ParseBlock();
            return result;
        }

        private IStatement ParseForeach()
        {
            var stmt = new ForeachStatement
            {
                Variable = ReadType(TokenType.Identifier).Data
            };

            ReadType(TokenType.In);
            stmt.Expression = ParseExpression();
            ReadType(TokenType.CloseParentheses);
            stmt.Block = ParseBlock();
            return stmt;
        }

        public IStatement ParseFor()
        {
            ReadType(TokenType.For);
            ReadType(TokenType.OpenParentheses);

            Stack.Cursor++;
            if (Stack.Peek().Type == TokenType.In)
            {
                Stack.Cursor--;
                return ParseForeach();
            }
            Stack.Cursor--;

            var result = new ForStatement
            {
                Section1 = ParseStatement()
            };

            ReadType(TokenType.Semicolon);
            result.Section2 = ParseStatement();
            ReadType(TokenType.Semicolon);
            result.Section3 = ParseStatement();
            ReadType(TokenType.CloseParentheses);
            result.Block = ParseBlock();
            return result;
        }

        public VariableStatement ParseVariableDefinition()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Var && token.Type != TokenType.Val)
                throw new ParserException("Var or Val", token);

            var statement = new VariableStatement
            {
                Immutable = token.Type == TokenType.Val,
                Name = ReadType(TokenType.Identifier).Data,
            };

            token = Stack.Peek();
            if (token.Type == TokenType.Colon)
            {
                Stack.Cursor++;
                statement.Type = ReadType(TokenType.Identifier).Data;
                token = Stack.Peek();
            }

            if (token.Type == TokenType.Equals)
            {
                Stack.Cursor++;
                statement.Assignment = ParseExpression();
            }

            return statement;
        }
    }
}