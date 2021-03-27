using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class WhileAST
    {
        public WhileAST()
        {

        }

        #region WHILE
        public While WHILE(ParseTreeNode actual, int cant_tabs)
        {
            //WHILE.Rule = RESERV_WHILE + LOGIC_EXPRESION + RESERV_DO + INSTRUCTIONS_BODY;

            var condition = (new ExpresionAST()).getExpresion(actual.ChildNodes[1], cant_tabs);

            int row = actual.ChildNodes[0].Token.Location.Line;
            int col = actual.ChildNodes[0].Token.Location.Column;


            InstructionAST instructionAST = new InstructionAST();

            LinkedList<Instruction> lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[3], cant_tabs+1);

            return new While(condition, new Sentence(lista_instrucciones), row, col, cant_tabs);

        }

        #endregion

    }
}
