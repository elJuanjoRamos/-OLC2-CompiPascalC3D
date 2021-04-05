﻿using CompiPascalC3D.Analizer.C3D;
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
            var valIz = this.left.Execute(ambit);

            var result = new Returned();

            var generator = C3DController.Instance;

            var op = GetType(this.type);

            switch (op)
            {
                case OpRelational.EQUALS:

                    //VERIFICA SI EL IZQUIERDO ES REAL O INT
                    if (valIz.getDataType == DataType.REAL || valIz.getDataType == DataType.INTEGER)
                    {
                        var valDer = this.right.Execute(ambit);
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
                            generator.add_If(valIz.getValue(), valDer.getValue(), "==", this.TrueLabel, cant_tabs);
                            generator.add_Goto(this.FalseLabel, cant_tabs);

                            result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel);

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
                        generator.addLabel(valIz.TrueLabel, cant_tabs);
                        this.right.TrueLabel = trueLabel;
                        this.right.FalseLabel = falseLabel;
                        var valDer = this.right.Execute(ambit);


                        generator.addLabel(valIz.FalseLabel, cant_tabs);

                        this.right.TrueLabel = falseLabel;
                        this.right.FalseLabel = trueLabel;
                        valDer = this.right.Execute(ambit);

                        //VERIFICA QUE EL DERECHO SEA BOOLEAN
                        if (valDer.getDataType == DataType.BOOLEAN)
                        {
                            result = new Returned("", DataType.BOOLEAN, false, trueLabel, falseLabel);

                        } else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }
                    }
                    ////VERIFICA QUE EL IZQUIERDO SEA STRING
                    else if (valIz.getDataType == DataType.STRING)
                    {
                        var valDer = this.right.Execute(ambit);
                        if (valDer.getDataType == DataType.STRING)
                        {
                            /*var temp = generator.newTemporal();
                            var tempAux = generator.newTemporal(); 
                            generator.freeTemp(tempAux);

                            generator.addExpression(tempAux, "p", (ambit.Size + 1).ToString(), "+", cant_tabs);
                            generator.set_stack(tempAux, valIz.getValue(), cant_tabs);

                            generator.addExpression(tempAux, tempAux, "1", "+", cant_tabs);
                            generator.set_stack(tempAux, valDer.getValue(), cant_tabs);
                            generator.add_next_ambit(enviorement.size);
                            generator.addCall('native_compare_str_str');
                            generator.addGetStack(temp, 'p');
                            generator.addAntEnv(enviorement.size);*/



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
                case OpRelational.DISCTINCT:
                    //VERIFICA QUE EL IZQUIERDO ESA REAL O INTEGER
                    if (valIz.getDataType == DataType.REAL || valIz.getDataType == DataType.INTEGER)
                    {
                        var valDer = this.right.Execute(ambit);
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
                            generator.add_If(valIz.getValue(), valDer.getValue(), "!=", this.TrueLabel, cant_tabs);
                            generator.add_Goto(this.FalseLabel, cant_tabs);

                            result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel);

                        }
                        else
                        {
                            set_error("Operador '" + this.type + "' NO puede ser aplicado a los tipos " + valIz.getDataType + " con " + valDer.getDataType, row, column);
                            return result;
                        }

                    }
                    else if (valIz.getDataType == DataType.STRING)
                    {

                    }
                    else if (valIz.getDataType == DataType.BOOLEAN)
                    {
                        var trueLabel = generator.newLabel();
                        var falseLabel = generator.newLabel();
                        generator.addLabel(valIz.TrueLabel, cant_tabs);
                        this.right.TrueLabel = falseLabel;
                        this.right.FalseLabel = trueLabel;
                        var valDer = this.right.Execute(ambit);


                        generator.addLabel(valIz.FalseLabel, cant_tabs);

                        this.right.TrueLabel = trueLabel;
                        this.right.FalseLabel = falseLabel;
                        valDer = this.right.Execute(ambit);

                        //VERIFICA QUE EL DERECHO SEA BOOLEAN
                        if (valDer.getDataType == DataType.BOOLEAN)
                        {
                            result = new Returned("", DataType.BOOLEAN, false, trueLabel, falseLabel);

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
                    if (valIz.getDataType == DataType.REAL || valIz.getDataType == DataType.INTEGER)
                    {
                        var valDer = this.right.Execute(ambit);

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
                            generator.add_If(valIz.getValue(), valDer.getValue(), this.type, this.TrueLabel, cant_tabs);
                            generator.add_Goto(this.FalseLabel, cant_tabs);
                            result = new Returned("", DataType.BOOLEAN, false, this.TrueLabel, this.FalseLabel);

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