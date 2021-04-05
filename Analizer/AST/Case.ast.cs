using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class CaseAST
    {

        //VARIABLES 
        InstructionAST instructionAST = new InstructionAST();
        ExpresionAST expressionAST = new ExpresionAST();
        public CaseAST()
        {

        }


        #region CASE
        public Switch SENTENCE_CASE(ParseTreeNode actual, int cant_tabs)
        {
            /*
             *  SENTENCE_CASE.Rule = RESERV_CASE  + LOGIC_EXPRESION + RESERV_OF + CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;
          
            CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */

            var condicion = expressionAST.getExpresion(actual.ChildNodes[1], cant_tabs);
            ArrayList lista_cases = new ArrayList();
            lista_cases = CASES(actual.ChildNodes[3], lista_cases, cant_tabs);

            var else_case = CASE_ELSE(actual.ChildNodes[4], cant_tabs);

            var row = actual.ChildNodes[0].Token.Location.Line;
            var column = actual.ChildNodes[0].Token.Location.Column;

            return new Switch(condicion, lista_cases, else_case, row, column, cant_tabs);
        }

        public ArrayList CASES(ParseTreeNode actual, ArrayList lista_cases, int cant_tabs)
        {


            /*
               CASES.Rule 
                = CASE + CASES
                | Empty                
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                lista_cases.Add(CaseSing(actual.ChildNodes[0], cant_tabs));

                lista_cases = CASES(actual.ChildNodes[1], lista_cases, cant_tabs);
            }

            return lista_cases;
        }

        public Case CaseSing(ParseTreeNode actual, int can_tabs)
        {
            /* CASE.Rule = LOGIC_EXPRESION + DOS_PUNTOS + INSTRUCTIONS;*/
            var condicion = expressionAST.getExpresion(actual.ChildNodes[0], can_tabs);

            var lista_instrucciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[2], can_tabs+2);
            var row = actual.ChildNodes[1].Token.Location.Line;
            var col = actual.ChildNodes[1].Token.Location.Column;

            return new Case(condicion, new Sentence(lista_instrucciones), row, col, can_tabs+1);

        }

        public Case CASE_ELSE(ParseTreeNode actual, int cant_tabs)
        {
            /*
             CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
             */

            Case _case = new Case(0, 0);
            if (actual.ChildNodes.Count > 0)
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var lista_declaraciones = instructionAST.ISTRUCCIONES(actual.ChildNodes[1], cant_tabs+2);
                _case = new Case(new Sentence(lista_declaraciones), row, col, cant_tabs+1);
            }

            return _case;
        }
        #endregion

    }
}

