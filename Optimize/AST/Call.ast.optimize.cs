using CompiPascalC3D.Optimize.Languaje.Function;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class CallOptimize
    {
        public CallOptimize()
        {

        }

        public Call GetCall(ParseTreeNode actual)
        {
            var id = actual.ChildNodes[0].Token.Text;
            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            return new Call(id, row, col);
        }
    }
}
