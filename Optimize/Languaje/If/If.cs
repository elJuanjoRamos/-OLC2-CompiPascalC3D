using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.If
{
    class If : Instruction
    {
        private Literal left;
        private Literal right;
        private string simbol;
        private Label label;

        public If(Literal left, Literal right, string simbol, Label label)
            : base("If")
        {
            this.left = left;
            this.right = right;
            this.simbol = simbol;
            this.label = label;
        }

        public override object Optimize()
        {
            return "";
        }
    }
}
