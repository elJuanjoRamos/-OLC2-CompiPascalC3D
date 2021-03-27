using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    public class AssignationAST
    {
        public AssignationAST()
        {

        }

        ExpresionAST expressionAST = new ExpresionAST();

        #region ASIGNACION
        public object VAR_ASSIGNATE(ParseTreeNode actual)
        {
            /*
             VAR_ASSIGNATE.Rule 
                = IDENTIFIER + VAR_ASSIGNATE_EXP
                ;
             
             */
            var identifier = actual.ChildNodes[0].Token.Text;
            var row = actual.ChildNodes[0].Token.Location.Line;
            var column = actual.ChildNodes[0].Token.Location.Column;

            return VAR_ASSIGNATE_EXP(identifier, row, column, actual.ChildNodes[1]);



        }


        public object VAR_ASSIGNATE_EXP(string identifier, int row, int column, ParseTreeNode actual)
        {
            /*
             
             VAR_ASSIGNATE_EXP.Rule
                = DOS_PUNTOS + EQUALS + EXPLOGICA + PUNTO_COMA
                | COR_IZQ + EXPLOGICA + COR_DER + MORE_ACCES + DOS_PUNTOS + EQUALS + EXPLOGICA + PUNTO_COMA
                ;
             */
            if (actual.ChildNodes.Count == 4)
            {
                Expresion exp = null;


                var encontrado = false;
                for (int i = 0; i < actual.ChildNodes[2].ChildNodes.Count; i++)
                {
                    var a = actual.ChildNodes[2].ChildNodes[i];
                    if (a.Term.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
                    {
                        encontrado = true;
                        break;
                    }
                }
                //SOLO ES UNA EXPRESION
                if (!encontrado)
                {
                    exp = (new ExpresionAST()).getExpresion(actual.ChildNodes[2]);

                    return new Assignation(identifier, exp, row, column);
                }
                //ES UNA LLAMADA
                /*else
                {
                    var llamada_funcion = (new Call_Expression()).CALLFUNCTION(actual.ChildNodes[2].ChildNodes[0]);
                    return new Assignation(identifier, llamada_funcion, row, column);
                }*/
            }
            else if (actual.ChildNodes.Count == 8)
            {


                var index = (expressionAST).getExpresion(actual.ChildNodes[1]);


                ArrayList arrays = new ArrayList();

                arrays = MORE_ACCES(actual.ChildNodes[3], arrays);


                Expresion exp = null;


                var encontrado = false;
                for (int i = 0; i < actual.ChildNodes[6].ChildNodes.Count; i++)
                {
                    var a = actual.ChildNodes[6].ChildNodes[i];
                    if (a.Term.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
                    {
                        encontrado = true;
                        break;
                    }
                }
                //SOLO ES UNA EXPRESION
                if (!encontrado)
                {
                    /*exp = (expressionAST).getExpresion(actual.ChildNodes[6]);

                    if (arrays.Count == 0)
                    {
                        return new Assignation_array(identifier, exp, row, column, index);

                    }
                    else
                    {
                        return new Assignation_arrayMultiple(identifier, exp, row, column, index, arrays);
                    }*/

                }
                //ES UNA LLAMADA
                else
                {
                    /*var llamada_funcion = (new Call_Expression()).CALLFUNCTION(actual.ChildNodes[6].ChildNodes[0]);
                    if (arrays.Count == 0)
                    {
                        return new Assignation_array(identifier, llamada_funcion, row, column, index);
                    }
                    else
                    {
                        return new Assignation_arrayMultiple(identifier, llamada_funcion, row, column, index, arrays);
                    }*/
                }

            }

            return null;
        }

        public ArrayList MORE_ACCES(ParseTreeNode actual, ArrayList lista)
        {
            /*
             MORE_ACCES.Rule
                = COR_IZQ + EXPLOGICA + COR_DER + MORE_ACCES
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var exp = expressionAST.EXPLOGICA(actual.ChildNodes[1]);
                lista.Add(exp);

                lista = MORE_ACCES(actual.ChildNodes[3], lista);

            }
            return lista;
        }

        #endregion

    }
}
