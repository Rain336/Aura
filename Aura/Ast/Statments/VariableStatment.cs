using Aura.Ast.Expressions;

namespace Aura.Ast.Statments
{
    public sealed class VariableStatment : IStatment
    {
        public bool IsImutable { get; set; }
        public string Name { get; set; }
        public IType Type { get; set; }
        public IExpression Assignment { get; set; }
    }
}
