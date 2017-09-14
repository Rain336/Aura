using Aura.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Expressions
{
    public sealed class UnaryOperator : IExpression
    {
        public IExpression Expression { get; set; }
        public TokenType Type { get; set; }
        public IType ResultType => Expression.ResultType;
    }
}
