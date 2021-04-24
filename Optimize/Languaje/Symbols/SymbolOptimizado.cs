using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Symbols
{
    class SymbolOptimizado
    {
        private string tipo;
        private string regla;
        private string codigo_eliminado;
        private string codigo_agregado;
        private string ambit;
        private int row;
        private int column;

        public SymbolOptimizado(string tipo, string regla, string codigo_eliminado, string codigo_agregado, int row, int column, string ambit)
        {
            this.tipo = tipo;
            this.regla = regla;
            this.codigo_eliminado = codigo_eliminado;
            this.codigo_agregado = codigo_agregado;
            this.row = row;
            this.column = column;
            this.ambit = ambit;
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public string Regla { get => regla; set => regla = value; }
        public string Codigo_eliminado { get => codigo_eliminado; set => codigo_eliminado = value; }
        public string Codigo_agregado { get => codigo_agregado; set => codigo_agregado = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public string Ambit { get => ambit; set => ambit = value; }
    }
}
