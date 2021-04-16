using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Expressions;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using CompiPascalC3D.Analizer.Languaje.Sentences.Array;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class DeclarationAST
    {
        //VARIABLES 
        ExpresionAST expressionAST = new ExpresionAST();
        public DeclarationAST()
        {

        }

        #region DECLARACION


        public LinkedList<Instruction> LIST_DECLARATIONS(ParseTreeNode actual, LinkedList<Instruction> lista_actual, ArrayList elementos_her, int cant_tabs)
        {

            /*
             DECLARATION_LIST.Rule
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               | Empty
               ;
             */

            if (actual.ChildNodes.Count != 0)
            {

                int row = actual.ChildNodes[0].Token.Location.Line;
                int col = actual.ChildNodes[0].Token.Location.Column;
                //VERIFICA SI ES VAR O CONST
                var tipo = actual.ChildNodes[0];

                //ES CONST
                if (tipo.Term.ToString().Equals("RESERV_CONST"))
                {

                    var identifier = actual.ChildNodes[1].Token.Text;
                    lista_actual.AddLast(new Declaration(identifier, expressionAST.getExpresion(actual.ChildNodes[3], cant_tabs), row, col, true, false));
                    lista_actual = CONST_DECLARATION(actual.ChildNodes[5], lista_actual, elementos_her, cant_tabs);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[6], lista_actual, elementos_her, cant_tabs);
                }
                //ES VAR
                else
                {
                    var identifier = actual.ChildNodes[1].Token.Text;
                    elementos_her.Add(identifier);

                    lista_actual = DECLARATION_BODY(actual.ChildNodes[2], lista_actual, elementos_her, cant_tabs);
                    lista_actual = VAR_DECLARATION(actual.ChildNodes[3], lista_actual, elementos_her, cant_tabs);
                    lista_actual = LIST_DECLARATIONS(actual.ChildNodes[4], lista_actual, elementos_her, cant_tabs);

                }

                return lista_actual;


            }


            return lista_actual;
        }

        public LinkedList<Instruction> DECLARATION_BODY(ParseTreeNode actual, 
            LinkedList<Instruction> lista_actual, ArrayList elementos_her, int cant_tabs)
        {
            /*
             
              DECLARATION_BODY.Rule
                = DOS_PUNTOS + DATA_TYPE + ASSIGNATION + PUNTO_COMA
                | COMA + IDENTIFIER + MORE_ID + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                ;
             */
            var element = actual.ChildNodes[0];
            // SI VIENE VARIOS IDES 
            if (element.Term.ToString().ToLower().Equals("tk_coma"))
            {
                //OBTENGO EL IDENTIFICADOR
                var identifier = actual.ChildNodes[1].Token.Text;
                elementos_her.Add(identifier);
                //OBTENGO LOS DEMAS IDENTIFICADORES
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
                //OBTENGO EL TIPO
                var datatype = actual.ChildNodes[4].ChildNodes[0].Token.Text;

                var row = actual.ChildNodes[1].Token.Location.Line;
                var col = actual.ChildNodes[1].Token.Location.Column;

                foreach (var item in elementos_her)
                {
                    lista_actual.AddLast(GetDeclarationValue(item.ToString(), datatype, false, row, col, false, cant_tabs));
                }
                elementos_her.Clear();

            }
            //SI VIENE UN SOLO ID
            else
            {
                var esArray = false;

                if (actual.ChildNodes[1].ChildNodes[0].Term.ToString().Equals("IDENTIFIER_ARRAY_TYPE"))
                {
                    esArray = true;
                }

                var datatype = actual.ChildNodes[1].ChildNodes[0].Token.Text;

                elementos_her.Add(datatype);
                lista_actual = ASSIGNATION_VARIABLE(actual.ChildNodes[2], lista_actual, elementos_her, esArray, cant_tabs);
            }
            return lista_actual;
        }
        public LinkedList<Instruction> VAR_DECLARATION(ParseTreeNode actual, 
            LinkedList<Instruction> lista_actual, ArrayList elementos_her, int cant_tabs)
        {
            /*
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[0].Token.Text;
                elementos_her.Add(identifier);
                lista_actual = DECLARATION_BODY(actual.ChildNodes[1], lista_actual, elementos_her, cant_tabs);
                lista_actual = VAR_DECLARATION(actual.ChildNodes[2], lista_actual, elementos_her, cant_tabs);

            }

            return lista_actual;
        }
        public LinkedList<Instruction> CONST_DECLARATION(ParseTreeNode actual, LinkedList<Instruction> lista_actual, ArrayList elementos_her, int cant_tabs)
        {
            /*
             *  CONST_DECLARATION.Rule = IDENTIFIER + EQUALS + LOGIC_EXPRESION + PUNTO_COMA + CONST_DECLARATION
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                int row = actual.ChildNodes[0].Token.Location.Line;
                int col = actual.ChildNodes[0].Token.Location.Column;
                var identifier = actual.ChildNodes[0].Token.Text;
                lista_actual.AddLast(new Declaration(identifier, expressionAST.getExpresion(actual.ChildNodes[2], cant_tabs), row, col, true, false));
                lista_actual = CONST_DECLARATION(actual.ChildNodes[4], lista_actual, elementos_her, cant_tabs);
            }
            return lista_actual;
        }

        public LinkedList<Instruction> ASSIGNATION_VARIABLE(ParseTreeNode actual, 
            LinkedList<Instruction> lista_actual, ArrayList elementos_her, bool esarray, int cant_tabs)
        {

            var row = 0;
            var col = 0;

            //VAR A: TIPO = EXP;
            if (actual.ChildNodes.Count > 0)
            {
                row = actual.ChildNodes[0].Token.Location.Line;
                col = actual.ChildNodes[0].Token.Location.Column;
                var exp = expressionAST.getExpresion(actual.ChildNodes[1], cant_tabs);
                lista_actual.AddLast(new Declaration(elementos_her[0].ToString(), elementos_her[1].ToString(), exp, row, col, true, false));
                elementos_her.Clear();
            }
            // VAR A:TIPO;
            else
            {
                if (!esarray)
                {
                    lista_actual.AddLast(GetDeclarationValue(elementos_her[0].ToString(), elementos_her[1].ToString(), false, row, col, false, cant_tabs));
                    elementos_her.Clear();
                }
                else
                {
                    lista_actual.AddLast(new DeclarationArray(elementos_her[0].ToString(), elementos_her[1].ToString(), row, col, cant_tabs));
                    elementos_her.Clear();
                }
            }
            return lista_actual;
        }

        public ArrayList MORE_ID_DECLARATION(ParseTreeNode actual, ArrayList elementos_her)
        {

            if (actual.ChildNodes.Count > 0)
            {
                var identifier = actual.ChildNodes[1].Token.Text;
                elementos_her.Add(identifier);
                elementos_her = MORE_ID_DECLARATION(actual.ChildNodes[2], elementos_her);
            }
            return elementos_her;
        }
        public Declaration GetDeclarationValue(string identifier, string datatype, bool perteneceFuncion, int row, int col, bool refe, int cant_tabs)
        {

            if (datatype.ToLower().Equals("integer"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(0, 1, row, col, cant_tabs), row, col, false, refe);
            }
            else if (datatype.ToLower().Equals("real"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(0, 4, row, col, cant_tabs), row, col, false, refe);
            }
            else if (datatype.ToLower().Equals("string"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal("", 2, row, col, cant_tabs), row, col, false, refe);
            }
            else if (datatype.ToLower().Equals("boolean"))
            {
                return new Declaration(identifier.ToString(), datatype, new Literal(false, 3, row, col, cant_tabs), row, col, false, refe);
            }
            return null;
        }

        #endregion
    }
}
