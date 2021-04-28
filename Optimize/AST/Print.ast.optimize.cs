using CompiPascalC3D.Optimize.Languaje.Prints;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class PrintOptimize
    {
        public PrintOptimize()
        {

        }
        LiteralOptimize LiteralOptimize = new LiteralOptimize();
        string tipo = "";
        public Print GetPrint(ParseTreeNode actual)
        {
            int row = actual.ChildNodes[0].Token.Location.Line;
            int col = actual.ChildNodes[0].Token.Location.Column;

            var cadena = actual.ChildNodes[2].Token.Text;
            var literal = PRINT_TERM(actual.ChildNodes[4]);

            return new Print(cadena, tipo, literal, row, col);
        }

        public Literal PRINT_TERM(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 1)
            {
                return new Literal(actual.ChildNodes[0].Token.Text);
            }
            tipo = actual.ChildNodes[1].Token.Text;
            return LiteralOptimize.getLiteral(actual.ChildNodes[3]);
        }
    }
}
