using CompiPascalC3D.Optimize.Languaje.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Function
{
    class Call : Instruction
    {
        private string id;
        private int row;
        private int column;

        public Call(string id, int row, int column)
            : base("Call")
        {
            this.id = id;
            this.row = row;
            this.column = column;
        }


        public override string Code()
        {
            return  id+"();";
        }

        public override object Optimize()
        {
            return this;
        }
    }
}
