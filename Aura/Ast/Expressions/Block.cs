using System;
using System.Collections.Generic;
using Aura.Ast.Statments;

namespace Aura.Ast.Expressions
{
    public sealed class Block : IExpression
    {
        public IType ResultType { get; set; }
        public readonly List<IStatment> Statments = new List<IStatment>();
    }
}
