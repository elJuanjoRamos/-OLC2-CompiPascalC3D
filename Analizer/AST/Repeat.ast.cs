using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class RepeatAst
    {
        public RepeatAst()
        {


        }

        #region REPEAT UTIL
        public Repeat REPEAT_UNTIL(ParseTreeNode actual, int cant_tabs)
        {
            //REPEAT_UNTIL.Rule = RESERV_REPEAT + INSTRUCTIONS + RESERV_UNTIL + LOGIC_EXPRESION + PUNTO_COMA;

            //SE OBTIENEN LOS VALORES
            var instrucciones = actual.ChildNodes[1];
            var condicion = (new ExpresionAST()).getExpresion(actual.ChildNodes[3], cant_tabs);

            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            InstructionAST instructionAST = new InstructionAST();

            //OBTENGO LA LISTA DE INSTRUCCIONES
            LinkedList<Instruction> lista_instrucciones = instructionAST.ISTRUCCIONES(instrucciones, cant_tabs+1);

            //RETORNO EL NUEVO REPEAT-UTIL
            return new Repeat(condicion, new Sentence(lista_instrucciones), row, col, cant_tabs);

        }
        #endregion
    }
}
