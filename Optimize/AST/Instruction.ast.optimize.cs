using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Block;
using CompiPascalC3D.Optimize.Languaje.If;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class InstructionOptimize
    {
        public InstructionOptimize()
        {
        }

        

        //INSTRUCCIONES
        public LinkedList<Blocks> GetInstructions(ParseTreeNode actual, int cant_tabs)
        {
            LinkedList<Blocks> listaInstrucciones = new LinkedList<Blocks>();

            Blocks blocks = new Blocks();
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                var inst = INSTRUCCION(nodo.ChildNodes[0], cant_tabs);


                if (inst.Name.Equals("If"))
                {
                    blocks.setInstruction(inst);
                    listaInstrucciones.AddLast(blocks);
                    blocks = new Blocks();
                }
                else if (inst.Name.Equals("SetLabel"))
                {
                    listaInstrucciones.AddLast(blocks);
                    blocks = new Blocks();
                    blocks.setInstruction(inst);
                } else
                {
                    blocks.setInstruction(inst);
                }


            }
            return listaInstrucciones;
        }

        //INSTRUCCION
        public Instruction INSTRUCCION(ParseTreeNode actual, int cant_tabs)
        {
            if (actual.Term.ToString().Equals("IF"))
            {
                If _ifs = (new IfOptimize()).IFTHEN(actual, cant_tabs);
                return _ifs;
            }


            return null;

        }
    }
}
