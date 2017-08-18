namespace Aura.Ast
{
    public interface ILiteral : IExpression
    {
        string Value { get; set; }
    }
}