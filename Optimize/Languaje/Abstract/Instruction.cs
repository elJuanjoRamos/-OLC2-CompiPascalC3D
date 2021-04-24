using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Abstract
{
    public abstract class Instruction
    {
        private string name;
        public Instruction(string name)
        {
            this.Name = name;
        }
        public Instruction()
        {
        }

        public string Name { get => name; set => name = value; }
        public abstract object Optimize();
        public abstract string Code();
    }
}
