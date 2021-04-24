using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Block;
using CompiPascalC3D.Optimize.Languaje.If;
using CompiPascalC3D.Optimize.Languaje.Jumps;
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
            int i = 0;
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {

                var inst = INSTRUCCION(nodo.ChildNodes[0], cant_tabs);
 
                if (inst.Name.Equals("If") || inst.Name.Equals("Goto"))
                {
                    blocks.setInstruction(inst, i);
                    listaInstrucciones.AddLast(blocks);
                    blocks = new Blocks();
                    i = 0;
                }
                else if (inst.Name.Equals("SetLabel"))
                {
                    listaInstrucciones.AddLast(blocks);
                    blocks = new Blocks();
                    i = 0;
                    blocks.setInstruction(inst, i);
                } else
                {
                    blocks.setInstruction(inst, i);
                    i++;
                    if (i == actual.ChildNodes.Count)
                    {
                        listaInstrucciones.AddLast(blocks);
                    }
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
            else if (actual.Term.ToString().Equals("GOTO"))
            {
                
                Goto @goto = (new GotoOptimize()).GetGoto(actual);
                return @goto;
            }
            else if (actual.Term.ToString().Equals("SET_LABEL"))
            {
                return (new LabelOptimize()).GetSetLabel(actual);
            }
            else if (actual.Term.ToString().Equals("ARITMETICA"))
            {
                return (Instruction)(new ArithmeticOptimizer()).GetArithmetic(actual);
            }


            return null;

        }
    }
}
