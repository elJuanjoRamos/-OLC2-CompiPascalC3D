﻿using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Sentence : Instruction
    {
        private LinkedList<Instruction> list;
        private int row;
        private int column;
        private bool isNull;

        public Sentence(LinkedList<Instruction> list)
           : base("Sentence")
        {
            this.list = list;
            this.row = 0;
            this.column = 0;
            this.isNull = false;
        }
        public Sentence()
            : base("Sentence")
        {
            this.isNull = true;
        }

       
        public string obtenerTipo(DataType tipo)
        {

            if (tipo == DataType.INTEGER)
            {
                return "integer";
            }
            else if (tipo == DataType.REAL)
            {
                return "real";
            }
            else if (tipo == DataType.STRING)
            {
                return "string";
            }
            else if (tipo == DataType.TYPE)
            {
                return "type";
            }
            else if (tipo == DataType.BOOLEAN)
            {
                return "boolean";
            }
            return "array";
        }

        public override string Execute(Ambit ambit)
        {
            if (IsNull)
            {
                return "";
            }

            var response = "";

            foreach (var inst in list)
            {
                var res = inst.Execute(ambit);
                if (res == "null")
                {
                    return "null";
                }
                response = response + res;
            }

            return response;

        }
    }
}
