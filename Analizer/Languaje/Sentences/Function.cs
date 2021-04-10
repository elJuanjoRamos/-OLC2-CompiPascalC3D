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
        private DataType tipe;
        private Instruction sentences;
        private int row;
        private int column;
        private bool isProcedure;
        private string retorno;
        private bool esHija;
        private string padre_inmediato;
        private string uniqId;

        public Function(string id, LinkedList<Instruction> parametos, LinkedList<Instruction> declas,
            LinkedList<Instruction> functs, string tipe, Instruction sentences, bool isProcedure, 
            bool eshija, string padre, string uniq_id, int row, int col)
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
            this.esHija = eshija;
            this.padre_inmediato = padre;
            this.uniqId = uniq_id;
        }
        public override string Execute(Ambit ambit)
        {

            if (ambit.Anterior != null)
            {
                this.UniqId = ambit.Ambit_name + "_" + id;
            }

            ambit.saveFuncion(this.id, this);

            var generator = C3D.C3DController.Instance;

            
            var texto = "Function";
            if (IsProcedure)
            {
                texto = "Procedure";
            }

            Ambit ambit_func = new Ambit(ambit, this.uniqId, texto, false);

            //FUNCIONES HIJAS
            foreach (var fun_hija in funciones_hijas)
            {
                var result = fun_hija.Execute(ambit_func);
                if (result == null)
                {
                    return null;
                }

            }


            generator.save_code("void " + uniqId + "(" + ") { \n", 0);

            //DECLARACIONES 
            foreach (var declas in declaraciones)
            {
                var result = declas.Execute(ambit_func);
                if (result == null)
                {
                    return null;
                }
            }

            

            //INSTRUCCIONES
            var instrucciones = sentences.Execute(ambit_func);
            if (instrucciones == null)
            {
                return null;
            }

            generator.save_code("return;\n}", 0);

            return "executed";

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
        public Instruction Sentences { get => sentences; set => sentences = value; }
        public bool IsProcedure { get => isProcedure; set => isProcedure = value; }
        public DataType Tipe { get => tipe; set => tipe = value; }
        public string Retorno { get => retorno; set => retorno = value; }
        public string Id { get => id; set => id = value; }
        public LinkedList<Instruction> Declaraciones { get => declaraciones; set => declaraciones = value; }
        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public LinkedList<Instruction> Funciones_hijas { get => funciones_hijas; set => funciones_hijas = value; }
        public string UniqId { get => uniqId; set => uniqId = value; }
        public string Padre_inmediato { get => padre_inmediato; set => padre_inmediato = value; }
    }
}
