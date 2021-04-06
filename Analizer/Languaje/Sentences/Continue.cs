﻿using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Continue : Instruction
    {
        private int row;
        private int column;
        private int cant_tabs;
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Continue(int row, int column, int cant) :
            base("Continue")
        {
            this.row = row;
            this.column = column;
            this.cant_tabs = cant;
        }
        

        public override string Execute(Ambit ambit)
        {
            var generator = C3D.C3DController.Instance;
            if (ambit.Continue == "")
            {
                Controller.ErrorController.Instance.SyntacticError("La sentencia Continue solo puede aparece en ciclos", row, column);
                return null;
            }
            var cont = generator.newLabel();
            ambit.Continue = cont;
            ambit.Change_continue = true;
            generator.add_Goto(ambit.Continue, cant_tabs);
            return "executed";
        }
    }
}
