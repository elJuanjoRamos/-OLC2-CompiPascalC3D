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
        
        public If()
            : base("If")
        {

        }
        public override object Optimize()
        {
            return "";
        }
    }
}
