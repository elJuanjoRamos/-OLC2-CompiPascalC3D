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



            //AMBITO DE LA FUNCION
            var tipo = (funcion_llamada.IsProcedure ? "Procedure" : "Function");








            //INSTANCIA DEL GENERADOR DE CODIGO
            var generator = C3D.C3DController.Instance;
            generator.update_posision_global();


            //COPIA DE LOS TEMPORALES
            var tempStorage = generator.getTempStorage();


            //PASO DE PARAMETROS
            var paramsValues = new ArrayList();


            //TEXTO DE LA FUNCION
            var funcion_total = generator.save_code("void " + funcion_llamada.UniqId 
                + "(" + ") { \n", 0);

            var tempo_return = "T13";
            var exit_label = "";
            if (!funcion_llamada.IsProcedure)
            {
                exit_label = generator.newLabel();
                funcion_total += generator.save_code("//Temporal de retorno", 1);
                funcion_total += generator.addExpression(tempo_return, "SP", "0", "+", 1);
            }
            Ambit function_ambit = new Ambit(ambit, funcion_llamada.UniqId, tipo, tempo_return, exit_label, !funcion_llamada.IsProcedure, funcion_llamada.Tipe);


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
                    
                    if (parametro.isRefer)
                    {
                        result.Value = result.Pos_refer.ToString();
                    }
                    function_ambit.setVariableFuncion(parametro.Id, result.Value,
                        result.Valor_original, result.getDataType,  parametro.isRefer, "Parameter", result.Pos_refer);

                    paramsValues.Add(result);
                }
                else
                {
                    set_error("El tipo " + result.getDataType + " no es asignable con " + parametro.Type, Row, Column);
                    return null;
                }
            }


            //SE HACEN LAS DECLARACIONES Y FUNCIONES HIJAS 
            //DECLARACIONES 
            foreach (var declas in funcion_llamada.Declaraciones)
            {
                var result = declas.Execute(function_ambit);
                if (result == null)
                {
                    return null;
                }
                funcion_total += result;
            }

            //FUNCIONES HIJAS
            var funcion_hija = "";
            foreach (var fun_hija in funcion_llamada.Funciones_hijas)
            {
                var result = fun_hija.Execute(function_ambit);
                if (result == null)
                {
                    return null;
                }
                funcion_hija += result;
            }

            //INSTRUCCIONES
            foreach (Instruction instruction in funcion_llamada.Sentences)
            {
                var instruccion = instruction.Execute(function_ambit);
                if (instruccion == null)
                {
                    return null;
                }
                funcion_total += instruccion;
            }


            if (!funcion_llamada.IsProcedure)
            {
                funcion_total += generator.addLabel(exit_label, 1);
            }

            funcion_total += generator.save_code(" return;\n", 2);
            funcion_total += generator.save_code("}\n", 0);

            generator.set_function_code(funcion_total, funcion_llamada.UniqId);

            ReporteController.Instance.save_ambit(function_ambit, function_ambit.Ambit_name);




            call_String += generator.save_comment("Inicia Llamada: " + funcion_llamada.UniqId, cant_tabs, false);

            //PASO DE PARAMETRO, CAMBIO SIMULADO
            if (paramsValues.Count > 0)
            {
                call_String += generator.save_comment("Inicia:Parametros, Cambio de ambito", cant_tabs, false);

                var temp = generator.newTemporal();


                call_String += generator.addExpression(temp, "SP", (ambit.Size+1).ToString(), "+", cant_tabs );
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
            call_String += generator.next_Env(ambit.Size, cant_tabs);
            call_String += generator.save_code(funcion_llamada.UniqId+"();", cant_tabs);
            
            //generator.get_stack(temp, "SP", cant_tabs);
            call_String += generator.ant_Env(ambit.Size, cant_tabs);
            //generator.freeTemp(temp);
            //generator.recoverTemps(ambit, size, cant_tabs);

            call_String += generator.save_comment("Fin Llamada: " + funcion_llamada.UniqId, cant_tabs, true);



            return call_String;
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
