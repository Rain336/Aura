using Aura.Ast;
using Aura.Tokens;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public VariableExpression ParseVariable()
        {
            return new VariableExpression
            {
                Name = ReadType(TokenType.Identifier).Data
            };
        }

        public BlockExpression ParseBlock()
        {
            ReadType(TokenType.OpenBrace);
            var result = new BlockExpression();

            while (true)
            {
                if (Stack.Peek().Type == TokenType.CloseBrace)
                    break;
                result.Statements.Add(ParseStatement());
            }

            Stack.Cursor++;
            return result;
        }

        public IfExpression ParseIf()
        {
            ReadType(TokenType.If);
            ReadType(TokenType.OpenParentheses);

            var result = new IfExpression
            {
                Condition = ParseExpression()
            };

            ReadType(TokenType.CloseParentheses);

            result.Block = ParseBlock();

            if (Stack.Peek().Type != TokenType.Else) return result;

            Stack.Cursor++;
            if (Stack.Peek().Type == TokenType.If)
                result.ElseIf = ParseIf();
            else if (Stack.Peek().Type == TokenType.OpenBrace)
                result.Else = ParseBlock();

            return result;
        }
    }
}