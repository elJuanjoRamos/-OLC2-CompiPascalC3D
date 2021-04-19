using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Symbols
{
    public class Returned
    {
        string _value;
        DataType type;
        bool isNull;
        bool isTemporal;
        private string trueLabel;
        private string falseLabel;
        private string texto_anterior;
        private string valor_original;
        private int position;
        private int pos_global;
        
        public Returned(string v, DataType d, bool istemp, string truel, string falsel, string texto_ant, string valori, int posi, int posglobal)
        {
            this._value = v;
            this.type = d;
            this.isTemporal = istemp;
            this.isNull = false;
            this.trueLabel = truel;
            this.falseLabel = falsel;
            this.texto_anterior = texto_ant;
            this.valor_original = valori;
            this.position = posi;
            this.pos_global = posglobal;
        }
        public Returned()
        {
            this._value = "";
            this.type = DataType.ERROR;
            this.isNull = true;
            this.isTemporal = false;
            this.trueLabel = this.falseLabel = "";
        }
        
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public DataType getDataType
        {
            get { return type; }
            set { type = value; }
        }
        public bool IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

        public bool IsTemporal { get => isTemporal; set => isTemporal = value; }
        public string TrueLabel { get => trueLabel; set => trueLabel = value; }
        public string FalseLabel { get => falseLabel; set => falseLabel = value; }
        public string Texto_anterior { get => texto_anterior; set => texto_anterior = value; }
        public string Valor_original { get => valor_original; set => valor_original = value; }
        public int Position { get => position; set => position = value; }
        public int Pos_global { get => pos_global; set => pos_global = value; }

        public string getValue()
        {
            //C3DController.Instance.freeTemp(this._value);
            return this._value;
        }
    }
}
