using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Expressions
{
    public sealed class VariableAccessExpression : IExpression
    {
        public string Name { get; set; }
        public IType ResultType { get; set; }
    }
}
