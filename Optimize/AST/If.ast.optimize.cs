using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.If;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class IfOptimize
    {
        public IfOptimize()
        {

        }
        LiteralOptimize LiteralOptimize = new LiteralOptimize();

        public IF IFTHEN(ParseTreeNode actual, int cant_tabs)
        {
            /*
             IF.Rule =
                RESERV_If + PAR_IZQ + TERMINAL + SIMB + TERMINAL + PAR_DER + RESERV_GOTO + LABEL + PUNTO_COMA;
             */
            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;

            var izq = LiteralOptimize.getLiteral(actual.ChildNodes[2]);
            var der = LiteralOptimize.getLiteral(actual.ChildNodes[4]);
            var lb = LiteralOptimize.getLabel(actual.ChildNodes[7]);
            var simb = get_simb(actual.ChildNodes[3]);

            return new IF(izq, der, simb, lb, row, col);
        }


        public string get_simb(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count > 1)
            {
                return "==";
            }
            return actual.ChildNodes[0].Token.Text;
        }
    }
}
