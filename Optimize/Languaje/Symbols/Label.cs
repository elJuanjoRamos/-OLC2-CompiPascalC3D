using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Symbols
{
    public class Label
    {
        private string name;
        public Label(string lb)
        {
            this.name = lb;
        }
        public string Name { get => name; set => name = value; }
    }
}
