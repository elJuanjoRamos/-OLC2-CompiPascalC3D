using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Heap_and_Stack
{
    class Access : Instruction
    {

        private Literal temp;
        private string dataStructure;
        private Literal index;

        public Access(Literal temp, string dataStructure, Literal index)
            : base("Access")
        {
            this.temp = temp;
            this.dataStructure = dataStructure;
            this.index = index;
        }

        public override string Code()
        {
            return temp.Value + " = " + dataStructure + "[(int)" + index.Value + "];\n";
        }

        public override object Optimize()
        {
            return this;
        }
    }
}
