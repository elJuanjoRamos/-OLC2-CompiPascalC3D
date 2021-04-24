using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Symbols
{
    class Literal
    {
        private string value;
        private bool isNumber;
        private bool isTemp;
        private bool isPointer;
        private bool isString;
        public Literal(string value, bool isNumber, bool isTemp, bool isPointer)
        {
            this.value = value;
            this.isNumber = isNumber;
            this.isTemp = isTemp;
            this.isPointer = isPointer;
            this.isString = false;
        }

        public Literal(string value)
        {
            this.value = value;
            this.isString = true;
        }

        public string Value { get => value; set => this.value = value; }
        public bool IsNumber { get => isNumber; set => isNumber = value; }
        public bool IsTemp { get => isTemp; set => isTemp = value; }
        public bool IsPointer { get => isPointer; set => isPointer = value; }
        public bool IsString { get => isString; set => isString = value; }
    }
}
