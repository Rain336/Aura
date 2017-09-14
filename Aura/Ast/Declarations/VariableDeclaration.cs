using Aura.Ast.Expressions;
using Aura.Utils;

namespace Aura.Ast.Declarations
{
    public sealed class VariableDeclaration
    {
        public Modifier AccessModifier { get; set; }
        public bool IsImutable { get; set; }
        public string Name { get; set; }
        public IType Type { get; set; }
        public IExpression Assignment { get; set; }
    }
}
