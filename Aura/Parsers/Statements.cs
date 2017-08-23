using Aura.Ast.Definitions;
using Aura.Ast.Expressions;
using Aura.Ast.Statements;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public WhileStatement ParseWhile()
        {
            ReadType(TokenType.While);
            ReadType(TokenType.OpenParentheses);
            var expr = ParseExpression();
            ReadType(TokenType.CloseParentheses);
            var block = ParseBlock();
            return new WhileStatement(expr, block);
        }

        private IStatement ParseForeach()
        {
            var name = ReadType(TokenType.Identifier).Data;
            ReadType(TokenType.In);
            var expr = ParseExpression();
            ReadType(TokenType.CloseParentheses);
            var block = ParseBlock();
            return new ForeachStatement(name, expr, block);
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

            var section1 = ParseStatement();
            ReadType(TokenType.Semicolon);
            var section2 = ParseStatement();
            ReadType(TokenType.Semicolon);
            var section3 = ParseStatement();
            ReadType(TokenType.CloseParentheses);
            var block = ParseBlock();
            return new ForStatement(section1, section2, section3, block);
        }

        public VariableDefinition ParseVariableDefinition()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Var && token.Type != TokenType.Val)
                throw new ParserException("Var or Val", token);

            var immutable = token.Type == TokenType.Val;
            var name = ReadType(TokenType.Identifier).Data;
            TypeElement type = null;

            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                type = ParseType();
            }

            IExpression expr = null;
            if (Stack.Peek().Type == TokenType.Equals)
            {
                Stack.Cursor++;
                expr = ParseExpression();
            }

            if (expr == null && type == null)
                throw new ParserException("Cannot deduce Type!", token);
            if (immutable && expr == null)
                throw new ParserException("Immutable variable must be asigned.", token);

            return new VariableDefinition(immutable, name, type, expr);
        }
    }
}