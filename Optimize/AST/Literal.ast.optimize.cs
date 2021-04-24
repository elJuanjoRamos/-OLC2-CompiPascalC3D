using CompiPascalC3D.Optimize.Languaje.Symbols;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class LiteralOptimize
    {
        public LiteralOptimize()
        {

        }

        public Literal getLiteral(ParseTreeNode actual)
        {

            if (actual.ChildNodes.Count > 1)
            {
                var numero = actual.ChildNodes[1].Token.Text;
                return new Literal("-"+numero, true, false, false);
            }

            var temp = actual.ChildNodes[0].Token.Text;
            int n;
            bool isNumeric = int.TryParse(temp, out n);
            bool isTemp = false;
            bool isPointer = false;
            if (isNumeric)
            {
                temp = n.ToString();
            }
            else if (temp.ToLower().Equals("hp") || temp.ToLower().Equals("sp"))
            {
                isPointer = true;
            }
            else
            {
                isTemp = true;
            }


            return new Literal(temp, isNumeric, isTemp, isPointer);
        }
        

        public Label getLabel(ParseTreeNode actual)
        {
            var name = actual.Token.Text;

            return new Label(name);
        }

    }
}
