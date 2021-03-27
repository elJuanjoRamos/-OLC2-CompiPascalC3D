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
        public Returned(string v, DataType d, bool istemp)
        {
            this._value = v;
            this.type = d;
            this.isTemporal = istemp;
            this.isNull = false;
            this.trueLabel = this.falseLabel = "";
        }
        public Returned(string v, DataType d, bool ist, string truel, string falsel)
        {
            this._value = v;
            this.type = d;
            this.isTemporal = ist;
            this.isNull = false;
            this.trueLabel = truel;
            this.falseLabel = falsel;
        }
        public Returned()
        {
            this._value = "";
            this.type = DataType.ERROR;
            this.isNull = true;
            this.isTemporal = false;
            this.trueLabel = this.falseLabel = "";
        }
        public Returned(string n)
        {
            this._value = n;
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
        public string getValue()
        {
            //C3DController.Instance.freeTemp(this._value);
            return this._value;
        }
    }
}
