using Aura.Ast.Expressions;

namespace Aura.Ast.Statments
{
    public sealed class ForStatment : IStatment
    {
        public IStatment Section1 { get; set; }
        public IStatment Section2 { get; set; }
        public IStatment Section3 { get; set; }
        public Block Block { get; set; }
    }
}
