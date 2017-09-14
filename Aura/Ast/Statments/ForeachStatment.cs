using Aura.Ast.Expressions;

namespace Aura.Ast.Statments
{
    public sealed class ForeachStatment : IStatment
    {
        public string Variable { get; set; }
        public IExpression Expression { get; set; }
        public Block Block { get; set; }
    }
}
