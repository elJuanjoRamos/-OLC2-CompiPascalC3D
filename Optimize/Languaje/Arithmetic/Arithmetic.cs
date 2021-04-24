using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Arithmetic
{
    class Arithmetic : Instruction
    {
        private string temp;
        private Literal left;
        private Literal right;
        private string simbol;

        public Arithmetic(string temp, Literal left, Literal right, string simbol)
            : base("Aritmetica")
        {
            this.temp = temp;
            this.left = left;
            this.right = right;
            this.simbol = simbol;
        }

        public override object Optimize()
        {
            return "";
        }
    }
}
