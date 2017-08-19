namespace Aura.Ast
{
    public sealed class IfExpression : IExpression
    {
        public IExpression Condition { get; set; }
        public BlockExpression Block { get; set; }
        public IfExpression ElseIf { get; set; }
        public BlockExpression Else { get; set; }
    }
}