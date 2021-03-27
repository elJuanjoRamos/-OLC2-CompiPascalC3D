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

        public Relational(Expresion left, Expresion right, string type, int r, int c)
            : base("Relational")
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.row = r;
            this.column = c;
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



                    var trueLabel = generator.newLabel();
                    var falseLabel = generator.newLabel();

                    generator.add_If(valIz.getValue(), valDer.getValue(), this.type, trueLabel);
                    generator.add_Goto(falseLabel);

                    result = new Returned("", DataType.BOOLEAN, false, trueLabel, falseLabel);

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

        public OpRelational GetType(string simb)
        {
            if (simb.Equals("<"))
            {
                return OpRelational.LESS;
            }
            else if (simb.Equals(">="))
            {
                return OpRelational.HIGHER_EQUALS;
            }
            else if (simb.Equals(">"))
            {
                return OpRelational.HIGHER;
            }
            else if (simb.Equals("<="))
            {
                return OpRelational.LESS_EQUALS;
            }
            else if (simb.Equals("<>"))
            {
                return OpRelational.DISCTINCT;
            }

            return OpRelational.EQUALS;
        }
        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
