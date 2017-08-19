using System.Collections.Generic;

namespace Aura.Ast
{
    public sealed class BlockExpression : IExpression
    {
        public readonly List<IStatement> Statements = new List<IStatement>();
    }
}