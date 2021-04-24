using CompiPascalC3D.Optimize.Languaje.Labels;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.AST
{
    class LabelOptimize
    {
        public LabelOptimize()
        {

        }

        public SetLabel GetSetLabel(ParseTreeNode actual)
        {
            var label = (new LiteralOptimize()).getLabel(actual.ChildNodes[0]);

            return new SetLabel(label);
        }
    }
}
