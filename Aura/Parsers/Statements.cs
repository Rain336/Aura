using Aura.Ast;
using Aura.Ast.Statments;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public VariableStatment ParseVariableStatment()
        {
            var variable = new VariableStatment();
            var token = Stack.Next();
            if (token.Type != TokenType.Var && token.Type != TokenType.Val)
                throw new ParserException("Var or Val", token);

            variable.IsImutable = token.Type == TokenType.Val;
            variable.Name = ReadType(TokenType.Identifier).Data;

            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                variable.Type = ParseType();
            }

            if (Stack.Peek().Type == TokenType.Equals)
            {
                Stack.Cursor++;
                variable.Assignment = ParseExpression();
            }

            if (variable.Type == null && variable.Assignment != null)
                variable.Type = variable.Assignment.ResultType;
            if (variable.Assignment == null && variable.Type == null)
                throw new ParserException("Cannot deduce Type!", token);
            if (variable.IsImutable && variable.Assignment == null)
                throw new ParserException("Immutable variable must be asigned.", token);

            return variable;
        }

        public WhileStatement ParseWhile()
        {
            var result = new WhileStatement();
            ReadType(TokenType.While);
            ReadType(TokenType.OpenParentheses);
            result.Condition = ParseExpression();
            ReadType(TokenType.CloseParentheses);
            result.Block = ParseBlock();
            return result;
        }

        private IStatment ParseForeach()
        {
            var fe = new ForeachStatment
            {
                Variable = ReadType(TokenType.Identifier).Data
            };
            ReadType(TokenType.In);
            fe.Expression = ParseExpression();
            ReadType(TokenType.CloseParentheses);
            fe.Block = ParseBlock();
            return fe;
        }

        public IStatment ParseFor()
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

            var f = new ForStatment
            {
                Section1 = ParseStatement()
            };
            ReadType(TokenType.Semicolon);
            f.Section2 = ParseStatement();
            ReadType(TokenType.Semicolon);
            f.Section3 = ParseStatement();
            ReadType(TokenType.CloseParentheses);
            f.Block = ParseBlock();
            return f;
        }
    }
}