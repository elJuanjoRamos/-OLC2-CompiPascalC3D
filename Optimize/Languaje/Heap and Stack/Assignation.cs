using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Heap_and_Stack
{
    class Assignation : Instruction
    {
        private Literal temp;
        private string dataStructure;
        private Literal index;

        public Assignation(Literal temp, string dataStructure, Literal index)
            : base("Assignation")
        {
            this.temp = temp;
            this.dataStructure = dataStructure;
            this.index = index;
        }

        public override string Code()
        {
            return dataStructure + "[(int)" + index.Value + "] = " + temp.Value;
        }

        public override object Optimize()
        {
            return this;
        }
    }
}
