using CompiPascalC3D.Analizer.Languaje.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Symbols
{
    public class Identifier
    {
        private string _value;
        private string default_value;
        private string id;
        private DataType type;
        private bool esconsante;
        private bool isNull;
        private bool isAssiged;
        private string tipo_dato;
        private int position;
        private bool isGlobal;
        private bool isHeap;
        private bool isReference;
        private int position_global;
        private int position_reference;

        public Identifier(string value, string defaultv, string ids, DataType tipo, bool es_const, 
            bool isassigned, int posi, bool isglobal, bool is_Heap, bool is_reference, string tipoDato, 
            int pos_global, int pos_referenced)
        {
            this._value = value;
            this.default_value = defaultv;
            this.id = ids;
            this.type = tipo;
            this.esconsante = es_const;
            this.isNull = false;
            this.isAssiged = isassigned;
            this.position = posi;
            this.isGlobal = isglobal;
            this.isHeap = is_Heap;
            this.isReference = is_reference;
            this.tipo_dato = tipoDato;
            this.position_global = pos_global;
            this.position_reference = pos_referenced;
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
        public string Tipo_dato { get => tipo_dato; set => tipo_dato = value; }
        public string Default_value { get => default_value; set => default_value = value; }
        public bool IsReference { get => isReference; set => isReference = value; }
        public int Position_global { get => position_global; set => position_global = value; }
        public int Position_reference { get => position_reference; set => position_reference = value; }
    }
}
