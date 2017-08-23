using Aura.Ast.Expressions;

namespace Aura.Ast.Literals
{
    public interface ILiteral : IExpression
    {
        string Value { get; }
    }
}