using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Jumps
{
    public class Goto : Instruction
    {
        private Label label;
        private int row;
        private int column;
        private bool isEmpty;

        public Goto(Label lb, int r, int c)
            : base("Goto")
        {
            this.label = lb;
            this.row = r;
            this.column = c;
            this.isEmpty = false;
        }
        public Goto()
        {
            this.isEmpty = true;
        }

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        internal Label Label { get => label; set => label = value; }

        public override string Code()
        {
            return "goto " + label.Name + ";\n";
        }

        public override object Optimize()
        {
            return this;
        }
    }
}
