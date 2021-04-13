using CompiPascalC3D.Analizer.Languaje.Expressions;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class CallExpresionAST
    {
        public CallExpresionAST()
        {

        }
        public CallFunction CALL(ParseTreeNode actual, int cant_tabs)
        {
            // CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;
            ArrayList prametros_llamada = new ArrayList();

            prametros_llamada = ((new ParametersAST())).CALL_PARAMETERS(actual.ChildNodes[2], prametros_llamada, cant_tabs);
            var row = actual.ChildNodes[0].Token.Location.Line;
            var column = actual.ChildNodes[0].Token.Location.Column;

            return new CallFunction(actual.ChildNodes[0].Token.Text, prametros_llamada, row, column, cant_tabs);
        }
    }
}
