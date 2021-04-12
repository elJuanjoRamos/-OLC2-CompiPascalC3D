using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Break : Instruction
    {
        private int row;
        private int column;
        private int cant_tabs;
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Break(int row, int column, int cant_tabs) :
            base("Break")
        {
            this.row = row;
            this.column = column;
            this.cant_tabs = cant_tabs;
        }
        

        public override object Execute(Ambit ambit)
        {
            var break_str = "";
            var generator = C3D.C3DController.Instance;
            if (getValidAmbit(ambit.Ambit_name_inmediato, ambit.Ambit_name))
            {
                Controller.ErrorController.Instance.SyntacticError("La sentencia Break solo puede aparece en ciclos o en la sentencia CASE", row, column);
                return null;
            }
            break_str = generator.add_Goto(ambit.Break, cant_tabs);
            return break_str;
        }

        public bool getValidAmbit(string ambit_name, string ambit_padre)
        {
            switch (ambit_name)
            {
                case "for":
                    return true;
                case "while":
                    return true;
                case "repeat":
                    return true;
                case "case":
                    return true;
            }

            if (ambit_padre.Contains("for"))
            {
                return true;
            }
            if (ambit_padre.Contains("while"))
            {
                return true;
            }
            if (ambit_padre.Contains("repeat"))
            {
                return true;
            }
            if (ambit_padre.Contains("case"))
            {
                return true;
            }
            return false;
        }

    }
}
