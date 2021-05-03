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
    class Access : Expresion
    {
        private string id;
        public int row;
        public int column;
        private int cant_Tabs;
        private string access_string = "";

        public string Id { get => id; set => id = value; }

        public Access(string id, int r, int c, int ct)
            : base("Access")
        {
            this.id = id;
            this.row = r;
            this.column = c;
            this.cant_Tabs = ct;
        }

        public override Returned Execute(Ambit ambit)
        {
            
            Identifier variable = ambit.getVariable(this.id);

            if (variable.IsNull)
            {
                set_error("La variable " + this.id + " No esta declarada", row, column);
                return new Returned();
            }
            var generator = C3DController.Instance;
            var temp = generator.newTemporal();
            

            if (variable.IsGlobal)
            {
                ambit.set_temp(temp);
                generator.freeTemp(temp);
                access_string += generator.get_stack(temp, (variable.Position_global).ToString() , cant_Tabs);


                if (variable.DataType != DataType.BOOLEAN)
                {
                    return new Returned(temp, variable.DataType, true, "", "", access_string, variable.Default_value, variable.Position, variable.Position_global);
                }


                if (this.TrueLabel == "")
                {
                    this.TrueLabel = generator.newLabel();
                }
                if (this.FalseLabel == "")
                {
                    this.FalseLabel = generator.newLabel();
                }
                access_string += generator.add_If(temp, "1", "==", this.TrueLabel, cant_Tabs);
                access_string += generator.add_Goto(this.FalseLabel, cant_Tabs);

                return new Returned("", variable.DataType, false, this.TrueLabel,
                    this.FalseLabel, access_string, variable.Default_value, variable.Position, variable.Position_global);

            }
            else
            {
                var tempAux = generator.newTemporal();
                access_string += generator.addExpression(tempAux, "SP", variable.Position.ToString(), "+", cant_Tabs);
                access_string += generator.get_stack(temp, tempAux, cant_Tabs);
                generator.freeTemp(tempAux);
                if (variable.IsReference)
                {
                    tempAux = generator.newTemporal();
                    access_string += generator.get_stack(tempAux, temp, cant_Tabs);
                    generator.freeTemp(temp);
                    temp = tempAux;
                }

                ambit.set_temp(temp);

                if (variable.DataType != DataType.BOOLEAN)
                {
                    return new Returned(temp, variable.DataType, true, "", "",
                        access_string, variable.Default_value, variable.Position, variable.Position_reference);
                }


                if (this.TrueLabel == "")
                {
                    this.TrueLabel = generator.newLabel();
                }
                if (this.FalseLabel == "")
                {
                    this.FalseLabel = generator.newLabel();
                }

                access_string += generator.add_If(temp, "1", "==", this.TrueLabel, cant_Tabs);
                access_string += generator.add_Goto(this.FalseLabel, cant_Tabs);
                return new Returned("", DataType.BOOLEAN, false, this.TrueLabel,
                    this.FalseLabel, access_string, variable.Default_value, variable.Position, variable.Position_reference);
            }

            /*Identifier variable = ambit.getVariableInAmbit(this.id);
            var generator = C3DController.Instance;
            var temp = generator.newTemporal();


            if (!variable.IsNull)
            {

                if (variable.IsGlobal)
                {
                    access_string += generator.get_stack(temp, variable.Position.ToString(), cant_Tabs);

                    if (variable.DataType != DataType.BOOLEAN)
                    {
                        return new Returned(temp, variable.DataType, true, "", "", access_string, variable.Default_value, variable.Position, variable.Position_global);
                    }


                    if (this.TrueLabel == "")
                    {
                        this.TrueLabel = generator.newLabel();
                    }
                    if (this.FalseLabel == "")
                    {
                        this.FalseLabel = generator.newLabel();
                    }
                    access_string += generator.add_If(temp, "1", "==", this.TrueLabel, cant_Tabs);
                    access_string += generator.add_Goto(this.FalseLabel, cant_Tabs);

                    return new Returned("", variable.DataType, false, this.TrueLabel,
                        this.FalseLabel, access_string, variable.Default_value, variable.Position, variable.Position_global);

                }
                else
                {
                    var tempAux = generator.newTemporal();
                    //generator.freeTemp(tempAux);

                    access_string += generator.addExpression(tempAux, "SP", variable.Position.ToString(), "+", cant_Tabs);
                    access_string += generator.get_stack(temp, tempAux, cant_Tabs);

                    if (variable.IsReference)
                    {
                        tempAux = generator.newTemporal();
                        access_string += generator.get_stack(tempAux, temp, cant_Tabs);
                        temp = tempAux;
                    }

                    if (variable.DataType != DataType.BOOLEAN)
                    {
                        return new Returned(temp, variable.DataType, true, "", "",
                            access_string, variable.Default_value, variable.Position, variable.Position_reference);
                    }


                    if (this.TrueLabel == "")
                    {
                        this.TrueLabel = generator.newLabel();
                    }
                    if (this.FalseLabel == "")
                    {
                        this.FalseLabel = generator.newLabel();
                    }

                    access_string += generator.add_If(temp, "1", "==", this.TrueLabel, cant_Tabs);
                    access_string += generator.add_Goto(this.FalseLabel, cant_Tabs);
                    return new Returned("", DataType.BOOLEAN, false, this.TrueLabel,
                        this.FalseLabel, access_string, variable.Default_value, variable.Position, variable.Position_reference);
                }
            }

            else
            {
                Identifier variable = ambit.getVariable(this.id);

                if (variable.IsNull)
                {
                    set_error("La variable " + this.id + " No esta declarada", row, column);
                    return new Returned();
                }
                access_string += generator.get_stack(temp, variable.Position_global.ToString(), cant_Tabs);
                
                if (variable.DataType != DataType.BOOLEAN)
                {
                    return new Returned(temp, variable.DataType, true, "", "", access_string, variable.Default_value, variable.Position, variable.Position_global);
                }

                if (this.TrueLabel == "")
                {
                    this.TrueLabel = generator.newLabel();
                }
                if (this.FalseLabel == "")
                {
                    this.FalseLabel = generator.newLabel();
                }
                access_string += generator.add_If(temp, "1", "==", this.TrueLabel, cant_Tabs);
                access_string += generator.add_Goto(this.FalseLabel, cant_Tabs);

                return new Returned("", variable.DataType, false, this.TrueLabel,
                    this.FalseLabel, access_string, variable.Default_value, variable.Position, variable.Position_global);

            }*/


        }



        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
