namespace Aura.Ast
{
    public sealed class UnaryOperator : IExpression
    {
        public char Operator { get; set; }
        public IExpression Number { get; set; }
    }
}