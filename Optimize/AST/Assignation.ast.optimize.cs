using CompiPascalC3D.Optimize.Languaje.Heap_and_Stack;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class AssignationOptimize
    {
        LiteralOptimize LiteralOptimize = new LiteralOptimize();
        public AssignationOptimize()
        {
        }

        public Assignation GetAssignation(ParseTreeNode actual)
        {
            //RESERV_STACK + COR_IZQ + PAR_IZQ + RESERV_INT + PAR_DER + TERMINAL + COR_DER + EQUALS + TERMINAL + PUNTO_COMA
            var structure = actual.ChildNodes[0].Token.Text;
            var row = actual.ChildNodes[0].Token.Location.Line;
            var col = actual.ChildNodes[0].Token.Location.Column;
            var index = LiteralOptimize.getLiteral(actual.ChildNodes[5]);
            var data = LiteralOptimize.getLiteral(actual.ChildNodes[8]);

            return new Assignation(data, structure, index, row, col);
        }
    }
}
