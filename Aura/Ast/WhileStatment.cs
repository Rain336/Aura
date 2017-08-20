namespace Aura.Ast
{
    public sealed class WhileStatment : IStatement
    {
        public IExpression Expression { get; set; }
        public BlockExpression Block { get; set; }
    }
}