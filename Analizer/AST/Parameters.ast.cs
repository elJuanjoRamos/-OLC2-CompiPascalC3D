using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class ParametersAST
    {
        //VARIABLES
        ExpresionAST expressionAST = new ExpresionAST();
        public ParametersAST()
        {

        }


        public ArrayList CALL_PARAMETERS(ParseTreeNode actual, ArrayList expresiones)
        {
            /*
             CALL_PARAMETERS.Rule
                = EXPRESION + CALL_PARAMETERS
                | COMA + EXPRESION + CALL_PARAMETERS 
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                if (actual.ChildNodes.Count == 2)
                {
                    var expr = (expressionAST.getExpresion(actual.ChildNodes[0], 0));
                    expresiones.Add(expr);
                    expresiones = CALL_PARAMETERS(actual.ChildNodes[1], expresiones);
                }

                else
                {
                    var expr = expressionAST.getExpresion(actual.ChildNodes[1], 0);

                    expresiones.Add(expr);

                    expresiones = CALL_PARAMETERS(actual.ChildNodes[2], expresiones);
                }


            }
            return expresiones;
        }
    }
}
