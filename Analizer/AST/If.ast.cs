using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class IF_AST
    {
        //VARIABLES
        InstructionAST instructionAST = new InstructionAST();

        public IF_AST()
        {

        }

        #region IF



        public If IFTHEN(ParseTreeNode actual, int cant_tabs)
        {
            /*
              IFTHEN.Rule
                = RESERV_IF + EXPRESION
                    + RESERV_THEN
                        + IF_SENTENCE
                    + ELIF;
             */
            If ifs = new If();
            ExpresionAST expressionAST = new ExpresionAST();
            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            var LOGIC_EXPRESION = expressionAST.getExpresion(actual.ChildNodes[1], cant_tabs);
            var SENTENCES = IF_SENTENCE(actual.ChildNodes[3], cant_tabs);
            var ELSE = ELIF(actual.ChildNodes[4], cant_tabs);
            
            return new If(LOGIC_EXPRESION, SENTENCES, ELSE, row, col, cant_tabs);
        }

        public LinkedList<Instruction> IF_SENTENCE(ParseTreeNode actual, int cant_tabs)
        {
            /*
               IF_SENTENCE.Rule = INSTRUCTIONS_BODY
                | Empty
                ;
             */

            if (actual.ChildNodes.Count == 0)
            {
                return new LinkedList<Instruction>();
            }

            return instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[0], cant_tabs + 1);
        }
        public LinkedList<Instruction> ELIF(ParseTreeNode actual, int cant_tabs)
        {

            LinkedList<Instruction> lista_instrucciones = new LinkedList<Instruction>();
            if (actual.ChildNodes.Count > 0)
            {
                // ELSE 
                if (actual.ChildNodes[1].Term.ToString().Equals("IF_SENTENCE"))
                {
                    lista_instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[1].ChildNodes[0], cant_tabs + 1);

                }
                // ELSE IF
                else
                {
                    var ifs = IFTHEN(actual.ChildNodes[1], cant_tabs);
                    lista_instrucciones.AddLast(ifs);
                }
            }
            return lista_instrucciones;
        }
        #endregion

    }
}
