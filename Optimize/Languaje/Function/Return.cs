using CompiPascalC3D.Optimize.Languaje.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Function
{
    class Return : Instruction
    {
        private string value;
        private bool isEmpty;
        private int row;
        private int column;

        public Return(string value, int row, int column)
            : base("Return")
        {
            this.value = value;
            this.row = row;
            this.column = column;
            this.isEmpty = false;
        }

        public Return()
            : base("Return")
        {
            this.value = "";
            this.isEmpty = true;
        }
        public bool IsEmpty { get => isEmpty; set => isEmpty = value; }

        public override string Code()
        {
            if (value != "")
            {
                return "return " + value + ";";
            }

            return "return;";
        }

        public override object Optimize()
        {
            return this;
        }
    }
}
