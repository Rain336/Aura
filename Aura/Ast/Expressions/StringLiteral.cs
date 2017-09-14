using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Expressions
{
    public sealed class StringLiteral : IExpression
    {
        public string Text { get; set; }
        public IType ResultType => SimpleType.String;
    }
}
