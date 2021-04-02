using CompiPascalC3D.Analizer.Languaje.Abstracts;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class WriteAST
    {
        //VARIABLES
        ExpresionAST ExpresionAST = new ExpresionAST();
        public WriteAST()
        {

        }

        #region WRITE
        public LinkedList<Expresion> WRITES(ParseTreeNode actual, int cant_tabs)
        {
            LinkedList<Expresion> list = new LinkedList<Expresion>();
            if (actual.ChildNodes.Count > 0)
            {
                var exp = ExpresionAST.getExpresion(actual.ChildNodes[0], cant_tabs);
                list.AddLast(exp);
                list = WRHITE_PARAMETER(actual.ChildNodes[1], list, cant_tabs);

            }
            return list;
        }
        public LinkedList<Expresion> WRHITE_PARAMETER(ParseTreeNode actual, LinkedList<Expresion> list, int cant_tabs)
        {
            if (actual.ChildNodes.Count > 0)
            {
                var exp = ExpresionAST.getExpresion(actual.ChildNodes[1], cant_tabs);
                list.AddLast(exp);
                list = WRHITE_PARAMETER(actual.ChildNodes[2], list, cant_tabs);

            }
            return list;
        }
        #endregion
    }
}
