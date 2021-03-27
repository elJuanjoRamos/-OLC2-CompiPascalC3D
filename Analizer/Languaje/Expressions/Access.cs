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

        public string Id { get => id; set => id = value; }

        public Access(string id, int r, int c)
            : base("Access")
        {
            this.id = id;
            this.row = r;
            this.column = c;
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
                generator.get_stack(temp, variable.Position.ToString());

                if (variable.DataType!= DataType.BOOLEAN) {
                    return new Returned(temp, variable.DataType, true);
                }


                /*var retorno = new Returned("", variable.DataType, false);
                
                var trueLabel = generator.newLabel();
                var falseLabel = generator.newLabel();
                

                
                generator.add_If(temp, "1", "==", trueLabel);
                generator.add_Goto(falseLabel);

                retorno.TrueLabel = trueLabel;
                retorno.FalseLabel = falseLabel;
                return retorno;*/

            }
            else
            {
                var tempAux = generator.newTemporal(); 
                generator.freeTemp(tempAux);
                
                generator.addExpression(tempAux, "p", variable.Position.ToString(), "+");

                generator.get_stack(temp, tempAux);


                if (variable.DataType != DataType.BOOLEAN)
                {
                    return new Returned(temp, variable.DataType, true);
                }


                /*const retorno = new Retorno('', false, symbol.type);
                this.trueLabel = this.trueLabel == '' ? generator.newLabel() : this.trueLabel;
                this.falseLabel = this.falseLabel == '' ? generator.newLabel() : this.falseLabel;
                generator.addIf(temp, '1', '==', this.trueLabel);
                generator.addGoto(this.falseLabel);
                retorno.trueLabel = this.trueLabel;
                retorno.falseLabel = this.falseLabel;
                return retorno;*/
            }




            return new Returned();


        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
