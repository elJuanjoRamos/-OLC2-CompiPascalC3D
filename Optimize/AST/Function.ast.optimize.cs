﻿using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Block;
using CompiPascalC3D.Optimize.Languaje.Function;
using Irony.Parsing;
using System;
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
                var identifier = actual.ChildNodes[1].Token.Text;

                LinkedList<Blocks> blocks = InstructionOptimize.GetInstructions(actual.ChildNodes[5], 1);

                lista_actual.AddLast(new Function(identifier, blocks));

                lista_actual = FUNCTION_LIS(actual.ChildNodes[7], lista_actual);

            }
            return lista_actual;


        }
    }
}
