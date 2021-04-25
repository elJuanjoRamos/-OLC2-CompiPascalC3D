using CompiPascalC3D.Optimize.Languaje.Arithmetics;
using CompiPascalC3D.Optimize.Languaje.Heap_and_Stack;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class ArithmeticOptimizer
    {
        public ArithmeticOptimizer()
        {

        }
        LiteralOptimize LiteralOptimize = new LiteralOptimize();


        public object GetArithmetic(ParseTreeNode actual)
        {
            var data = LiteralOptimize.getLiteral(actual.ChildNodes[0]);
            int row = actual.ChildNodes[1].Token.Location.Line;
            int col = actual.ChildNodes[1].Token.Location.Column;

            if (actual.ChildNodes[2].ChildNodes.Count == 3)
            {
                var expresion = actual.ChildNodes[2];

                var izquierdo = LiteralOptimize.getLiteral(expresion.ChildNodes[0]);
                var derecho = LiteralOptimize.getLiteral(expresion.ChildNodes[2]);
                var simb = expresion.ChildNodes[1].Token.Text;

                return new Expresion(data.Value, izquierdo, derecho, simb, row, col);

            }
            else if (actual.ChildNodes[2].ChildNodes.Count == 1)
            {
                var expresion = actual.ChildNodes[2].ChildNodes[0];

                if (expresion.ChildNodes.Count == 1)
                {
                    var izquierdo = LiteralOptimize.getLiteral(expresion);
                    return new Expresion(data.Value, izquierdo, row, col);
                }
                //ES ACCESO
                // RESERV_HEAP + COR_IZQ + PAR_IZQ + RESERV_INT + PAR_DER + TERMINAL + COR_DER 
                else
                {
                    var structure = expresion.ChildNodes[0].Token.Text;
                    var indice = LiteralOptimize.getLiteral(expresion.ChildNodes[5]);
                    return new Access(data, structure, indice);
                }


            }
            

            return null;
        }

        
    }
}
