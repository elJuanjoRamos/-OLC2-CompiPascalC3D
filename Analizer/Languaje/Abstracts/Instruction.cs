using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Analizer.Languaje.Ambits;
namespace CompiPascalC3D.Analizer.Languaje.Abstracts
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
        public abstract object Execute(Ambit ambit);
    }
}
