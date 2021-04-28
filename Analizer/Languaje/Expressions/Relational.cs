using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Expressions
{
    class Relational : Expresion
    { 
        private Expresion left;
        private Expresion right;
        private string type;
        private int row;
        private int column;
        private int cant_tabs;

        public Relational(Expresion left, Expresion right, string type, int r, int c, int cantTabs)
            : base("Relational")
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.row = r;
            this.column = c;
            this.cant_tabs = cantTabs;
        }

        public override Returned Execute(Ambit ambit)
        {
            var relational_Str = "";
            var valIz = this.left.Execute(ambit);
            relational_Str += valIz.Texto_anterior;

            var result = new Returned();

            var generator = C3DController.Instance;

            var op = GetType(this.type);

            switch (op)
            {
                case OpRelational.EQUALS:
                    relational_Str += generator.save_comment("Empieza EQUALS", cant_tabs, false);
                    //VERIFICA SI EL IZQUIERDO ES REAL O INT
                    if (valIz.getDataType == DataType.REAL || valIz.getDataType == DataType.INTEGER)
                    {
                        var valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;

                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            if (this.TrueLabel == "")
                            {
                                this.TrueLabel = generator.newLabel();
                            }
                            if (this.FalseLabel == "")
                            {
                                this.FalseLabel = generator.newLabel();
                            }
                            ambit.free_temp(valIz.getValue());
                            ambit.free_temp(valDer.getValue());

                            relational_Str += generator.add_If(valIz.getValue(), valDer.getValue(), "==", this.TrueLabel, cant_tabs);
                            relational_Str += generator.add_Goto(this.FalseLabel, cant_tabs);
                            relational_Str += generator.save_comment("Termina EQUALS", cant_tabs, true);

                            result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel, relational_Str, "",0,0);

                        }
                        else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }
                    }
                    //VERIFICA SI EL IZQUIERDO ES BOOLEAN
                    else if (valIz.getDataType == DataType.BOOLEAN)
                    {
                        var trueLabel = generator.newLabel();
                        var falseLabel = generator.newLabel();
                        relational_Str += generator.addLabel(valIz.TrueLabel, cant_tabs);
                        this.right.TrueLabel = trueLabel;
                        this.right.FalseLabel = falseLabel;
                        var valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;

                        relational_Str += generator.addLabel(valIz.FalseLabel, cant_tabs);

                        this.right.TrueLabel = falseLabel;
                        this.right.FalseLabel = trueLabel;
                        valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;

                        //VERIFICA QUE EL DERECHO SEA BOOLEAN
                        if (valDer.getDataType == DataType.BOOLEAN)
                        {
                            relational_Str += generator.save_comment("Termina EQUALS", cant_tabs, true);
                            ambit.free_temp(valIz.getValue());
                            ambit.free_temp(valDer.getValue());
                            result = new Returned("", DataType.BOOLEAN, false, trueLabel, falseLabel, relational_Str, "",0,0);

                        } else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }
                    }
                    ////VERIFICA QUE EL IZQUIERDO SEA STRING
                    else if (valIz.getDataType == DataType.STRING)
                    {
                        generator.Native_equals = true;

                        var valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;


                        if (valDer.getDataType == DataType.STRING)
                        {
                            if (this.TrueLabel == "")
                            {
                                this.TrueLabel = generator.newLabel();
                            }
                            if (this.FalseLabel == "")
                            {
                                this.FalseLabel = generator.newLabel();
                            }



                            var tempo_izq = valIz.Value.ToString();
                            var tempo_der = valDer.Value.ToString();

                            relational_Str += generator.addExpression("T3", tempo_izq, "", "", cant_tabs);
                            relational_Str += generator.addExpression("T4", tempo_der, "", "", cant_tabs);
                            relational_Str += generator.addExpression("T8", "native_cmp_str()","","",cant_tabs);
                            relational_Str += generator.add_If("T8", "1", "==", this.TrueLabel, cant_tabs);
                            relational_Str += generator.add_Goto(this.FalseLabel, cant_tabs);

                            ambit.free_temp(tempo_der);
                            ambit.free_temp(tempo_izq);

                            relational_Str += generator.save_comment("Termina EQUALS", cant_tabs, true);

                            result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel, relational_Str, "",0,0);

                        }
                        else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }
                    }

                    else
                    {
                        set_error("Operador '" + this.type + "' NO puede ser aplicado al tipo " + valIz.getDataType, row, column);
                        return result;
                    }


                    break;
                case OpRelational.DISCTINCT:
                    relational_Str += generator.save_comment("Empieza DISTICT", cant_tabs, false);

                    //VERIFICA QUE EL IZQUIERDO ESA REAL O INTEGER
                    if (valIz.getDataType == DataType.REAL || valIz.getDataType == DataType.INTEGER)
                    {
                        var valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;

                        if (valDer.getDataType == DataType.INTEGER || valDer.getDataType == DataType.REAL)
                        {
                            if (this.TrueLabel == "")
                            {
                                this.TrueLabel = generator.newLabel();
                            }
                            if (this.FalseLabel == "")
                            {
                                this.FalseLabel = generator.newLabel();
                            }
                            ambit.free_temp(valIz.getValue());
                            ambit.free_temp(valDer.getValue());
                            relational_Str += generator.add_If(valIz.getValue(), valDer.getValue(), "!=", this.TrueLabel, cant_tabs);
                            relational_Str += generator.add_Goto(this.FalseLabel, cant_tabs);
                            relational_Str += generator.save_comment("Termina DISTICT", cant_tabs, true);
                            result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel, relational_Str, "",0,0);

                        }
                        else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }

                    }
                    else if (valIz.getDataType == DataType.BOOLEAN)
                    {
                        var trueLabel = generator.newLabel();
                        var falseLabel = generator.newLabel();
                        relational_Str += generator.addLabel(valIz.TrueLabel, cant_tabs);
                        this.right.TrueLabel = falseLabel;
                        this.right.FalseLabel = trueLabel;
                        var valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;

                        relational_Str += generator.addLabel(valIz.FalseLabel, cant_tabs);

                        this.right.TrueLabel = trueLabel;
                        this.right.FalseLabel = falseLabel;
                        valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;

                        //VERIFICA QUE EL DERECHO SEA BOOLEAN
                        if (valDer.getDataType == DataType.BOOLEAN)
                        {
                            ambit.free_temp(valIz.getValue());
                            ambit.free_temp(valDer.getValue());
                            relational_Str += generator.save_comment("Termina DISTICT", cant_tabs, true);
                            result = new Returned("", DataType.BOOLEAN, false, trueLabel, falseLabel, relational_Str, "",0,0);

                        }
                        else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }
                    }
                    else
                    {
                        set_error("Operador '" + this.type + "' NO puede ser aplicado al tipo " + valIz.getDataType, row, column);
                        return result;
                    }

                    break;
                case OpRelational.LESS:
                case OpRelational.LESS_EQUALS:
                case OpRelational.HIGHER:
                case OpRelational.HIGHER_EQUALS:
                    relational_Str += generator.save_comment("Empieza " + op, cant_tabs, false);
                    if (valIz.getDataType == DataType.REAL || valIz.getDataType == DataType.INTEGER)
                    {
                        var valDer = this.right.Execute(ambit);
                        relational_Str += valDer.Texto_anterior;

                        if (valDer.getDataType == DataType.REAL || valDer.getDataType == DataType.INTEGER)
                        {
                            if (this.TrueLabel == "")
                            {
                                this.TrueLabel = generator.newLabel();
                            }
                            if (this.FalseLabel == "")
                            {
                                this.FalseLabel = generator.newLabel();
                            }
                            ambit.free_temp(valIz.getValue());
                            ambit.free_temp(valDer.getValue());
                            relational_Str += generator.add_If(valIz.getValue(), valDer.getValue(), this.type, this.TrueLabel, cant_tabs);
                            relational_Str += generator.add_Goto(this.FalseLabel, cant_tabs);
                            relational_Str += generator.save_comment("Termina " + op, cant_tabs, true);
                            result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel, relational_Str, "",0,0);

                        } else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }   

                    }
                    else
                    {
                        set_error("Operador '" + this.type + "' NO puede ser aplicado al tipo " + valIz.getDataType, row, column);
                        return result;
                    }

                    break;
                default:
                    return result;
            }

            return result;

        }


        public OpRelational GetType(string simb)
        {
            if (simb.Equals("<"))
            {
                return OpRelational.LESS;
            }
            else if (simb.Equals(">="))
            {
                return OpRelational.HIGHER_EQUALS;
            }
            else if (simb.Equals(">"))
            {
                return OpRelational.HIGHER;
            }
            else if (simb.Equals("<="))
            {
                return OpRelational.LESS_EQUALS;
            }
            else if (simb.Equals("<>"))
            {
                return OpRelational.DISCTINCT;
            }

            return OpRelational.EQUALS;
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
