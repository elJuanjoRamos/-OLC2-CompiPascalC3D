using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Arithmetics;
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

        public LinkedList<Blocks> Block_instructions { get => block_instructions; set => block_instructions = value; }
        public string Id { get => id; set => id = value; }

        public override object Optimize()
        {

            //Simplificación algebraica y reducción por fuerza
            var i = 0;
            Dictionary<int, Instruction> newDictionary = new Dictionary<int, Instruction>();

            foreach (Blocks blocks in block_instructions)
            {
                foreach (Instruction instruction in blocks.Instructions.Values)
                {
                    if (instruction is Expresion)
                    {
                        Expresion arithmetic = (Expresion)instruction;
                        arithmetic.set_ambit(Id);
                        var result = arithmetic.Optimize();
                        newDictionary[i] = (Instruction)result;       
                        i++;
                    }
                }
                blocks.set_new_instruction(newDictionary);

            }


            return true;
        }
    }
}
