using System.Collections.Generic;
using Aura.Ast.Statements;

namespace Aura.Ast.Expressions
{
    public sealed class Block : IExpression
    {
        public IAstElement Parent { get; set; }
        public TypeElement Type { get; set; }
        private readonly List<IStatement> _statments = new List<IStatement>();

        public Block WithStatments(IStatement stmt)
        {
            stmt.Parent = this;
            _statments.Add(stmt);
            return this;
        }

        public Block WithStatments(params IStatement[] stmts)
        {
            foreach (var stmt in stmts)
                stmt.Parent = this;
            _statments.AddRange(stmts);
            return this;
        }
    }
}