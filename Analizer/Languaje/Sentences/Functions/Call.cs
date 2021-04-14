using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
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
            //GUARDAR LOS PARAMETROS EN LA TABLA DE SIMBOLOS

            Ambit function_ambit = new Ambit();

            if (funcion_llamada.IsProcedure)
            {
                function_ambit = new Ambit(ambit, funcion_llamada.Id, "Procedure", false, ambit.IsFunction);
            }
            else
            {
                function_ambit = new Ambit(ambit, funcion_llamada.Id, "Function", false, ambit.IsFunction);
            }
            var generator = C3D.C3DController.Instance;


            var size = ambit.Size;


            var paramsValues = new ArrayList();

            for (int i = 0; i < parametros.Count; i++)
            {
                var variable = (Declaration)(funcion_llamada.getParameterAt(i));

                
                var result = ((Expresion)parametros[i]).Execute(ambit);
                call_String += result.Texto_anterior;

                if (variable.Type == result.getDataType)
                {
                    function_ambit.setVariableFuncion(variable.Id, result.Value, result.Valor_original, result.getDataType, i, "Parameter");
                    paramsValues.Add(result);
                }
                else
                {
                    set_error("El tipo " + result.getDataType + " no es asignable con " + variable.Type, Row, Column);
                    return null;
                }


            }

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
