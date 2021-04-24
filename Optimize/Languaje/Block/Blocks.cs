using CompiPascalC3D.Optimize.Languaje.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Block
{
    public class Blocks
    {
        private Dictionary<int, Instruction> instructions;

        public Blocks()
        {
            this.instructions = new Dictionary<int, Instruction>();
        }

        public Dictionary<int, Instruction> Instructions { get => instructions; set => instructions = value; }

        public void setInstruction(Instruction instruction, int i)
        {
            this.instructions[i] =  instruction;
        }
        public void set_new_instruction(Dictionary<int, Instruction> newDict)
        {
            this.instructions = newDict;
        }

         

    
    }
}
