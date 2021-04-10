using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Expressions
{
    class Logical : Expresion
    {
        private Expresion left;
        private Expresion right;
        private string type;
        public int row;
        public int column;
        private int cant_tabs;
        public Logical(Expresion l, Expresion r, string t, int ro, int c, int ct)
        : base("Logical")
        {
            this.left = l;
            this.right = r;
            this.type = t;
            this.row = ro;
            this.column = c;
            this.cant_tabs = ct;
        }

        public override Returned Execute(Ambit ambit)
        {
            var logical_str = "";
            var result = new Returned();
            var operacion = GetOpLogical(this.type);

            //INSTANCIA DEL GENERADOR C3D
            var generator = C3DController.Instance;

            if (this.TrueLabel == "")
            {
                this.TrueLabel = generator.newLabel();
            }
            if (this.FalseLabel == "")
            {
                this.FalseLabel = generator.newLabel();
            }

            

            switch (operacion)
            {
                case OpLogical.AND:


                    this.left.TrueLabel = this.TrueLabel;
                    this.left.FalseLabel = this.FalseLabel;

                    //EXPRESIONES
                    var varIz = this.left.Execute(ambit);

                    logical_str += varIz.Texto_anterior;

                    if (varIz.getDataType == DataType.BOOLEAN)
                    {
                        logical_str += generator.addLabel(this.left.TrueLabel, cant_tabs);

                        this.right.TrueLabel = generator.newLabel();
                        this.right.FalseLabel = varIz.FalseLabel;

                        var valDer = right.Execute(ambit);
                        logical_str += valDer.Texto_anterior;

                        if (valDer.getDataType != DataType.BOOLEAN)
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado alos tipos " + varIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }
                        
                        return new Returned("", DataType.BOOLEAN, false, this.right.TrueLabel, this.right.FalseLabel, logical_str);

                    }
                    set_error("Operador '" + this.type + "' NO puede ser aplicado al tipo " + varIz.getDataType, row, column);
                    return result;


                case OpLogical.OR:
                    this.left.TrueLabel = this.TrueLabel;
                    this.left.FalseLabel = this.FalseLabel;

                    //EXPRESIONES
                    var valIz = this.left.Execute(ambit);
                    logical_str += valIz.Texto_anterior;
                    if (valIz.getDataType == DataType.BOOLEAN)
                    {
                        logical_str += generator.addLabel(this.left.FalseLabel, cant_tabs);

                        this.right.TrueLabel = valIz.TrueLabel;
                        this.right.FalseLabel = generator.newLabel();

                        var valDer = right.Execute(ambit);
                        logical_str += valDer.Texto_anterior;

                        if (valDer.getDataType != DataType.BOOLEAN)
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado alos tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }
                        return new Returned("", DataType.BOOLEAN, false,this.right.TrueLabel, this.right.FalseLabel, logical_str);

                    }
                    set_error("Operador '" + this.type + "' NO puede ser aplicado al tipo " + valIz.getDataType, row, column);
                    return result;


                case OpLogical.NOT:


                    this.left.TrueLabel = this.FalseLabel;
                    this.left.FalseLabel = this.TrueLabel;

                    //EXPRESIONES
                    var varrIz = this.left.Execute(ambit);
                    logical_str += varrIz.Texto_anterior;
                    if (varrIz.getDataType != DataType.BOOLEAN)
                    {
                        set_error("Operador '" + this.type + "' NO puede ser aplicado al tipo " + varrIz.getDataType , row, column);
                        return result;
                    }
                    return new Returned("", DataType.BOOLEAN, false, varrIz.FalseLabel, varrIz.TrueLabel, logical_str);



                default:
                    break;
            }


            return result;
        }


        public OpLogical GetOpLogical(string simb)
        {
            if (simb.ToLower().Equals("and"))
            {
                return OpLogical.AND;
            }
            if (simb.ToLower().Equals("or"))
            {
                return OpLogical.OR;
            }
            return OpLogical.NOT;
        }
        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
