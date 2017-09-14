using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Expressions
{
    public sealed class IfExpression : IExpression
    {
        public IExpression Condition { get; set; }
        public Block Block { get; set; }
        public Block ElseBlock { get; set; }
        public IfExpression ElseIf { get; set; }
        public IType ResultType => Block.ResultType;
    }
}
