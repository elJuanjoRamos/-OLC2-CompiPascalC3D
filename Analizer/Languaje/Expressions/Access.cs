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
    class Access : Expresion
    {
        private string id;
        public int row;
        public int column;
        private int cant_Tabs;
        public string Id { get => id; set => id = value; }

        public Access(string id, int r, int c, int ct)
            : base("Access")
        {
            this.id = id;
            this.row = r;
            this.column = c;
            this.cant_Tabs = ct;
        }

        public override Returned Execute(Ambit ambit)
        {
            var generator = C3DController.Instance;

            Identifier variable = ambit.getVariable(this.id);
            if (variable.IsNull)
            {
                set_error("La variable '" + this.id + "' no ha sido declarada", row, column);
                return new Returned();
            }


            var temp = generator.newTemporal();
            if (variable.IsGlobal)
            {
                generator.get_stack(temp, variable.Position.ToString(), cant_Tabs);

                if (variable.DataType!= DataType.BOOLEAN) {
                    return new Returned(temp, variable.DataType, true);
                }

                
                if (this.TrueLabel == "")
                {
                    this.TrueLabel = generator.newLabel();
                }
                if (this.FalseLabel == "")
                {
                    this.FalseLabel = generator.newLabel();
                }
                generator.add_If(temp, "1", "==", this.TrueLabel, cant_Tabs);
                generator.add_Goto(this.FalseLabel, cant_Tabs);

                return new Returned("", variable.DataType, false, this.TrueLabel, this.FalseLabel);


            }
            else
            {
                var tempAux = generator.newTemporal(); 
                generator.freeTemp(tempAux);
                
                generator.addExpression(tempAux, "p", variable.Position.ToString(), "+", cant_Tabs);

                generator.get_stack(temp, tempAux, cant_Tabs);


                if (variable.DataType != DataType.BOOLEAN)
                {
                    return new Returned(temp, variable.DataType, true);
                }


                if (this.TrueLabel == "")
                {
                    this.TrueLabel = generator.newLabel();
                }
                if (this.FalseLabel == "")
                {
                    this.FalseLabel = generator.newLabel();
                }

                generator.add_If(temp, "1", "==", this.TrueLabel, cant_Tabs);
                generator.add_Goto(this.FalseLabel, cant_Tabs);
                return new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel);
            }
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
