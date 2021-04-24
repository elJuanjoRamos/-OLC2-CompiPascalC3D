using CompiPascalC3D.Optimize.Languaje.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Block
{
    public class Blocks
    {
        private ArrayList instructions;

        public Blocks()
        {
            this.instructions = new ArrayList();
        }
        public void setInstruction(Instruction instruction)
        {
            this.instructions.Add(instruction);
        }

        public ArrayList Instructions { get => instructions; set => instructions = value; }
    }
}
