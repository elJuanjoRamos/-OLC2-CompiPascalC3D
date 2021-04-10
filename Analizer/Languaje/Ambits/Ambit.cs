using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Ambits
{
    public class Ambit
    {
        Dictionary<string, Identifier> variables;
        Dictionary<string, Function> functions;
        // Dictionary<string, Arrays> arrays;
        //Dictionary<string, string> procedures;
        private string ambit_name = "";
        private string actualfunc = "";
        private string unicId = "";
        private string idParent = "";
        private string _break = "";
        private string _continue = "";
        private bool change_continue = false;
        private string ambit_name_inmediato = "";
        private Ambit anterior;
        private int size = 1;
        private int size_relativ = 1;
        public Boolean ambit_null;


        public Ambit(Ambit a, string n, string ni, bool isnull)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.ambit_name = n;
            this.ambit_name_inmediato = ni;
            this.anterior = a;
            this.ambit_null = isnull;
            this.size = a.Size;
            this._break = a.Break;
            this._continue = a.Continue;
            if (ni.Equals("Procedure") || ni.Equals("Function"))
            {
                this.size = 1;
            }
        }

        public Ambit()
        {

            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            //this.procedures = new Dictionary<string, string>();
            this.ambit_null = true;
            this.ambit_name = "General";
            this.ambit_name_inmediato = "General";
            this.size = 1;
        }


        #region VARIABLES

        public Identifier save(string id, object valor, DataType type, bool esconstante, bool isAssigned, bool isheap)
        {
            Ambit amb = this;

            Identifier ident = new Identifier(valor.ToString(), id, type, esconstante, 
                isAssigned, this.size++, (anterior == null), isheap);
            
            
            if (!amb.Ambit_name_inmediato.Equals("Function"))
            {
                if (!amb.variables.ContainsKey(id.ToLower()))
                {
                    amb.variables[id.ToLower()] = (ident);
                }
            }
            else
            {
                saveVarFunction(ident);
            }

            return ident;

        }

        public void setVariableInAmbit(string id, string valor, DataType type, int pos)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Variables.ContainsKey(id.ToLower()))
                {
                    env.Variables[id.ToLower()] = new Identifier(valor, id, type, false, false,  pos, false, false);
                }
                env = env.anterior;
            }
        }
        public void setVariableFuncion(string id, string valor, DataType type, int posi)
        {
            Ambit env = this;

            if (env.Variables.ContainsKey(id.ToLower()))
            {
                env.Variables[id.ToLower()] = new Identifier(valor, id, type, false, false, posi, false, false);
            }
        }

        #region FUNCIONES
        public void saveVarFunction(Identifier ident)
        {
            Ambit amb = this;

            if (!amb.variables.ContainsKey(ident.Id.ToLower()))
            {
                amb.variables[ident.Id.ToLower()] = (ident);
            }

        }

        public void saveFuncion(string id, Function function)
        {
            Ambit amb = this;

            if (!amb.functions.ContainsKey(id.ToLower()))
            {
                amb.functions[id.ToLower()] = function;
            }
        }
        public void setFunction(string id, Function function)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Functions.ContainsKey(id.ToLower()))
                {
                    env.Functions[id.ToLower()] = function;
                    return;
                }
                env = env.anterior;
            }
        }
        public Function getFuncion(string id)
        {
            Ambit amb = this;
            while (amb != null)
            {
                if (amb.Functions.ContainsKey(id.ToLower()))
                {
                    return amb.Functions[id.ToLower()];
                }
                amb = amb.anterior;
            }
            return null;
        }
        #endregion

        public Identifier getVariable(string id)
        {
            Identifier identifier = new Identifier();
            Ambit amb = this;
            while (amb != null)
            {
                if (amb.Variables.ContainsKey(id.ToLower()))
                {
                    identifier = amb.Variables[id.ToLower()];
                    break;
                }
                amb = amb.anterior;
            }
            return identifier;
        }

        public Identifier getVariableFunctionInAmbit(string id)
        {
            Identifier identifier = new Identifier();
            Ambit amb = this;
            if (amb.Variables.ContainsKey(id))
            {
                identifier = amb.Variables[id];
            }
            return identifier;
        }
        #endregion
        



        public int Size { get => size; set => size = value; }
        internal Dictionary<string, Identifier> Variables { get => variables; set => variables = value; }
        public string Ambit_name_inmediato { get => ambit_name_inmediato; set => ambit_name_inmediato = value; }
        public string Ambit_name { get => ambit_name; set => ambit_name = value; }
        public string Break { get => _break; set => _break = value; }
        public string Continue { get => _continue; set => _continue = value; }
        public bool Change_continue { get => change_continue; set => change_continue = value; }
        public Dictionary<string, Function> Functions { get => functions; set => functions = value; }
        public Ambit Anterior { get => anterior; set => anterior = value; }
        public int Size_relativ { get => size_relativ; set => size_relativ = value; }
    }
}
