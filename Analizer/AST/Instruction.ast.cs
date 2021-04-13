using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class InstructionAST
    {

        public InstructionAST()
        {

        }


        public LinkedList<Instruction> INSTRUCTIONS_BODY(ParseTreeNode actual, int cant_tabs)
        {
            /*INSTRUCTIONS_BODY.Rule 
                = RESERV_BEGIN + INSTRUCTIONS + RESERV_END  + PUNTO
                ;*/
            var begind = actual.ChildNodes[0];

            LinkedList<Instruction> lista_instruciones = ISTRUCCIONES(actual.ChildNodes[1], cant_tabs);


            var end = actual.ChildNodes[2];
            return lista_instruciones;
        }

        //INSTRUCCIONES
        public LinkedList<Instruction> ISTRUCCIONES(ParseTreeNode actual, int cant_tabs)
        {
            LinkedList<Instruction> listaInstrucciones = new LinkedList<Instruction>();

            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                var inst = INSTRUCCION(nodo.ChildNodes[0], cant_tabs);
                listaInstrucciones.AddLast(inst);
            }
            return listaInstrucciones;
        }


        //INSTRUCCION
        public Instruction INSTRUCCION(ParseTreeNode actual, int cant_tabs)
        {
            if (actual.Term.ToString().Equals("WRITE"))
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;
                var WRHITE_PARAMETER = actual.ChildNodes[2];

                var isln = false;
                LinkedList<Expresion> list = new LinkedList<Expresion>();
                WriteAST writeAST = new WriteAST();

                list = writeAST.WRITES(WRHITE_PARAMETER, cant_tabs);

                if (actual.ChildNodes[0].Term.ToString().Equals("RESERV_WRITEN"))
                {
                    isln = true;
                }
                return new Write(list, isln, row, col, cant_tabs);

            }
            else if (actual.Term.ToString().Equals("IF-THEN"))
            {
                If _ifs = (new IF_AST()).IFTHEN(actual, cant_tabs);
                return _ifs;
            }
            else if (actual.Term.ToString().Equals("WHILE"))
            {
                While _while = (new WhileAST()).WHILE(actual, cant_tabs);
                return _while;
            }
            else if (actual.Term.ToString().Equals("VAR_ASSIGNATE"))
            {
                AssignationAST assignationAST = new AssignationAST();
                var _assignation = (new AssignationAST()).VAR_ASSIGNATE(actual, cant_tabs);

                if (_assignation is Assignation)
                {
                    return (Assignation)_assignation;
                }

                /*else if (_assignation is Assignation_array)
                {
                    //return (Assignation_array)_assignation;
                }
                else if (_assignation is Assignation_arrayMultiple)
                {
                    //return (Assignation_arrayMultiple)_assignation;
                }*/
            }
            else if (actual.Term.ToString().Equals("REPEAT_UNTIL"))
            {
                Repeat _repeat = (new RepeatAst()).REPEAT_UNTIL(actual, cant_tabs);
                return _repeat;
            }
            else if (actual.Term.ToString().Equals("FOR"))
            {
                For _for = ((new ForAST())).SENCECIA_FOR(actual, cant_tabs);
                return _for;
            }
            else if (actual.Term.ToString().Equals("SENTENCE_CASE"))
            {
                Switch _SW = (new CaseAST()).SENTENCE_CASE(actual, cant_tabs);
                return _SW;
            }
            else if (actual.Term.ToString().Equals("CONTINUE"))
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                return new Continue(row, col, cant_tabs);
            }
            else if (actual.Term.ToString().Equals("BREAK"))
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                return new Break(row, col, cant_tabs);
            }
            else if (actual.Term.ToString().Equals("CALL"))
            {
                return (new CallInstruction()).CALL(actual, cant_tabs);
            }
            else if (actual.Term.ToString().Equals("EXIT"))
            {
                return (new ExitAST()).getExit(actual, cant_tabs);
            }

            return null;
        }

    }
}
