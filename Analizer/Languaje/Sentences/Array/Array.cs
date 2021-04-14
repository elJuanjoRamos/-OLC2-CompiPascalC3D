using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences.Array
{
    class Array : Instruction
    {
        private string id;
        private LinkedList<Dimention> dimensiones;
        private DataType dataType;
        private int row;
        private int column;
        private int cant_tabs;

        public Array(string id, LinkedList<Dimention> dimensiones, DataType dataType, int row, int column, int cant_tabs)
        {
            this.id = id;
            this.dimensiones = dimensiones;
            this.dataType = dataType;
            this.row = row;
            this.column = column;
            this.cant_tabs = cant_tabs;
        }

        public override object Execute(Ambit ambit)
        {
            throw new NotImplementedException();
        }
    }
}
