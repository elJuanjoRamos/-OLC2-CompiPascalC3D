using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences.Array
{
    public class Dimention : Instruction
    {
        private Expresion inferior;
        private Expresion superior;
        private int row;
        private int column;
        private int cant_tabs;
        private int dimention;

        public Dimention(Expresion inferior, Expresion superior, int row, int column, int cant_tabs, int dimention)
        {
            this.inferior = inferior;
            this.superior = superior;
            this.row = row;
            this.column = column;
            this.cant_tabs = cant_tabs;
            this.dimention = dimention;
        }

        public Expresion Inferior { get => inferior; set => inferior = value; }
        public Expresion Superior { get => superior; set => superior = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public int Cant_tabs { get => cant_tabs; set => cant_tabs = value; }
        public int Dimentions { get => dimention; set => dimention = value; }

        public override object Execute(Ambit ambit)
        {
            throw new NotImplementedException();
        }
    }
}
