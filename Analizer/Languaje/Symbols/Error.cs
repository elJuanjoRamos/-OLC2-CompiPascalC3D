using CompiPascalC3D.Analizer.Languaje.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Symbols
{
    class Error
    {
        private string message;
        private int row;
        private int column;
        

        public string Message { get => message; set => message = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }


        public Error(string message, int row, int column)
        {
            this.message = message;
            this.row = row;
            this.column = column;
        }
        public string toString()
        {
            return " - En la linea " + this.row + ", columna " + this.column + " - Mensaje: " + this.message;
        }
    }
}
