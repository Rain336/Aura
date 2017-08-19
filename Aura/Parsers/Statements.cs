using Aura.Ast;
using Aura.Tokens;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public VariableStatement ParseVariable()
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