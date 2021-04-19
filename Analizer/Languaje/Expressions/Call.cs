using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Expressions
{
    class CallFunction : Expresion
    {
        private string id;
        private ArrayList parametros;
        private int row;
        private int column;
        private int cant_tabs;
        public CallFunction(string id, ArrayList expresion, int r, int c, int cant_tabs) :
            base("Call")
        {
            this.id = id;
            this.parametros = expresion;
            this.row = r;
            this.column = c;
            this.cant_tabs = cant_tabs;
        }

        public override Returned Execute(Ambit ambit)
        {
            var funcion_llamada = ambit.getFuncion(this.id);
            if (funcion_llamada == null)
            {
                set_error("La funcion '" + this.id + "' no esta definido", row, column);
                return new Returned();
            }

            if (funcion_llamada.Parametos.Count != parametros.Count)
            {
                set_error("La funcion '" + this.id + "' no recibe la misma cantidad de parametros", row, column);
                return new Returned();

            }

            

            //GUARDAR LOS PARAMETROS EN LA TABLA DE SIMBOLOS

            Ambit function_ambit = new Ambit();


            if (funcion_llamada.IsProcedure)
            {
                set_error("El procedimiento'" + this.id + "' no puede asignarse como valor de retorno", row, column);
                return new Returned();

            }
            else
            {
                function_ambit = new Ambit(ambit, funcion_llamada.UniqId, "Function", ambit.Temp_return, ambit.Exit, ambit.IsFunction, ambit.Tipo_fun);
            }

            var generator = C3D.C3DController.Instance;


            //ETIQUETAS VERDADERAS Y FALSAS EN CASO DE QUE SEA BOOL
            if (funcion_llamada.Tipe == DataType.BOOLEAN)
            {
                if (this.TrueLabel == "")
                {
                    this.TrueLabel = generator.newLabel();
                }
                if (this.FalseLabel == "")
                {
                    this.FalseLabel = generator.newLabel();
                }
            }

            var call_String = "";

            var size = ambit.Size;


            var paramsValues = new ArrayList();

            


            for (int i = 0; i < parametros.Count; i++)
            {
                var variable = (Declaration)(funcion_llamada.getParameterAt(i));


                var result = ((Expresion)parametros[i]).Execute(ambit);
                call_String += result.Texto_anterior;

                if (variable.Type == result.getDataType)
                {
                    if (variable.isRefer)
                    {
                        result.Value = result.Pos_refer.ToString();
                    }

                    function_ambit.setVariableFuncion(variable.Id, result.Value,
                        result.Valor_original, result.getDataType, variable.isRefer, "Parameter", result.Pos_refer);

                    paramsValues.Add(result);
                }
                else
                {
                    set_error("El tipo " + result.getDataType + " no es asignable con " + variable.Type, row, column);
                    return null;
                }


            }

            call_String += generator.save_comment("Inicia Llamada: " + funcion_llamada.UniqId, cant_tabs, false);

            //PASO DE PARAMETRO, CAMBIO SIMULADO
            if (paramsValues.Count > 0)
            {
                call_String += generator.save_comment("Inicia:Parametros, Cambio de ambito", cant_tabs, false);

                var temp = generator.newTemporal();
                call_String += generator.addExpression(temp, "SP", (ambit.Size + 1).ToString(), "+", cant_tabs);
                int i = 0;
                foreach (Returned item in paramsValues)
                {
                    i++;
                    call_String += generator.set_stack(temp, item.Value, cant_tabs);
                    if (i != paramsValues.Count)
                    {
                        call_String += generator.addExpression(temp, temp, "1", "+", cant_tabs);
                    }
                }
                call_String += generator.save_comment("Fin:Parametros, Cambio de ambito", cant_tabs, false);
            }
            call_String += generator.next_Env(ambit.Size, cant_tabs);
            call_String += generator.save_code(funcion_llamada.UniqId + "();", cant_tabs);
            //generator.get_stack(temp, "SP", cant_tabs);
            call_String += generator.ant_Env(ambit.Size, cant_tabs);
            //generator.freeTemp(temp);
            //generator.recoverTemps(ambit, size, cant_tabs);
            call_String += generator.get_stack("T14", "T13", cant_tabs);
            call_String += generator.save_comment("Fin Llamada: " + funcion_llamada.UniqId, cant_tabs, true);

            if (funcion_llamada.Tipe == DataType.BOOLEAN)
            {
                call_String += generator.add_If("T14", "1", "==", this.TrueLabel, cant_tabs);
                call_String += generator.add_Goto(this.FalseLabel, cant_tabs);

            }

            return new Returned("T14", funcion_llamada.Tipe, true, this.TrueLabel, this.FalseLabel, call_String, "",0,0);

        }
        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
