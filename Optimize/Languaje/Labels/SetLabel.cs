using System;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;

namespace CompiPascalC3D.Optimize.Languaje.Labels
{
    class SetLabel : Instruction
    {
        private Label label;


        public SetLabel(Label label)
            : base("SetLabel")
        {
            this.label = label;
        }

        public Label Label { get => label; set => label = value; }

        public override string Code()
        {
            return label.Name + ":\n";
        }

        public override object Optimize()
        {
            return "";
        }
    }
}
