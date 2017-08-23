using System;
using Aura.Ast.Expressions;

namespace Aura.Ast.Statements
{
    public sealed class ForStatement : IStatement
    {
        public IAstElement Parent { get; set; }
        public readonly IStatement Section1;
        public readonly IStatement Section2;
        public readonly IStatement Section3;
        public readonly Block Block;

        public ForStatement(IStatement section1, IStatement section2, IStatement section3, Block block)
        {
            if(section1 == null)
                throw new ArgumentNullException(nameof(section1));
            if(section2 == null)
                throw new ArgumentNullException(nameof(section2));
            if(section3 == null)
                throw new ArgumentNullException(nameof(section3));
            if(block == null)
                throw new ArgumentNullException(nameof(block));

            section1.Parent = this;
            section2.Parent = this;
            section3.Parent = this;
            block.Parent = this;
            
            Section1 = section1;
            Section2 = section2;
            Section3 = section3;
            Block = block;
        }
    }
}