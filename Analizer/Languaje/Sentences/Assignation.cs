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
    class Assignation : Instruction
    {
        private string id;
        private Expresion value;
        private int row;
        private int column;
        private int tabs;
        private string assignation_string = "";
        public Assignation(string id, Expresion value, int row, int column, int cantTabs) :
            base("Assignation")
        {
            this.id = id;
            this.value = value;
            this.row = row;
            this.column = column;
            this.tabs = cantTabs;
            this.assignation_string = "";
        }
        public override object Execute(Ambit ambit)
        {
            try
            {

                Identifier variable = ambit.getVariable(id);

                /**
                * VALIDAR EXISTENCIA
                */
                if (!variable.IsNull)
                {
                    /**
                    * VERIFICA QUE NO SEA CONSTABTE
                    */
                    if (variable.Esconstante)
                    {
                        setError("No se puede cambiar el valor a una constante", row, column);
                        return null;
                    }
                    else
                    {
                        var val = this.value.Execute(ambit);

                        assignation_string += val.Texto_anterior;

                        if (val == null || val.getDataType == DataType.ERROR)
                        {
                            return null;
                        }

                        /**
                        * VALIDAR VALOR: VERIFICA SI EL TIPO DE LA VARIABLE ES IGUAL AL DEL VALOR A ASIGNAR
                        */
                        if (variable.DataType == val.getDataType)
                        {
                            var generator = C3DController.Instance;

                            if (val.IsTemporal)
                            {
                                //generator.free_temps(val.Value);
                            }

                            if (variable.IsGlobal)
                            {
                                if (variable.DataType == DataType.BOOLEAN)
                                {
                                    var templabel = generator.newLabel();
                                    assignation_string += generator.addLabel(val.TrueLabel, tabs);
                                    assignation_string += generator.set_stack(variable.Position.ToString(), "1", tabs);
                                    assignation_string += generator.add_Goto(templabel, tabs + 1);
                                    assignation_string += generator.addLabel(val.FalseLabel, tabs);
                                    assignation_string += generator.set_stack(variable.Position.ToString(), "0", tabs);
                                    assignation_string += generator.addLabel(templabel, tabs);
                                }
                                else
                                {
                                    assignation_string += generator.set_stack(variable.Position.ToString(), val.getValue(), tabs);
                                }
                            }
                            else
                            {

                                var temp = generator.newTemporal();
                                assignation_string += generator.addExpression(temp, "SP", variable.Position.ToString(), "+", tabs);

                                if (variable.DataType == DataType.BOOLEAN)
                                {
                                    var templabel = generator.newLabel();
                                    assignation_string += generator.addLabel(val.TrueLabel, tabs);
                                    assignation_string += generator.set_stack(temp, "1", tabs);
                                    assignation_string += generator.add_Goto(templabel, tabs + 1);
                                    assignation_string += generator.addLabel(val.FalseLabel, tabs);
                                    assignation_string += generator.set_stack(temp, "0", tabs);
                                    assignation_string += generator.addLabel(templabel, tabs);
                                }
                                else
                                {
                                    assignation_string += generator.set_stack(temp, val.getValue(), tabs);
                                }

                            }


                            return assignation_string;

                        }
                        else
                        {
                            setError("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, row, column);
                            return null;
                        }
                    }

                }
                else
                {
                    /*Function function = ambit.getFuncion(id);
                    if (function != null)
                    {
                        if (function.IsProcedure)
                        {
                            setError("No puede asignarse ningun valor al procedimiento '" + id + "' ", row, column);
                            return null;
                        }
                        /**
                       * VALIDAR VALOR: VERIFICA SI EL TIPO DE LA VARIABLE ES IGUAL AL DEL VALOR A ASIGNAR
                       */
                    /* if (function.Tipe == val.getDataType)
                     {
                         function.Retorno = val.Value.ToString();
                         ambit.setFunction(Id, function);
                         return new Returned(function.Retorno, function.Tipe, false);
                     }
                     else
                     {
                         setError("El tipo " + val.getDataType + " no es asignable con " + variable.DataType, row, column);
                         return null;
                     }
                 }
                 else
                 {
                     setError("La variable '" + id + "' no esta declara", row, column);
                     return null;
                 }*/
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ConsolaController.Instance.Add("Recuperado de error, Row:" + row + " Col: " + column);
                return null;
            }
            return assignation_string;
        }

       
        public void setError(string texto, int row, int col)
        {
            ErrorController.Instance.SemantycErrors(texto, row, col);
            ConsolaController.Instance.Add(texto + " - Row:" + row + " - Col: " + col + "\n");
        }

       
        public string Id { get => id; set => id = value; }
        public Expresion Value { get => value; set => this.value = value; }


    }
}
