using Aura.Ast.Expressions;

namespace Aura.Ast.Statments
{
    public sealed class ExpressionStatment : IStatment
    {
        public IExpression Expression { get; set; }
    }
}
