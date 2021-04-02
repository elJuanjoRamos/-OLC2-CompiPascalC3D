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
        private int cant_tabs;
        public Literal(Object v, int t, int r, int c, int ct) :
            base("Literal")
        {
            this.value = v;
            this.type = t;
            this.isNull = false;
            this.row = r;
            this.column = c;
            this.cant_tabs = ct;
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

                var generator = C3D.C3DController.Instance;

                if (this.TrueLabel == "")
                {
                    this.TrueLabel = generator.newLabel();
                }
                if (this.FalseLabel == "")
                {
                    this.FalseLabel = generator.newLabel();
                }

                if (this.value.ToString() == "false")
                {
                    generator.add_Goto(this.FalseLabel, cant_tabs+1);
                    returned = new Returned("false", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel);
                }
                else
                {
                    generator.add_Goto(this.TrueLabel, cant_tabs+1);
                    returned = new Returned("true", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel);
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
