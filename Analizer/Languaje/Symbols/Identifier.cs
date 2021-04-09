using CompiPascalC3D.Analizer.Languaje.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Symbols
{
    public class Identifier
    {
        private string _value;
        private string id;
        private DataType type;
        private bool esconsante;
        private bool isNull;
        private bool isAssiged;
        
        private int position;
        private bool isGlobal;
        private bool isHeap;

        public Identifier(string value, string ids, DataType tipo, bool es_const, bool isassigned, int posi, bool isglobal, bool is_Heap)
        {
            this._value = value;
            this.id = ids;
            this.type = tipo;
            this.esconsante = es_const;
            this.isNull = false;
            this.isAssiged = isassigned;
            this.position = posi;
            this.isGlobal = isglobal;
            this.isHeap = is_Heap;
        }
        public Identifier()
        {
            this.isNull = true;
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public DataType DataType
        {
            get { return type; }
            set { type = value; }
        }

        public bool Esconstante
        {
            get { return esconsante; }
            set { esconsante = value; }
        }
        public bool IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

        public bool IsAssiged { get => isAssiged; set => isAssiged = value; }
        public int Position { get => position; set => position = value; }
        public bool IsGlobal { get => isGlobal; set => isGlobal = value; }
        public bool IsHeap { get => isHeap; set => isHeap = value; }
    }
}
