using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Jumps;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.If
{
    class IF : Instruction
    {
        private Literal left;
        private Literal right;
        private string simbol;
        private Label label;
        private bool regla2;
        private bool regla3;
        private bool regla4;
        private bool isEmpty;
        private int row;
        private int column;

        public IF(Literal left, Literal right, string simbol, Label label, int row, int col)
            : base("If")
        {
            this.left = left;
            this.right = right;
            this.simbol = simbol;
            this.label = label;
            this.regla2 = false;
            this.regla3 = false;
            this.regla4 = false;
            this.isEmpty = false;
            this.row = row;
            this.column = col;
        }
        public IF()
        {
            this.isEmpty = true;
        }

        public Label Label { get => label; set => label = value; }
        public bool Regla2 { get => regla2; set => regla2 = value; }
        public bool Regla3 { get => regla3; set => regla3 = value; }
        public bool IsEmpty { get => isEmpty; set => isEmpty = value; }
        public int Column { get => column; set => column = value; }
        public int Row { get => row; set => row = value; }
        internal Literal Left { get => left; set => left = value; }
        internal Literal Right { get => right; set => right = value; }
        public bool Regla4 { get => regla4; set => regla4 = value; }

        public override string Code()
        {
            return "if(" + left.Value + simbol + right.Value + ") goto " + label.Name + ";\n"; 
        }

        public override object Optimize()
        {
            if (regla2)
            {
                this.simbol = get_simbolo_contrario(simbol);
                return this;
            }
            else if (regla3)
            {
                return new Goto(Label, row, column);
            }
            else if (regla4)
            {
                return new Goto(Label, row, column);
            }
            return new IF();
        }

        public bool evaluate_condition()
        {
            switch (simbol)
            {
                case "<":
                    return (Double.Parse(left.Value) < Double.Parse(right.Value));
                case ">":
                    return (Double.Parse(left.Value) > Double.Parse(right.Value));
                case ">=":
                    return (Double.Parse(left.Value) <= Double.Parse(right.Value));
                case "<=":
                    return (Double.Parse(left.Value) >= Double.Parse(right.Value));
                case "==":
                    return (Double.Parse(left.Value) == Double.Parse(right.Value));
                case "!=":
                    return (Double.Parse(left.Value) != Double.Parse(right.Value));
                default:
                    return false;
            }
        }

        public string get_simbolo_contrario(string simbolo)
        {
            switch (simbolo)
            {
                case "<":
                    return ">";
                case ">":
                    return "<";
                case ">=":
                    return "<=";
                case "<=":
                    return ">=";
                case "==":
                    return "!=";
                case "!=":
                    return "==";
                default:
                    return simbolo;
            }
        }
    }
}
