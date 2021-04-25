using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Arithmetics
{
    class Expresion : Instruction
    {
        private string temp;
        private Literal left;
        private Literal right;
        private string simbol;
        private int row;
        private int column;
        private bool isEmpty;
        private string ambit_name;

        public bool IsEmpty { get => isEmpty; set => isEmpty = value; }
        internal Literal Right { get => right; set => right = value; }
        internal Literal Left { get => left; set => left = value; }
        public string Temp { get => temp; set => temp = value; }

        public Expresion(string temp, Literal left, Literal right, string simbol, int r, int c)
            : base("Aritmetica")
        {
            this.temp = temp;
            this.left = left;
            this.right = right;
            this.simbol = simbol;
            this.row = r;
            this.column = c;
            this.isEmpty = false;
        }

        public Expresion(string temp, Literal left, int r, int c)
            : base("Aritmetica")
        {
            this.temp = temp;
            this.left = left;
            this.right = null;
            this.row = r;
            this.column = c;
            this.isEmpty = false;
        }
        public Expresion()
            : base("Aritmetica")
        {
            this.isEmpty = true;
        }
        public override object Optimize()
        {
            var controller = ReporteController.Instance;
            

            if (right == null)
            {
                return this;
            }
            var iz = left.Value;
            var der = right.Value;


            if (left.IsNumber && right.IsNumber)
            {
                var res = Double.Parse(left.Value) + Double.Parse(right.Value);
                controller.set_optimizacion("Regla 6", temp + "=" + iz + "+" + der, temp + "=" + res.ToString(), row, column, ambit_name);
                return new Expresion(temp, new Literal(res.ToString(), true, false, false), row, column); 
            }

            if (temp.Equals(iz))
            {
                //Regla 6
                if (der.Equals("0") && simbol.Equals("+"))
                {
                    controller.set_optimizacion("Regla 6", temp + "=" + iz + "+" + der, "", row, column, ambit_name);
                    return new Expresion();
                }
                //Regla 7
                else if (der.Equals("0") && simbol.Equals("-"))
                {
                    controller.set_optimizacion("Regla 7", temp + "=" + iz + "-" + der, "", row, column,ambit_name);
                    return new Expresion();
                }
                //Regla 8
                else if (der.Equals("1") && simbol.Equals("*"))
                {
                    controller.set_optimizacion("Regla 8", temp + "=" + iz + "*" + der, "", row, column,ambit_name);
                    return new Expresion();
                }
                //Regla 9
                else if (der.Equals("1") && simbol.Equals("/"))
                {
                    controller.set_optimizacion("Regla 9", temp + "=" + iz + "/" + der, "", row, column,ambit_name);
                    return new Expresion();
                }
                else
                {
                    return this;
                }
            }
            else
            {
                //Regla 10
                if (simbol.Equals("+") && der.Equals("0"))
                {
                    controller.set_optimizacion("Regla 10", temp + "=" + iz + simbol + der, temp + "=" + iz, row, column, ambit_name);
                    return new Expresion(temp, left, row, column);  
                }
                //Regla 11
                else if (simbol.Equals("-") && der.Equals("0"))
                {
                    controller.set_optimizacion("Regla 11", temp + "=" + iz + simbol + der, temp + "=" + iz, row, column, ambit_name);
                    return new Expresion(temp, left, row, column);
                }
                //Regla 12
                else if (simbol.Equals("*") && der.Equals("1"))
                {
                    controller.set_optimizacion("Regla 12", temp + "=" + iz + simbol + der, temp + "=" + iz, row, column, ambit_name);
                    return new Expresion(temp, left, row, column);
                }
                //Regla 13
                else if (simbol.Equals("/") && der.Equals("1"))
                {
                    controller.set_optimizacion("Regla 13", temp + "=" + iz + simbol + der, temp + "=" + iz, row, column, ambit_name);
                    return new Expresion(temp, left, row, column);
                }
                //Regla 14
                else if (simbol.Equals("*") && der.Equals("2"))
                {
                    controller.set_optimizacion("Regla 14", temp + "=" + iz + simbol + der, temp + "=" + iz + "+" + iz, row, column, ambit_name);
                    return new Expresion(temp, left, left, "+", row, column);
                }
                //Regla 15
                else if (simbol.Equals("*") && der.Equals("0"))
                {
                    controller.set_optimizacion("Regla 15", temp + "=" + iz + simbol + der, temp + "=" + "0", row, column, ambit_name);
                    return new Expresion(temp, new Literal("0", true, false, false), row, column);
                }
                //Regla 16
                else if (simbol.Equals("/") && iz.Equals("0"))
                {
                    controller.set_optimizacion("Regla 16", temp + "=" + iz + simbol + der, temp + "=" + "0", row, column, ambit_name);
                    return new Expresion(temp, new Literal("0", true, false, false), row, column);
                }
                else
                {
                    return this;
                }
            }

            return "";
        }

        public override string Code()
        {
            if (isEmpty)
            {
                return "";
            }
            var cadena = temp + " = ";
            if (right == null)
            {
                cadena += left.Value + ";\n";
                return cadena;
            }
            cadena += left.Value + simbol + right.Value + ";\n";

            return cadena;
        }

        public void set_ambit(string ambit)
        {
            this.ambit_name = ambit;
        }

    }
}
