using Aura.Ast;
using Aura.Tokens;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public VariableExpression ParseVariable()
        {
            var token = Stack.Next();
            if (token.Type != TokenType.Identifier)
                return null;
            return new VariableExpression
            {
                Name = token.Data
            };
        }

        public BlockExpression ParseBlock()
        {
            if (Stack.Next().Type != TokenType.OpenBrace)
                return null;

            var result = new BlockExpression();

            while (true)
            {
                if (Stack.Peek().Type == TokenType.CloseBrace)
                    break;
                var stmt = ParseStatement();
                if (stmt != null)
                    result.Statements.Add(stmt);
            }

            Stack.Cursor++;
            return result;
        }

        public IfExpression ParseIf()
        {
            if (Stack.Next().Type != TokenType.If)
                return null;

            if (Stack.Next().Type != TokenType.OpenParentheses)
                return null;

            var expr = ParseExpression();
            if (expr == null)
                return null;

            var result = new IfExpression
            {
                Condition = expr
            };

            if (Stack.Next().Type != TokenType.CloseParentheses)
                return null;

            var block = ParseBlock();
            if (block == null)
                return null;
            result.Block = block;

            if (Stack.Peek().Type != TokenType.Else) return result;

            Stack.Cursor++;
            if (Stack.Peek().Type == TokenType.If)
            {
                var elif = ParseIf();
                if (elif == null)
                    return null;
                result.ElseIf = elif;
            }
            else if (Stack.Peek().Type == TokenType.OpenBrace)
            {
                var el = ParseBlock();
                if (el == null)
                    return null;
                result.Else = el;
            }

            return result;
        }
    }
}