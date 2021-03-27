using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Expressions
{
    class Relational : Expresion
    { 
        private Expresion left;
        private Expresion right;
        private string type;
        private int row;
        private int column;
        private int cant_tabs;

        public Relational(Expresion left, Expresion right, string type, int r, int c, int cantTabs)
            : base("Relational")
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.row = r;
            this.column = c;
            this.cant_tabs = cantTabs;
        }

        public override Returned Execute(Ambit ambit)
        {
            var valIz = this.left.Execute(ambit);
            var valDer = this.right.Execute(ambit);
            var result = new Returned();




            if (valIz.getDataType == DataType.REAL || valIz.getDataType == DataType.INTEGER)
            {
                if (valDer.getDataType == DataType.REAL || valDer.getDataType == DataType.INTEGER)
                {
                    var generator = C3DController.Instance;

                    if (this.TrueLabel == "")
                    {
                        this.TrueLabel = generator.newLabel();
                    }
                    if (this.FalseLabel == "")
                    {
                        this.FalseLabel = generator.newLabel();
                    }

                    generator.add_If(valIz.getValue(), valDer.getValue(), GetType(this.type), this.TrueLabel, cant_tabs);
                    generator.add_Goto(this.FalseLabel, cant_tabs);

                    result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel);

                } else
                {
                    set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                }

            } else
            {
                set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
            }
            return result;
        }

        public string GetType(string simb)
        {
            if (simb.Equals("<>"))
            {
                return "!=";
            }
            else if (simb.Equals("="))
            {
                return "==";
            } 
            return simb;
        }
        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
