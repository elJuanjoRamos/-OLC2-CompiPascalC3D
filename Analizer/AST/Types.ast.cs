using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences.Array;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using Array = CompiPascalC3D.Analizer.Languaje.Sentences.Array.Array;

namespace CompiPascalC3D.Analizer.AST
{
    class TypeAST
    {
        public TypeAST()
        {

        }
        ExpresionAST expressionAST = new ExpresionAST();

        public LinkedList<Instruction> TYPE_LIST(ParseTreeNode actual, LinkedList<Instruction> lista_actual, int cant_tabs)
        {

            /*
             TYPE_LIST.Rule
                = TYPE + TYPE_LIST
                | Empty
                ;             
             */
            if (actual.ChildNodes.Count > 0)
            {
                lista_actual = TYPE(actual.ChildNodes[0], lista_actual, cant_tabs);
                lista_actual = TYPE_LIST(actual.ChildNodes[1], lista_actual, cant_tabs);
            }


            return lista_actual;
        }

        public LinkedList<Instruction> TYPE(ParseTreeNode actual, LinkedList<Instruction> lista_actual, int cant_tabs)
        {
            /*
              TYPE.Rule = RESERV_TYPE + IDENTIFIER_ARRAY_TYPE + EQUALS + TYPE_P;
             */
            var identifier = actual.ChildNodes[1].Token.Text;
            var element = TYPE_P(identifier, actual.ChildNodes[3], cant_tabs);
            lista_actual.AddLast(element);

            return lista_actual;
        }

        public Instruction TYPE_P(string name, ParseTreeNode actual, int cant_tabs)
        {

            /*
             TYPE_P.Rule 
                = OBJECT
                | ARRAY
                ;
             */

            var element = actual.ChildNodes[0].ChildNodes[0].Token.Text;
            if (element.Equals("array"))
            {
                var result = ARRAYs(actual.ChildNodes[0], name, cant_tabs);
                return result;
            }
            else
            {

            }
            return null;
        }

        
        public Instruction ARRAYs(ParseTreeNode actual, string name, int cant_tabs)
        {

            /*
              ARRAY.Rule =   
            RESERV_ARRAY  + COR_IZQ + EXPLOGICA + PUNTO + PUNTO + EXPLOGICA + MORE_ARRAY 
            + COR_DER + RESERV_OF + DATA_TYPE + PUNTO_COMA;
             */

            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;


            var limit_inf = expressionAST.getExpresion(actual.ChildNodes[2], cant_tabs);
            var limit_sup = expressionAST.getExpresion(actual.ChildNodes[5], cant_tabs);


            LinkedList<Dimention> lista_actual = new LinkedList<Dimention>();
            lista_actual.AddLast(new Dimention(limit_inf, limit_sup, row, col, cant_tabs, 1));
            lista_actual = MORE_ARRAY(actual.ChildNodes[6], lista_actual, cant_tabs, 2);

            var data_type = GetDataType(actual.ChildNodes[9].ChildNodes[0].ToString());

            return new Array(name, lista_actual, data_type, row, col, cant_tabs);

        }

        public LinkedList<Dimention> MORE_ARRAY(ParseTreeNode actual, LinkedList<Dimention> lista_actual, int cant_tabs, int size)
        {

            /*
               MORE_ARRAY.Rule
                = COMA + EXPLOGICA + PUNTO + PUNTO + EXPLOGICA + MORE_ARRAY
                | Empty
                ;
             */


            if (actual.ChildNodes.Count > 0)
            {
                var row = actual.ChildNodes[0].Token.Location.Line;
                var col = actual.ChildNodes[0].Token.Location.Column;
                var inf = expressionAST.getExpresion(actual.ChildNodes[1], cant_tabs);
                var sup = expressionAST.getExpresion(actual.ChildNodes[4], cant_tabs);
                lista_actual.AddLast(new Dimention(inf, sup, row, col, cant_tabs, size));
                lista_actual = MORE_ARRAY(actual.ChildNodes[5], lista_actual, cant_tabs, size+1);
            }

            return lista_actual;
        }

        public DataType GetDataType(string d)
        {
            if (d.Equals("integer"))
            {
                return DataType.INTEGER;
            }
            else if (d.Equals("boolean"))
            {
                return DataType.BOOLEAN;
            }
            else if (d.Equals("real"))
            {
                return DataType.REAL;
            }
            return DataType.STRING;

        }
    }
}
