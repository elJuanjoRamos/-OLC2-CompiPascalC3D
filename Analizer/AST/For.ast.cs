using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class ForAST
    {
        //VARIABLES
        InstructionAST instructionAST = new InstructionAST();
        ExpresionAST expressionAST = new ExpresionAST();

        public ForAST()
        {

        }


        #region FOR
        public For SENCECIA_FOR(ParseTreeNode actual, int cant_tabs)
        {
            /*
             FOR.Rule
                = RESERV_FOR + IDENTIFIER + DOS_PUNTOS + EQUALS + LOGIC_EXPRESION + TODOWN + LOGIC_EXPRESION
                    + RESERV_DO
                        + INSTRUCTIONS_BODY //+ PUNTO_COMA
                ;
            TODOWN.Rule 
                = RESERV_TO
                | RESERV_DOWN + RESERV_TO
                ;
             */
            var ident = actual.ChildNodes[1].Token.Text;
            var inicio = expressionAST.getExpresion(actual.ChildNodes[4], cant_tabs);
            var direccion = actual.ChildNodes[5].ChildNodes[0].Token.Text;
            var fin = expressionAST.getExpresion(actual.ChildNodes[6], cant_tabs);
            var lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[8], cant_tabs+1);
            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            return new For(ident, inicio, fin, lista_instrucciones, direccion, row, col, cant_tabs);



        }
        #endregion

    }
}
