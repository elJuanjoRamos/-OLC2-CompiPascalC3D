using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Jumps
{
    class Goto : Instruction
    {
        private Label label;

        public Goto(Label lb)
        {
            this.label = lb;
        }
        internal Label Label { get => label; set => label = value; }

        public override object Optimize()
        {
            return "goto " + label.Name;
        }
    }
}
