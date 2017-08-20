using Aura.Ast;
using Aura.Tokens;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public WhileStatment ParseWhile()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.While)
                return null;

            token = Stack.Next();
            if (token.Type != TokenType.OpenParentheses)
                return null;

            var expr = ParseExpression();
            if (expr == null)
                return null;

            var result = new WhileStatment
            {
                Expression = expr,
            };

            token = Stack.Next();
            if (token.Type != TokenType.CloseParentheses)
                return null;

            var block = ParseBlock();
            if (block == null)
                return null;

            result.Block = block;

            return result;
        }

        private IStatement ParseForeach()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Identifier)
                return null;

            var stmt = new ForeachStatement
            {
                Variable = token.Data
            };

            token = Stack.Next();
            if (token.Type != TokenType.In)
                return null;

            var expr = ParseExpression();
            if (expr == null)
                return null;

            stmt.Expression = expr;

            token = Stack.Next();
            if (token.Type != TokenType.CloseParentheses)
                return null;

            var block = ParseBlock();
            if (block == null)
                return null;

            stmt.Block = block;

            return stmt;
        }

        public IStatement ParseFor()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.For)
                return null;

            token = Stack.Next();
            if (token.Type != TokenType.OpenParentheses)
                return null;

            Stack.Cursor++;
            if (Stack.Peek().Type == TokenType.In)
            {
                Stack.Cursor--;
                return ParseForeach();
            }
            Stack.Cursor--;

            var stmt = ParseStatement();
            if (stmt == null)
                return null;

            var result = new ForStatement
            {
                Section1 = stmt
            };

            token = Stack.Next();
            if (token.Type != TokenType.Semicolon)
                return null;

            stmt = ParseStatement();
            if (stmt == null)
                return null;

            result.Section2 = stmt;

            token = Stack.Next();
            if (token.Type != TokenType.Semicolon)
                return null;

            stmt = ParseStatement();
            if (stmt == null)
                return null;

            result.Section3 = stmt;

            token = Stack.Next();
            if (token.Type != TokenType.CloseParentheses)
                return null;

            var block = ParseBlock();
            if (block == null)
                return null;

            result.Block = block;

            return result;
        }

        public VariableStatement ParseVariableDefinition()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Var && token.Type != TokenType.Val)
                return null;

            var statement = new VariableStatement
            {
                Immutable = token.Type == TokenType.Val,
            };

            token = Stack.Next();
            if (token.Type != TokenType.Identifier)
                return null;

            statement.Name = token.Data;

            token = Stack.Peek();

            if (token.Type == TokenType.Colon)
            {
                Stack.Cursor++;
                token = Stack.Next();
                if (token.Type != TokenType.Identifier)
                    return null;
                statement.Type = token.Data;
                token = Stack.Peek();
            }

            if (token.Type == TokenType.Equals)
            {
                Stack.Cursor++;
                var expr = ParseExpression();
                if (expr == null)
                    return null;
                statement.Assignment = expr;
            }

            return statement;
        }
    }
}