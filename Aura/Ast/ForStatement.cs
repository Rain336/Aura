namespace Aura.Ast
{
    public sealed class ForStatement : IStatement
    {
        public IStatement Section1 { get; set; }
        public IStatement Section2 { get; set; }
        public IStatement Section3 { get; set; }
        public BlockExpression Block { get; set; }
    }
}