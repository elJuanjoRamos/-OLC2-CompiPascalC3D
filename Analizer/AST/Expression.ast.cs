using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Expressions;
using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class ExpresionAST
    {
        public ExpresionAST()
        {

        }

        #region EXPRESIONES


        public Expresion getExpresion(ParseTreeNode actual, int cant_tabs)
        {

            return EXPLOGICA(actual, cant_tabs);

        }

        public Expresion GetLiteral(ParseTreeNode node, int cant_Tabs)
        {
            var row = node.Token.Location.Line;
            var column = node.Token.Location.Column;

            if (node.Term.ToString().ToString().Equals("NUMERO"))
            {
                return new Literal(node.Token.Value, 1, row, column, cant_Tabs);
            }
            else if (node.Term.ToString().Equals("CADENA"))
            {
                return new Literal(node.Token.Value, 2, row, column, cant_Tabs);
            }
            else if (node.Term.ToString().Equals("RESERV_TRUE") || node.Term.ToString().Equals("RESERV_FALSE"))
            {
                return new Literal(node.Token.Value, 3, row, column, cant_Tabs);
            }
            else if (node.Term.ToString().Equals("REAL"))
            {
                return new Literal(node.Token.Value, 4, row, column, cant_Tabs);
            }
            else if (node.Term.ToString().Equals("TYPE"))
            {
                return new Literal(node.Token.Value, 5, row, column, cant_Tabs);
            }
            else if (node.Term.ToString().Equals("ARRAY"))
            {
                return new Literal(node.Token.Value, 6, row, column, cant_Tabs);
            }
            else if (node.Term.ToString().Equals("IDENTIFIER"))
            {
                return new Access(node.Token.Value.ToString(), row, column, cant_Tabs);
            }

            return null;
        }
        #endregion



        public Expresion EXPLOGICA(ParseTreeNode actual, int cant_tabs)
        {
            /*
              EXPLOGICA.Rule 
                = EXPRELACIONAL + EXPLOGICA_PRIMA
                | NOT + EXPRELACIONAL + EXPLOGICA_PRIMA;
             */

            if (actual.ChildNodes.Count == 2)
            {
                var relacional = EXPRELACIONAL(actual.ChildNodes[0], cant_tabs);
                return EXPLOGICA_PRIMA(actual.ChildNodes[1], relacional, cant_tabs);
            }
            else
            {
                var not = actual.ChildNodes[0].Token.Text.ToLower();
                var izq = EXPRELACIONAL(actual.ChildNodes[1], cant_tabs);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var logical = new Logical(izq, null, not, row, col, cant_tabs);
                return EXPLOGICA_PRIMA(actual.ChildNodes[2], logical, cant_tabs);
                
            }

        }

        public Expresion EXPLOGICA_PRIMA(ParseTreeNode actual, Expresion izq, int cant_Tabs)
        {
            /*
              EXPLOGICA_PRIMA.Rule
                = AND + EXPRELACIONAL + EXPLOGICA_PRIMA
                | OR + EXPRELACIONAL + EXPLOGICA_PRIMA
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text.ToLower();
                var derecho = EXPRELACIONAL(actual.ChildNodes[1], cant_Tabs);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var logica = new Logical(izq, derecho, simb, row, col, cant_Tabs);
                return EXPLOGICA_PRIMA(actual.ChildNodes[2], logica, cant_Tabs);
                
            }
            return izq;
        }



        public Expresion EXPRELACIONAL(ParseTreeNode actual, int cant_tabs)
        {
            //EXPRELACIONAL.Rule = EXPRESION + EXPRELACIONAL_PRIMA;

            var exp = EXPRESION(actual.ChildNodes[0], cant_tabs);

            return EXPRELACIONAL_PRIMA(actual.ChildNodes[1], exp, cant_tabs);
        }

        public Expresion EXPRESION(ParseTreeNode actual, int cant_tabs)
        {
            //EXPRESION.Rule = TERMINO + EXPRESION_PRIMA;
            var exp = TERMINO(actual.ChildNodes[0], cant_tabs);
            return EXPRESION_PRIMA(actual.ChildNodes[1], exp, cant_tabs);

        }

        public Expresion EXPRELACIONAL_PRIMA(ParseTreeNode actual, Expresion izq, int cant_tabs)
        {
            /*
              EXPRELACIONAL_PRIMA.Rule
                = LESS + EXPRESION + EXPRELACIONAL_PRIMA
                | HIGHER + EXPRESION + EXPRELACIONAL_PRIMA
                | LESS_EQUAL + EXPRESION + EXPRELACIONAL_PRIMA
                | HIGHER_EQUAL + EXPRESION + EXPRELACIONAL_PRIMA
                | EQUALS + EXPRESION + EXPRELACIONAL_PRIMA
                | DISCTINCT + EXPRESION + EXPRELACIONAL_PRIMA
                | Empty
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text;

                var derecho = EXPRESION(actual.ChildNodes[1], cant_tabs);

                int row = actual.ChildNodes[0].Token.Location.Line;
                int col = actual.ChildNodes[0].Token.Location.Column;

                var relacional = new Relational(izq, derecho, simb, row, col, cant_tabs);

                return EXPRELACIONAL_PRIMA(actual.ChildNodes[2], relacional, cant_tabs);
            }
            return izq;
        }


        public Expresion TERMINO(ParseTreeNode actual, int cant_tabs)
        {
            //TERMINO.Rule = FACTOR + TERMINO_PRIMA;
            var fac = FACTOR(actual.ChildNodes[0], cant_tabs);
            return TERMINO_PRIMA(actual.ChildNodes[1], fac, cant_tabs);

        }

        public Expresion EXPRESION_PRIMA(ParseTreeNode actual, Expresion izq, int cant_tabs)
        {
            /*
             EXPRESION_PRIMA.Rule
                = PLUS + TERMINO + EXPRESION_PRIMA
                | MIN + TERMINO + EXPRESION_PRIMA
                ;
             */
            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text;

                var derecho = TERMINO(actual.ChildNodes[1], cant_tabs);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var aritmetica = new Arithmetic(izq, derecho, simb, row, col, cant_tabs);

                return EXPRESION_PRIMA(actual.ChildNodes[2], aritmetica, cant_tabs);

            }
            return izq;
        }

        public Expresion TERMINO_PRIMA(ParseTreeNode actual, Expresion izq, int cant_tabs)
        {
            /*
             TERMINO_PRIMA.Rule
                = POR + FACTOR + TERMINO_PRIMA
                | DIVI + FACTOR + TERMINO_PRIMA
                | MODULE + FACTOR + TERMINO_PRIMA
                | Empty
                ;
             */

            if (actual.ChildNodes.Count > 0)
            {
                var simb = actual.ChildNodes[0].Token.Text;
                var derecho = FACTOR(actual.ChildNodes[1], cant_tabs);
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;

                var aritmetica = new Arithmetic(izq, derecho, simb, row, col, cant_tabs);
                return TERMINO_PRIMA(actual.ChildNodes[2], aritmetica, cant_tabs);
            }
            return izq;
        }

        public Expresion FACTOR(ParseTreeNode actual, int cant_tabs)
        {

            /*
             FACTOR.Rule
                = PAR_IZQ + EXPLOGICA + PAR_DER
                | REAL
                | CADENA
                | NUMERO
                | IDENTIFIER + ID_TIPE
                | RESERV_TRUE
                | RESERV_FALSE
                | CALL_FUNCTION_PROCEDURE
                | MIN + FACTOR
                ;
            ID_TIPE.rule
                = [ exp ] + MORE_ACCES
                | Empty
                ;
             */



            if (actual.ChildNodes.Count == 3)
            {
                var izq = actual.ChildNodes[0];
                return getExpresion(actual.ChildNodes[1], cant_tabs);
            }
            else if (actual.ChildNodes.Count == 2)
            {

                if (actual.ChildNodes[0].Term.ToString().Equals("IDENTIFIER"))
                {
                    return getAccess(actual, cant_tabs);
                }
                else
                {
                    var simb = actual.ChildNodes[0].Token.Text;


                    if (simb.Equals("-"))
                    {
                        var iz = FACTOR(actual.ChildNodes[1], cant_tabs);
                        var row = actual.ChildNodes[0].Token.Location.Line;
                        var col = actual.ChildNodes[0].Token.Location.Column;

                        return new Arithmetic(new Literal("0", 1, row, col, cant_tabs), iz, "-", row, col, cant_tabs);
                    }
                }

            }
            else
            {
                //verifica que no sea una llamada a funcion
                var a = actual.ChildNodes[0].Term;
                if (a.ToString().Equals("CALL_FUNCTION_PROCEDURE"))
                {
                    return (new CallExpresionAST()).CALL(actual.ChildNodes[0], cant_tabs);
                }
                else
                {
                    return GetLiteral(actual.ChildNodes[0], cant_tabs);
                }

            }
            return null;
        }

        public Expresion getAccess(ParseTreeNode actual, int cant_tabs)
        {
            /*
                 ID_TIPE.rule
                = [ exp ] + MORE_ACCES
                | Empty
                ;
                 */
            if (actual.ChildNodes[1].ChildNodes.Count == 0)
            {
                return GetLiteral(actual.ChildNodes[0], cant_tabs);
            }

            
            return null;
        }

        
    }
}
