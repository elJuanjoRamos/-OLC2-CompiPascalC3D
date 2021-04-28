using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    public class Function : Instruction
    {
        private string id;
        private LinkedList<Instruction> parametos;
        private LinkedList<Instruction> declaraciones;
        private LinkedList<Instruction> funciones_hijas;
        private LinkedList<Instruction> sentences;
        private DataType tipe;
        private int row;
        private int column;
        private string retorno;
        private string uniqId;
        private string tipo;
        private bool isProcedure;
        public Function(string id, LinkedList<Instruction> parametos, LinkedList<Instruction> declas,
            LinkedList<Instruction> functs, string tipe, LinkedList<Instruction> sentences, bool isProcedure, 
            string uniq_id, int row, int col)
        : base("Function")
        {
            this.retorno = "-";
            this.id = id;
            this.parametos = parametos;
            this.declaraciones = declas;
            this.tipe = GetDataType(tipe);
            this.sentences = sentences;
            this.isProcedure = isProcedure;
            this.funciones_hijas= functs;
            this.row = row;
            this.column = col;
            this.uniqId = uniq_id;
            this.tipo = IsProcedure ? "Procedure" : "Function";
        }
        public override object Execute(Ambit ambit)
        {

            if (ambit.Anterior != null)
            {
                this.UniqId = ambit.Ambit_name + "_" + id;
            }

            ambit.saveFuncion(this.id, this);

            var generator = C3D.C3DController.Instance;
            generator.update_posision_global();

            var funcion_total = generator.save_code("void " + uniqId + "(" + ") { \n", 0);



            var tempo_return = "";
            var exit_label = "";
            var size = 0;
            if (!IsProcedure)
            {
                tempo_return = "T13";
                exit_label = generator.newLabel();
                funcion_total += generator.save_code("//Temporal de retorno", 1);
                funcion_total += generator.addExpression(tempo_return, "SP", "0", "+", 1);
                size = 1;
            }

            Ambit ambit_func = new Ambit(ambit, this.uniqId, tipo, tempo_return, exit_label, !isProcedure, this.tipe, size);


            foreach (var param in parametos)
            {
                Declaration dec = (Declaration)param;
                ambit_func.saveVarFunction(dec.Id, "0", "0", dec.Type, dec.isRefer, "Parameter", 0);
            }

            //DECLARACIONES 
            foreach (var declas in declaraciones)
            {
                var result = declas.Execute(ambit_func);
                if (result == null)
                {
                    return null;
                }
                funcion_total += result;
            }

            //FUNCIONES HIJAS
            var funcion_hija = "";
            foreach (var fun_hija in funciones_hijas)
            {
                var result = fun_hija.Execute(ambit_func);
                if (result == null)
                {
                    return null;
                }
                funcion_hija += result;
            }

            //INSTRUCCIONES
            foreach (Instruction instruction in sentences)
            {               
                var instruccion = instruction.Execute(ambit_func);

                if (instruccion is Call)
                {
                    funcion_total += generator.save_comment("Inicia salvado de temporales ", 1, false);
                    //DECLARACIONES 
                    foreach (var declas in declaraciones)
                    {
                        var result = declas.Execute(ambit_func);
                        if (result == null)
                        {
                            return null;
                        }
                        funcion_total += result;
                    }
                    funcion_total += generator.save_comment("Fin salvado de temporales ", 1, false);
                }

                if (instruccion == null)
                {
                    return null;
                }
                funcion_total += instruccion;
            }


            if (!isProcedure)
            {
                funcion_total += generator.addLabel(exit_label, 1);
            }

            funcion_total += generator.save_code(" return;\n", 2);
            funcion_total += generator.save_code("}\n", 0);



            ReporteController.Instance.save_ambit(ambit_func, ambit_func.Ambit_name);

            return funcion_hija + funcion_total;
        }



        public DataType GetDataType(string d)
        {
            if (d.Equals("integer"))
            {
                this.retorno = "0";
                return DataType.INTEGER;
            }
            else if (d.Equals("boolean"))
            {
                this.retorno = "false";
                return DataType.BOOLEAN;
            }
            else if (d.Equals("real"))
            {
                this.retorno = "0";
                return DataType.REAL;
            }
            else if (d.Equals("any"))
            {
                return DataType.ANY;
            }
            this.retorno = "-";
            return DataType.STRING;

        }



        public Instruction getParameterAt(int i)
        {
            var cont = 0;
            foreach (var item in parametos)
            {
                if (cont == i)
                {
                    return item;
                }
                cont = cont + 1;

            }
            return null;
        }
        public LinkedList<Instruction> Parametos { get => parametos; set => parametos = value; }
        public LinkedList<Instruction> Sentences { get => sentences; set => sentences = value; }
        public LinkedList<Instruction> Declaraciones { get => declaraciones; set => declaraciones = value; }
        public string Retorno { get => retorno; set => retorno = value; }
        public string Id { get => id; set => id = value; }
        public string UniqId { get => uniqId; set => uniqId = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public LinkedList<Instruction> Funciones_hijas { get => funciones_hijas; set => funciones_hijas = value; }
        public bool IsProcedure { get => isProcedure; set => isProcedure = value; }
        public DataType Tipe { get => tipe; set => tipe = value; }

    }
}
