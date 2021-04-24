using CompiPascalC3D.Optimize.Languaje.Jumps;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    public class GotoOptimize
    {
        LiteralOptimize LiteralOptimize = new LiteralOptimize();
    
        public GotoOptimize()
        {

        }


        public Goto GetGoto(ParseTreeNode actual)
        {
            var label = actual.ChildNodes[1];
            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;
            return new Goto(LiteralOptimize.getLabel(label), row, col);
        }
    }
}
