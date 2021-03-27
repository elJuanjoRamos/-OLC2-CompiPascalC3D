using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Expressions
{
    class Literal : Expresion
    {
        private Object value;
        private int type;
        private bool isNull;
        private int row;
        private int column;
        public Literal(Object v, int t, int r, int c) :
            base("Literal")
        {
            this.value = v;
            this.type = t;
            this.isNull = false;
            this.row = r;
            this.column = c;

        }
        public Literal(int r, int c) :
            base("Literal")
        {
            this.isNull = true;
            this.row = r;
            this.column = c;
        }

      
        public bool IsNull { get => isNull; set => isNull = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public override Returned Execute(Ambit ambit)
        {
            var returned = new Returned();
            if (this.type == 1)
            {
                returned = new Returned(this.value.ToString(), DataType.INTEGER, false);
            }
            else if (this.type == 2)
            {
                returned = new Returned(this.value.ToString(), DataType.STRING, false);
            }

            else if (this.type == 3)
            {
                if (this.value.ToString() == "false")
                {
                    returned = new Returned("false", DataType.BOOLEAN, false);
                }
                else
                {
                    returned = new Returned("true", DataType.BOOLEAN, false);
                }
            }
            else if (this.type == 4)
            {
                returned = new Returned(this.value.ToString(), DataType.REAL, false);
            }

            else if (this.type == 7)
            {
                returned = new Returned(this.value.ToString(), DataType.IDENTIFIER, false);
            }
            return returned;

        }
    }
}
