using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Block;
using CompiPascalC3D.Optimize.Languaje.Function;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class FunctionOptimize
    {
        public FunctionOptimize()
        {

        }

        InstructionOptimize InstructionOptimize = new InstructionOptimize();
        public LinkedList<Instruction> FUNCTION_LIS(ParseTreeNode actual, LinkedList<Instruction> lista_actual)
        {
            /*
              FUNCTION_LIST.Rule
               = RESERV_VOID + IDENTIFIER + PAR_IZQ + PAR_DER + KEY_IZQ + INSTRUCTIONS + KEY_DER
               + FUNCTION_LIST
               | Empty
               ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var tipo = actual.ChildNodes[0].ChildNodes[0].Token.Text;
                var identifier = actual.ChildNodes[1].Token.Text;

                ArrayList blocks = InstructionOptimize.GetInstructions(actual.ChildNodes[5], 1);

                lista_actual.AddLast(new Function(identifier, tipo, blocks));

                lista_actual = FUNCTION_LIS(actual.ChildNodes[7], lista_actual);

            }
            return lista_actual;


        }
    }
}
