namespace Aura.Ast
{
    public sealed class ForeachStatement : IStatement
    {
        public string Variable { get; set; }
        public IExpression Expression { get; set; }
        public BlockExpression Block { get; set; }
    }
}