namespace Aura.Ast
{
    public sealed class ExpressionStatement : IStatement
    {
        public IExpression Expression { get; set; }
    }
}