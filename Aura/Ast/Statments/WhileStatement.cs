using Aura.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Statments
{
    public sealed class WhileStatement : IStatment
    {
        public IExpression Condition { get; set; }
        public Block Block { get; set; }
    }
}
