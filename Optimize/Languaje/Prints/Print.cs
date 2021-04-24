using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Prints
{
    class Print : Instruction
    {
        private string formato;
        private Literal literal;
        private int row;
        private int column;
        private bool isEmpty;

        public bool IsEmpty { get => isEmpty; set => isEmpty = value; }

        public Print(string cadena, Literal literal, int row, int column)
        {
            this.formato = cadena;
            this.literal = literal;
            this.row = row;
            this.column = column;
            this.isEmpty = false;
        }
        public Print()
        {
            this.isEmpty = true;
        }
        public override string Code()
        {
            var texto = (literal.IsString) ? literal.Value : "(int)" + literal.Value;
            
            return "printf(" + formato + "," + texto + ");\n";
        }

        public override object Optimize()
        {
            return this;
        }
    }
}
