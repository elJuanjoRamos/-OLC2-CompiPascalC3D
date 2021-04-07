using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class FunctionAST
    {
        public FunctionAST()
        {

        }

        //variables
        InstructionAST instructionAST = new InstructionAST();
        DeclarationAST declarationAST = new DeclarationAST();


        #region FUNCIONES


        public LinkedList<Instruction> FUNCTION_LIST(ParseTreeNode actual,
            LinkedList<Instruction> lista_funciones, ArrayList parametros_her, int cant_tabs)
        {
            /*
              FUNCTION_LIST.Rule
                = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA 
                + FUNCTION_HIJA
                + DECLARATION_LIST
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */
            //EMPTY
            if (actual.ChildNodes.Count > 0)
            {


                var tipo = actual.ChildNodes[0].Term.ToString();

                if (tipo.Equals("RESERV_PROCEDURE"))
                {
                    lista_funciones = getProcedimientos(actual, lista_funciones, parametros_her, cant_tabs);
                }
                else
                {
                    lista_funciones = getFunciones(actual, lista_funciones, parametros_her, cant_tabs);
                }


            }
            return lista_funciones;
        }

        public LinkedList<Instruction> getFunciones(ParseTreeNode actual,
            LinkedList<Instruction> lista_funciones, ArrayList parametros_her, int cant_tabs)
        {
            /*
              FUNCTION_LIST.Rule
                = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                + DECLARATION_LIST_HIJA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */
            LinkedList<Instruction> parametros = new LinkedList<Instruction>();
            LinkedList<Instruction> declaraciones = new LinkedList<Instruction>();
            LinkedList<Instruction> fuciones_hijas = new LinkedList<Instruction>();

            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;


            var reserv_fun = actual.ChildNodes[0].Token.Text;

            var identifier = actual.ChildNodes[1].Token.Text;

            parametros = PARAMETER(actual.ChildNodes[3], parametros, parametros_her);

            var function_type = actual.ChildNodes[6].ChildNodes[0].Token.Text;

            declaraciones = declarationAST.LIST_DECLARATIONS(actual.ChildNodes[8], declaraciones, new ArrayList(), cant_tabs + 1);

            fuciones_hijas = FUNCION_HIJA(actual.ChildNodes[9], fuciones_hijas, cant_tabs, identifier);

            var function_instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[10], cant_tabs + 1);

            lista_funciones.AddLast(new Function(identifier, parametros, declaraciones, fuciones_hijas, function_type, 
                new Sentence(function_instructions), false, false, "", identifier, row, col));


            parametros_her.Clear();

            lista_funciones = FUNCTION_LIST(actual.ChildNodes[12], lista_funciones, parametros_her, cant_tabs);

            return lista_funciones;
        }


        public LinkedList<Instruction> getProcedimientos(ParseTreeNode actual, LinkedList<Instruction>
            lista_funciones, ArrayList parametros_her, int can_tabs)
        {

            /*
             FUNCTION_LIST.Rule
                =  RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                + DECLARATION_LIST_HIJA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
             */

            LinkedList<Instruction> parametros = new LinkedList<Instruction>();
            LinkedList<Instruction> declaraciones = new LinkedList<Instruction>();
            LinkedList<Instruction> fuciones_hijas = new LinkedList<Instruction>();

            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            var identifier = actual.ChildNodes[1].Token.Text;

            parametros = PARAMETER(actual.ChildNodes[3], parametros, parametros_her);

            declaraciones = declarationAST.LIST_DECLARATIONS(actual.ChildNodes[6], declaraciones, new ArrayList(), can_tabs + 1);

            fuciones_hijas = FUNCION_HIJA(actual.ChildNodes[7], fuciones_hijas, 0, identifier);


            var instructions = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[8], can_tabs + 1);

            parametros_her.Clear();

            lista_funciones.AddLast(new Function(identifier, parametros, declaraciones, fuciones_hijas, "void",
                new Sentence(instructions), true, false, "", identifier,  row, col));


            lista_funciones = FUNCTION_LIST(actual.ChildNodes[10], lista_funciones, parametros_her, can_tabs);

            return lista_funciones;
        }


        public LinkedList<Instruction> PARAMETER(ParseTreeNode actual, 
            LinkedList<Instruction> parametros, ArrayList elementos_her)
        {
            /*
               PARAMETER.Rule
                = RESERV_VAR + IDENTIFIER + PARAMETER_BODY + DOS_PUNTOS + DATA_TYPE + PARAMETER_END
                | IDENTIFIER + PARAMETER_BODY + DOS_PUNTOS + DATA_TYPE + PARAMETER_END
                | Empty;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;
                //CON RESERVADA VAR
                if (actual.ChildNodes.Count == 6)
                {
                    elementos_her.Add(actual.ChildNodes[1].Token.Text);
                    elementos_her = PARAMETER_BODY(actual.ChildNodes[2], elementos_her);
                    var dataType = actual.ChildNodes[4].ChildNodes[0].Token.Text;


                    foreach (var item in elementos_her)
                    {
                        parametros.AddLast(declarationAST.GetDeclarationValue(item.ToString(), dataType, true, row, col, true, 0));
                    }

                    //SI VIENEN MAS PARAMETROS
                    elementos_her.Clear();
                    parametros = PARAMETER_END(actual.ChildNodes[5], parametros, elementos_her);



                }

                //SIN RESERVADA VAR
                else
                {
                    elementos_her.Add(actual.ChildNodes[0].Token.Text);
                    elementos_her = PARAMETER_BODY(actual.ChildNodes[1], elementos_her);
                    var dataType = actual.ChildNodes[3].ChildNodes[0].Token.Text;


                    foreach (var item in elementos_her)
                    {
                        parametros.AddLast(declarationAST.GetDeclarationValue(item.ToString(), dataType, true, row, col, false, 0));
                    }

                    //SI VIENEN MAS PARAMETROS
                    elementos_her.Clear();
                    parametros = PARAMETER_END(actual.ChildNodes[4], parametros, elementos_her);

                }

            }
            return parametros;
        }
        public ArrayList PARAMETER_BODY(ParseTreeNode actual, ArrayList parametros_her)
        {

            /*
             PARAMETER_BODY.Rule 
                =  COMA + IDENTIFIER + PARAMETER_BODY
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                parametros_her.Add(actual.ChildNodes[1].Token.Text);
                parametros_her = PARAMETER_BODY(actual.ChildNodes[2], parametros_her);
            }

            return parametros_her;
        }
        public LinkedList<Instruction> PARAMETER_END(ParseTreeNode actual, LinkedList<Instruction> parametros, ArrayList parametros_her)
        {
            /*
             PARAMETER_END.Rule = PUNTO_COMA + PARAMETER
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                parametros = PARAMETER(actual.ChildNodes[1], parametros, parametros_her);
            }

            return parametros;
        }


        public LinkedList<Instruction> FUNCION_HIJA(ParseTreeNode actual,
            LinkedList<Instruction> fuciones_hijas, int cant_tabs, string padre)
        {
            /*
              
                
                | RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                + DECLARATION_LIST_HIJA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCION_HIJA
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var tipo = actual.ChildNodes[0].Term.ToString();
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                if (tipo.Equals("RESERV_PROCEDURE"))
                {
                    /*
                 | RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                 + DECLARATION_LIST_HIJA
                 + FUNCION_HIJA
                 + INSTRUCTIONS_BODY
                 + PUNTO_COMA
                 + FUNCION_HIJA
                 | Empty
                 ;
                  */
                    LinkedList<Instruction> parametros = new LinkedList<Instruction>();
                    LinkedList<Instruction> declaraciones = new LinkedList<Instruction>();
                    LinkedList<Instruction> func_hijas = new LinkedList<Instruction>();

                    var id = actual.ChildNodes[1].Token.Text;

                    parametros = PARAMETER(actual.ChildNodes[3], parametros, new ArrayList());

                    declaraciones = declarationAST.LIST_DECLARATIONS(actual.ChildNodes[6], declaraciones, new ArrayList(), cant_tabs + 1);

                    func_hijas = FUNCION_HIJA(actual.ChildNodes[7], func_hijas, 0, id);

                    var instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[8], cant_tabs + 1);

                    fuciones_hijas.AddLast(new Function(id, parametros, declaraciones, func_hijas, "void", 
                        new Sentence(instrucciones), true, true, padre, id, row, col));

                    fuciones_hijas = FUNCION_HIJA(actual.ChildNodes[10], fuciones_hijas, cant_tabs, padre);


                }
                else
                {
                    /*
                     FUNCION_HIJA.Rule 
                    = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA 
                    + DECLARATION_LIST_HIJA
                    + FUNCION_HIJA
                    + INSTRUCTIONS_BODY
                    + PUNTO_COMA 
                    + FUNCION_HIJA
                     */
                    LinkedList<Instruction> parametros = new LinkedList<Instruction>();
                    LinkedList<Instruction> declaraciones = new LinkedList<Instruction>();
                    LinkedList<Instruction> func_hijas = new LinkedList<Instruction>();

                    var id = actual.ChildNodes[1].Token.Text;

                    parametros = PARAMETER(actual.ChildNodes[3], parametros, new ArrayList());

                    var type = actual.ChildNodes[6].ChildNodes[0].Token.Text;

                    declaraciones = declarationAST.LIST_DECLARATIONS(actual.ChildNodes[8], declaraciones, new ArrayList(), cant_tabs + 1);

                    func_hijas = FUNCION_HIJA(actual.ChildNodes[9], func_hijas, 0, id);

                    var instrucciones = instructionAST.INSTRUCTIONS_BODY(actual.ChildNodes[10], cant_tabs + 1);

                    fuciones_hijas.AddLast(new Function(id, parametros, declaraciones, func_hijas, type, new Sentence(instrucciones), false, true, padre, id, row, col));

                    fuciones_hijas = FUNCION_HIJA(actual.ChildNodes[12], fuciones_hijas, cant_tabs, padre);

                }
            }
            return fuciones_hijas;
        }


        #endregion

    }
}
