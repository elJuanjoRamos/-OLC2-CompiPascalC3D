using CompiPascalC3D.Optimize.Languaje.Function;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class ReturnOptimize
    {
        public ReturnOptimize()
        {

        }
        LiteralOptimize LiteralOptimize = new LiteralOptimize();
        public Return GetReturn(ParseTreeNode actual)
        {
            int row = actual.ChildNodes[0].Token.Location.Line;
            int col = actual.ChildNodes[0].Token.Location.Column;
            var retorno = "";
            if (actual.ChildNodes.Count == 3)
            {
                var resul = LiteralOptimize.getLiteral(actual.ChildNodes[1]);
                retorno = resul.Value;
            }

            return new Return(retorno, row, col);
        }
    }
}
