using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Block;
using CompiPascalC3D.Optimize.Languaje.If;
using CompiPascalC3D.Optimize.Languaje.Jumps;
using Irony.Parsing;
using System;
using System.Collections;
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
        public ArrayList GetInstructions(ParseTreeNode actual, int cant_tabs)
        {
            ArrayList listaInstrucciones = new ArrayList();

            Blocks blocks = new Blocks();
            int i = 0;
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {

                var inst = INSTRUCCION(nodo.ChildNodes[0], cant_tabs);

                listaInstrucciones.Add(inst);
                
 
            }
            return listaInstrucciones;
        }

        //INSTRUCCION
        public Instruction INSTRUCCION(ParseTreeNode actual, int cant_tabs)
        {
            if (actual.Term.ToString().Equals("IF"))
            {
                IF _ifs = (new IfOptimize()).IFTHEN(actual, cant_tabs);
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
            else if (actual.Term.ToString().Equals("PRINT"))
            {
                return (new PrintOptimize()).GetPrint(actual);
            }
            else if (actual.Term.ToString().Equals("RETURN"))
            {
                return (new ReturnOptimize()).GetReturn(actual);
            }
            else if (actual.Term.ToString().Equals("CALL"))
            {
                return (new CallOptimize()).GetCall(actual);
            }
            else
            {
                return (new AssignationOptimize()).GetAssignation(actual);
            }
        }
    }
}
