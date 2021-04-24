using CompiPascalC3D.Optimize.Languaje.Arithmetics;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class ArithmeticOptimizer
    {
        public ArithmeticOptimizer()
        {

        }
        LiteralOptimize LiteralOptimize = new LiteralOptimize();


        public object GetArithmetic(ParseTreeNode actual)
        {
            var data = LiteralOptimize.getLiteral(actual.ChildNodes[0]);
            int row = actual.ChildNodes[1].Token.Location.Line;
            int col = actual.ChildNodes[1].Token.Location.Column;

            if (actual.ChildNodes[2].ChildNodes.Count == 3)
            {
                var expresion = actual.ChildNodes[2];

                var izquierdo = LiteralOptimize.getLiteral(expresion.ChildNodes[0]);
                var derecho = LiteralOptimize.getLiteral(expresion.ChildNodes[2]);
                var simb = expresion.ChildNodes[1].Token.Text;

                return new Expresion(data.Value, izquierdo, derecho, simb, row, col);

            }

            return null;
        }

        
    }
}
