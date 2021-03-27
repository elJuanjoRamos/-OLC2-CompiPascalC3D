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
        public Expresion(string n)
        {
            //this.row = r;
            //this.column = c;
            this.name = n;
        }
        public Expresion()
        {
        }

        public abstract Returned Execute(Ambit ambit);
    }
}
