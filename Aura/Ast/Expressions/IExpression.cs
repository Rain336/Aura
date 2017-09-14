using System;
using System.Collections.Generic;
using System.Text;

namespace Aura.Ast.Expressions
{
    public interface IExpression
    {
        IType ResultType { get; }
    }
}
