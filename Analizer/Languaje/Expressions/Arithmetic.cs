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

            var arithmetic_string = varIz.Texto_anterior + valDer.Texto_anterior;
            var texto_original = varIz.Valor_original + type + valDer.Valor_original;

            var generator = C3DController.Instance;


            if (varIz.getDataType == DataType.INTEGER || varIz.getDataType == DataType.REAL)
            {

                if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                {
                    var temp = generator.newTemporal();
                    arithmetic_string += generator.addExpression(temp, varIz.Value, valDer.Value, type, cant_tabs);

                    if (valDer.getDataType == DataType.REAL || varIz.getDataType == DataType.REAL)
                    {
                        return new Returned(temp, DataType.REAL, true, arithmetic_string, texto_original,0);
                    }
                    return new Returned(temp, DataType.INTEGER, true, arithmetic_string, texto_original,0);

                }
                set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                return new Returned();
            }
            else if (varIz.getDataType == DataType.STRING && valDer.getDataType == DataType.STRING)
            {

                arithmetic_string += generator.addExpression("T9", varIz.Value.ToString(), "", "", cant_tabs);
                arithmetic_string += generator.addExpression("T10", valDer.Value.ToString(), "", "", cant_tabs);
                arithmetic_string += generator.save_code("native_concat_str();", cant_tabs);
                return new Returned("T12", DataType.STRING, true, arithmetic_string, texto_original,0);

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
