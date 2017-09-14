using System;
using Aura.Tokens;

namespace Aura.Ast.Expressions
{
    public sealed class BinaryOperator : IExpression
    {
        public IExpression Left { get; set; }
        public TokenType Operator { get; set; }
        public IExpression Right { get; set; }
        public IType ResultType => Left.ResultType;
    }
}
