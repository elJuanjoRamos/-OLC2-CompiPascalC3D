using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences.Array
{
    class DeclarationArray : Instruction
    {
        private string id;
        private string array;
        private int row;
        private int column;
        private int cant_tabs;
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public DeclarationArray(string id, String dataType, int r, int c, int canttabs)
            : base("Declaration")
        {
            this.id = id;
            this.array = dataType;
            this.row = r;
            this.column = c;
            this.cant_tabs = canttabs;
        }

        public override object Execute(Ambit ambit)
        {
            var array_str = "";
            Arrays ar = ambit.getArray(array);
            if (ar == null)
            {
                set_error("El arreglo '" + array + "' no ha sido declarado", row, column);
                return null;
            }

            var arreglo = ambit.getArray(id);
            if (arreglo != null)
            {
                set_error("El arreglo '" + id + "' ya fue declarado", row, column);
                return null;
            }
            var this_array = new Arrays(id, ar.Dimensiones, ar.DataType, Row, Column, cant_tabs);
            ambit.saveArray(id, this_array);

            //////////////////////////////////////////////







            return array_str;
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
