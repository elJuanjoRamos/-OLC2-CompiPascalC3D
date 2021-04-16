using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Declaration : Instruction
    {
        private string id;
        private DataType type;
        private Expresion value;
        public int row;
        public int column;
        public bool isConst;
        public bool isAssigned;
        public bool isRefer;


        public DataType Type { get => type; set => type = value; }
        public string Id { get => id; set => id = value; }




        //CONSTRUCTOR PARA VARIABLES
        public Declaration(string id, String dataType, Expresion ex, int r, int c, bool isAs, bool refe)
            : base("Declaration")
        {
            this.id = id;
            this.type = GetDataType(dataType);
            this.value = ex;
            this.row = r;
            this.column = c;
            this.isConst = false;
            this.isAssigned = isAs;
            this.isRefer = refe;
        }
        //CONSTRUCTOR PARA CONSTANTES
        public Declaration(string i, Expresion e, int r, int c, bool isc, bool isrefe)
            : base("Declaration")
        {
            this.id = i;
            this.type = DataType.CONST;
            this.value = e;
            this.row = r;
            this.column = c;
            this.isConst = isc;
            this.isAssigned = true;
            this.isRefer = isrefe;
        }



        public override object Execute(Ambit ambit)
        {
            var string_declaracion = "";

            Identifier buscar = new Identifier();
            //BUSCA LA VARIABLE SI NO HA SIDO DECLARADA
            if (!ambit.Ambit_name_inmediato.Equals("Function") && !ambit.Ambit_name_inmediato.Equals("Procedure"))
            {
                buscar = ambit.getVariable(id);
            }
            //SIGFINICA QUE ES UNA DECLARACION EN FUNCION
            else
            {
                buscar = ambit.getVariableFunctionInAmbit(id);
            }

            if (buscar.IsNull)
            {
                try
                {
                    Returned val = this.value.Execute(ambit);
                    string_declaracion += val.Texto_anterior;


                    //VERIFICA QUE NO HAYA ERROR
                    if (val.getDataType == DataType.ERROR)
                    {
                        return null;
                    }

                    Identifier variable;

                    if (this.type == DataType.CONST)
                    {
                        variable = ambit.save(this.id, val.Value, val.Valor_original, val.getDataType, true, true, false, this.isRefer, "Constant");
                    }
                    else
                    {
                        if (val.getDataType == this.type)
                        {
                            variable = ambit.save(this.id, val.Value, val.Valor_original, val.getDataType, false, isAssigned, false, this.isRefer, "Variable");
                        } else
                        {
                            set_error("El tipo " + val.getDataType + " no es asignable con " + this.type.ToString(), row, column);
                            return null;
                        }
                    }


                    var generator = C3DController.Instance;
                    if (val.IsTemporal)
                    {
                        generator.free_temps(val.Value);
                    }

                    if (variable.IsGlobal)
                    {


                        if (this.type == DataType.BOOLEAN || val.Valor_original.ToLower().Equals("false") || val.Valor_original.ToLower().Equals("true"))
                        {
                            var templabel = generator.newLabel();
                            string_declaracion += generator.addLabel(val.TrueLabel, 1);
                            string_declaracion += generator.set_stack(variable.Position.ToString(), "1", 1);
                            string_declaracion += generator.add_Goto(templabel, 1);
                            string_declaracion += generator.addLabel(val.FalseLabel, 1);
                            string_declaracion += generator.set_stack(variable.Position.ToString(), "0", 1);
                            string_declaracion += generator.addLabel(templabel, 1);
                        }
                        else
                        {
                            string_declaracion += generator.set_stack(variable.Position.ToString(), val.getValue(), 1);
                        }
                    }
                    else
                    {
                        var temp = generator.newTemporal();
                        //generator.freeTemp(temp);
                        string_declaracion += generator.addExpression(temp, "SP", variable.Position.ToString(), "+", 1);

                        if (variable.DataType == DataType.BOOLEAN)
                        {
                            var templabel = generator.newLabel();
                            string_declaracion += generator.addLabel(val.TrueLabel, 1);
                            string_declaracion += generator.set_stack(temp, "1", 1);
                            string_declaracion += generator.add_Goto(templabel, 1);
                            string_declaracion += generator.addLabel(val.FalseLabel, 1);
                            string_declaracion += generator.set_stack(temp, "0", 1);
                            string_declaracion += generator.addLabel(templabel, 1);
                        }
                        else
                        {
                            string_declaracion += generator.set_stack(temp, val.getValue(), 1);
                            generator.free_temps(temp);
                        }

                    }
                }
                catch (Exception)
                {

                }

            }
            else
            {
                set_error("La variable '" + id + "' ya fue declarada", row, column);
                return null;
            }
            return string_declaracion;
        }



        public DataType GetDataType(string d)
        {
            if (d.Equals("integer"))
            {
                return DataType.INTEGER;
            }
            else if (d.Equals("boolean"))
            {
                return DataType.BOOLEAN;
            }
            else if (d.Equals("real"))
            {
                return DataType.REAL;
            }
            return DataType.STRING;

        }
        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
