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
    class Arithmetic : Expresion
    {
        private Expresion left;
        private Expresion right;
        private string type;
        private int row;
        private int column;
        private int cant_tabs;
        public Arithmetic(Expresion l, Expresion ri, String t, int r, int c, int ct)
       : base("Arithmetic")
        {
            this.right = ri;
            this.left = l;
            this.type = t;
            this.row = r;
            this.column = c;
            this.cant_tabs = ct;
        }

        public override Returned Execute(Ambit ambit)
        {
            var varIz = this.left.Execute(ambit);
            var valDer = this.right.Execute(ambit);

            var generator = C3DController.Instance;

            var temp = generator.newTemporal();

            if (varIz.getDataType == DataType.INTEGER || varIz.getDataType == DataType.REAL)
            {
                if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                {
                    generator.addExpression(temp, varIz.Value, valDer.Value, type, cant_tabs);

                    if (valDer.getDataType == DataType.REAL || varIz.getDataType == DataType.REAL)
                    {
                        return new Returned(temp, DataType.REAL, true);
                    }
                    return new Returned(temp, DataType.INTEGER, true);

                }
                set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                return new Returned();
            }
            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
            return new Returned();

        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
