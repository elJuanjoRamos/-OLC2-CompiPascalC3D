using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Expressions;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Call : Instruction
    {

        private string id;
        private ArrayList parametros;
        private int row;
        private int column;
        private int cant_tabs;
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Call(string id, ArrayList expresion, int row, int col, int ct) :
            base("Call")
        {
            this.id = id;
            this.parametros = expresion;
            this.row = row;
            this.column = col;
            this.cant_tabs = ct;
        }


        public override object Execute(Ambit ambit)
        {
            var call_String = "";


            var funcion_llamada = ambit.getFuncion(this.id);

            //VALIDACION DE EXISTENCIA
            if (funcion_llamada == null)
            {
                set_error("La funcion o procediminento '" + this.id + "' no esta definido", Row, Column);
                return null;
            }

            //VALIDACION DE MISMA CANTIDAD DE PARAMETROS
            if (funcion_llamada.Parametos.Count != parametros.Count)
            {
                set_error("La funcion '" + this.id + "' no recibe la misma cantidad de parametros", Row, Column);
                return null;
            }


            var paramsValues = new ArrayList();

            var generator = C3D.C3DController.Instance;


            call_String  += generator.save_Temps(ambit, cant_tabs); //Guardo temporales

            int size = generator.get_size();

            //AMBITO DE LA FUNCION
            var tipo = (funcion_llamada.IsProcedure ? "Procedure" : "Function");

            Ambit function_ambit = new Ambit(ambit, funcion_llamada.UniqId, tipo, "", "", !funcion_llamada.IsProcedure, funcion_llamada.Tipe, size);


            //SE GUARDAN LOS PARAMETROS EN EL AMBITO
            foreach (var param in funcion_llamada.Parametos)
            {
                Declaration dec = (Declaration)param;
                function_ambit.saveVarFunction(dec.Id, "0", "0", dec.Type, dec.isRefer, "Parameter", 0);
            }

            //SE ENVIAN LOS PARAMTETROS POR REFERENCIA Y VALOR
            for (int i = 0; i < parametros.Count; i++)
            {

                var parametro = (Declaration)(funcion_llamada.getParameterAt(i));

                var result = ((Expresion)parametros[i]).Execute(ambit);
                call_String += result.Texto_anterior;

                if (parametro.Type == result.getDataType)
                {

                    if (parametro.isRefer && !(parametros[i] is Literal))
                    {
                        result.Value = result.Pos_refer.ToString();
                    }
                    function_ambit.setVariableFuncion(parametro.Id, result.Value,
                        result.Valor_original, result.getDataType, parametro.isRefer, "Parameter", result.Pos_refer, false);

                    paramsValues.Add(result);
                }
                else
                {
                    set_error("El tipo " + result.getDataType + " no es asignable con " + parametro.Type, Row, Column);
                    return null;
                }
            }

            var temp = generator.newTemporal();
            generator.freeTemp(temp);

            call_String += generator.save_comment("Inicia Llamada: " + funcion_llamada.UniqId, cant_tabs, false);

            //Paso de parametros en cambio simulado
            if (paramsValues.Count != 0)
            {
                var index = (funcion_llamada.IsProcedure) ? ambit.Size : ambit.Size + 1;
                call_String += generator.save_comment("Inicia:Parametros, Cambio de ambito", cant_tabs, false);
                call_String +=  generator.addExpression(temp, "SP", (index).ToString(), "+", cant_tabs); //+1 porque la posicion 0 es para el retorno;

                int i = 0;
                foreach (Returned value in paramsValues)
                {
                    i++;
                    call_String += generator.set_stack(temp, value.getValue(), cant_tabs);
                    if (i != paramsValues.Count)
                    {
                        call_String += generator.addExpression(temp, temp, "1", "+", cant_tabs);
                    }
                }
                call_String += generator.save_comment("Fin:Parametros, Cambio de ambito", cant_tabs, true);
            }

            call_String += generator.next_Env(ambit.Size, cant_tabs);
            call_String += generator.save_code(funcion_llamada.UniqId + "();", cant_tabs);
            call_String += generator.ant_Env(ambit.Size, cant_tabs);

            call_String +=  generator.recoverTemps(ambit, size, cant_tabs);
            

            call_String += generator.save_comment("Fin Llamada: " + funcion_llamada.UniqId, cant_tabs, true);


            /*var funcion_llamada = ambit.getFuncion(this.id);

            //VALIDACION DE EXISTENCIA
            if (funcion_llamada == null)
            {
                set_error("La funcion o procediminento '" + this.id + "' no esta definido", Row, Column);
                return null;
            }
            //VALIDACION DE MISMA CANTIDAD DE PARAMETROS
            if (funcion_llamada.Parametos.Count != parametros.Count)
            {
                set_error("La funcion '" + this.id + "' no recibe la misma cantidad de parametros", Row, Column);
                return null;
            }



            //AMBITO DE LA FUNCION
            var tipo = (funcion_llamada.IsProcedure ? "Procedure" : "Function");
            var size = (funcion_llamada.IsProcedure ? 0 : 1);


            //INSTANCIA DEL GENERADOR DE CODIGO
            var generator = C3D.C3DController.Instance;
            //generator.update_posision_global();






            //PASO DE PARAMETROS
            var paramsValues = new ArrayList();

            Ambit function_ambit = new Ambit(ambit, funcion_llamada.UniqId, tipo, "", "", !funcion_llamada.IsProcedure, funcion_llamada.Tipe, size);


            //SE GUARDAN LOS PARAMETROS EN EL AMBITO
            foreach (var param in funcion_llamada.Parametos)
            {
                Declaration dec = (Declaration)param;
                function_ambit.saveVarFunction(dec.Id, "0", "0", dec.Type, dec.isRefer, "Parameter", 0);
            }


            //SE ENVIAN LOS PARAMTETROS POR REFERENCIA Y VALOR
            for (int i = 0; i < parametros.Count; i++)
            {

                var parametro = (Declaration)(funcion_llamada.getParameterAt(i));

                var result = ((Expresion)parametros[i]).Execute(ambit);
                call_String += result.Texto_anterior;

                if (parametro.Type == result.getDataType)
                {

                    if (parametro.isRefer && !(parametros[i] is Literal) )
                    {
                        result.Value = result.Pos_refer.ToString();
                    }
                    function_ambit.setVariableFuncion(parametro.Id, result.Value,
                        result.Valor_original, result.getDataType, parametro.isRefer, "Parameter", result.Pos_refer, false);

                    paramsValues.Add(result);
                }
                else
                {
                    set_error("El tipo " + result.getDataType + " no es asignable con " + parametro.Type, Row, Column);
                    return null;
                }
            }

            call_String += generator.save_comment("Inicia Llamada: " + funcion_llamada.UniqId, cant_tabs, false);



            //COPIA DE LOS TEMPORALES
            call_String += generator.save_comment("Inicia Salvado Temporales: " + ambit.Ambit_name, cant_tabs, false);

            var temp_save = generator.newTemporal();
            var temp_index = generator.newTemporal();
            for (int i = 0; i < ambit.Size; i++)
            {
                call_String += generator.addExpression(temp_index, "SP", i.ToString(), "+", cant_tabs);
                call_String += generator.get_stack(temp_save, temp_index, cant_tabs);
                call_String += generator.addExpression(temp_index, "SP", (ambit.Size + i).ToString(), "+", cant_tabs);
                call_String += generator.set_stack(temp_index, temp_save, cant_tabs);
            }
            call_String += generator.save_comment("Fin Salvado Temporales: " + ambit.Ambit_name, cant_tabs, true);



            //PASO DE PARAMETRO, CAMBIO SIMULADO
            if (paramsValues.Count > 0)
            {
                call_String += generator.save_comment("Inicia:Parametros, Cambio de ambito", cant_tabs, false);

                var temp = generator.newTemporal();

                var index = (funcion_llamada.IsProcedure) ? ambit.Size * 2 : ambit.Size * 2 + 1;


                call_String += generator.addExpression(temp, "SP", (index).ToString(), "+", cant_tabs);
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
                generator.free_temps(temp);
                call_String += generator.save_comment("Fin:Parametros, Cambio de ambito", cant_tabs, false);
            }
            call_String += generator.next_Env(ambit.Size * 2, cant_tabs);
            call_String += generator.save_code(funcion_llamada.UniqId + "();", cant_tabs);

            call_String += generator.ant_Env(ambit.Size * 2, cant_tabs);

            //COPIA DE LOS TEMPORALES
            call_String += generator.save_comment("Inicia Recuperado Temporales: " + ambit.Ambit_name, cant_tabs, false);
            for (int i = ambit.Size * 2-1; i >= ambit.Size; i--)
            {
                call_String += generator.addExpression(temp_index, "SP", (i).ToString(), "+", cant_tabs);
                call_String += generator.get_stack(temp_save, temp_index, cant_tabs);
                call_String += generator.addExpression(temp_index, "SP", (i - ambit.Size).ToString(), "+", cant_tabs);
                call_String += generator.set_stack(temp_index, temp_save, cant_tabs);
            }

            call_String += generator.save_comment("Fin Recuperado Temporales: " + ambit.Ambit_name, cant_tabs, true);





            call_String += generator.save_comment("Fin Llamada: " + funcion_llamada.UniqId, cant_tabs, true);*/



            return call_String;
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}