namespace Aura.Ast.Expressions
{
    public interface IExpression : IAstElement
    {
        TypeElement Type { get; set; }
    }
}