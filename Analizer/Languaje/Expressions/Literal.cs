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
        public object Value { get => value; set => this.value = value; }
        public int Type { get => type; set => type = value; }

        public override Returned Execute(Ambit ambit)
        {
            var literal_string = "";
            var generator = C3D.C3DController.Instance;
            var returned = new Returned();
            if (this.type == 1)
            {
                returned = new Returned(this.value.ToString(), DataType.INTEGER, false, "","", literal_string,  this.value.ToString(),0,0);
            }
            else if (this.type == 2)
            {
                var temp = generator.newTemporal();
                ambit.set_temp(temp);
                literal_string += generator.addExpression(temp, "HP", "", "", cant_tabs);
                foreach (char cha in this.value.ToString())
                {
                    literal_string += generator.set_Heap("HP", ((int)cha).ToString(), cant_tabs);
                    literal_string += generator.next_Heap(cant_tabs);
                }
                literal_string += generator.set_Heap("HP", "-1", cant_tabs);
                literal_string += generator.next_Heap(cant_tabs);

                returned = new Returned(temp, DataType.STRING, true, "","", literal_string, this.value.ToString(),0,0);
            }

            else if (this.type == 3)
            {

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
                    literal_string += generator.add_Goto(this.FalseLabel, cant_tabs);
                    returned = new Returned("false", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel, literal_string, "false", 0,0);
                }
                else
                {
                    literal_string += generator.add_Goto(this.TrueLabel, cant_tabs);
                    returned = new Returned("true", DataType.BOOLEAN, false,  this.TrueLabel, this.FalseLabel, literal_string, "true", 0,0);
                }
            }
            else if (this.type == 4)
            {
                returned = new Returned(this.value.ToString(), DataType.REAL, false, "","", literal_string, this.value.ToString(),0,0);
            }

            else if (this.type == 7)
            {
                returned = new Returned(this.value.ToString(), DataType.IDENTIFIER, false,"","", literal_string, this.value.ToString(),0,0);
            }
            return returned;

        }
    }
}
