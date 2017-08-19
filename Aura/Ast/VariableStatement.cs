namespace Aura.Ast
{
    public sealed class VariableStatement : IStatement
    {
        public bool Immutable { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IExpression Assignment { get; set; }
    }
}