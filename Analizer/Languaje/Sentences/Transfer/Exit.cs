using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Exit : Instruction
    {
        private Expresion value;
        private bool return_func_return;
        private int row;
        private int column;
        private int cant_tabs;

        public Expresion Value { get => value; set => this.value = value; }
        public bool Return_func_return { get => return_func_return; set => return_func_return = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public int Cant_tabs { get => cant_tabs; set => cant_tabs = value; }

        public Exit(Expresion value, int row, int col, int cant) :
            base("Exit")
        {
            this.value = value;
            this.return_func_return = false;
            this.row = row;
            this.column = col;
            this.cant_tabs = cant;
        }

        public Exit() :
            base("Exit")
        {
            this.value = null;
            this.return_func_return = true;
        }
        public override object Execute(Ambit ambit)
        {
            if (!ambit.IsFunction)
            {
                Controller.ErrorController.Instance.SyntacticError("La sentencia Exit solo puede aparece dentro de funciones", row, column);
                return null;
            }
            var generator = C3D.C3DController.Instance;

            var texto = "";
            if (value != null)
            {
                var val = value.Execute(ambit);

                if (val == null || val.getDataType == DataType.ERROR)
                {
                    return null;
                }

                if (ambit.Tipo_fun != val.getDataType)
                {
                    Controller.ErrorController.Instance.SyntacticError("Retorno de tipos incorrecto", row, column);
                    return null;
                }


                var res = "";
                if (val.getDataType == DataType.BOOLEAN)
                {
                    res = (val.Value.ToString().ToLower().Equals("true") ? "1" : "0");
                } else
                {
                    texto += val.Texto_anterior;
                    res = val.Value.ToString();
                }

                texto += generator.set_stack(ambit.Temp_return, res, cant_tabs);
                texto += generator.add_Goto(ambit.Exit, Cant_tabs);


            }
            return texto;
        }

    }
}
