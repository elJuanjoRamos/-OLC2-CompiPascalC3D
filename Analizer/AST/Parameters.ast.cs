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


        public ArrayList CALL_PARAMETERS(ParseTreeNode actual, ArrayList expresiones, int cant_tabs)
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
                    var expr = (expressionAST.getExpresion(actual.ChildNodes[0], cant_tabs));
                    expresiones.Add(expr);
                    expresiones = CALL_PARAMETERS(actual.ChildNodes[1], expresiones, cant_tabs);
                }

                else
                {
                    var expr = expressionAST.getExpresion(actual.ChildNodes[1], cant_tabs);

                    expresiones.Add(expr);

                    expresiones = CALL_PARAMETERS(actual.ChildNodes[2], expresiones, cant_tabs);
                }


            }
            return expresiones;
        }
    }
}
