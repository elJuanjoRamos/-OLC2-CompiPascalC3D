using CompiPascalC3D.Analizer.Languaje.Sentences;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.AST
{
    class ExitAST
    {
        public ExitAST()
        {

        }

        public Exit getExit(ParseTreeNode actual, int cant_tabs)
        {
            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;
            var param = actual.ChildNodes[2];
            if (param.ChildNodes.Count != 0)
            {
                var exp = (new ExpresionAST()).getExpresion(param.ChildNodes[0], cant_tabs);

                return new Exit(exp, row, col, cant_tabs);
            }

            return new Exit();
        }
    }
}
