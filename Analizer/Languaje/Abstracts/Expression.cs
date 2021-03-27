using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Abstracts
{
    public abstract class Expresion
    {
        
        public string name;
        private string trueLabel;
        private string falseLabel;

        public string TrueLabel { get => trueLabel; set => trueLabel = value; }
        public string FalseLabel { get => falseLabel; set => falseLabel = value; }

        public Expresion(string n)
        {
            this.name = n;
            this.trueLabel = this.falseLabel = "";
        }
        public Expresion()
        {
            this.trueLabel = this.falseLabel = "";
        }

        public abstract Returned Execute(Ambit ambit);
    }
}
