using System;

namespace Aura.Ast.Expressions
{
    public sealed class IfExpression : IExpression
    {
        public IAstElement Parent { get; set; }
        public TypeElement Type { get; set; }
        public readonly IExpression Condition;
        public readonly Block Block;
        public readonly IfExpression ElseIf;
        public readonly Block ElseBlock;

        public IfExpression(IExpression condition, Block block, IfExpression elseIf, Block elseBlock)
        {
            if(condition == null)
                throw new ArgumentNullException(nameof(condition));
            if(block == null)
                throw new ArgumentNullException(nameof(block));
            
            Condition = condition;
            Block = block;
            ElseIf = elseIf;
            ElseBlock = elseBlock;
        }
    }
}