using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Block;

namespace CompiPascalC3D.Optimize.Languaje.Function
{
    class Function : Instruction
    {
        private string id;
        private LinkedList<Blocks> block_instructions;

        public Function(string id, LinkedList<Blocks> block_instructions)
        {
            this.id = id;
            this.block_instructions = block_instructions;
        }


        public override object Optimize()
        {
            return "";
        }
    }
}
