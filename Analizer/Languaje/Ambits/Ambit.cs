using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using CompiPascalC3D.Analizer.Languaje.Sentences.Array;
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
        Dictionary<string, Arrays> arrays;
        // Dictionary<string, Arrays> arrays;
        private Ambit anterior;
        private string ambit_name = "";
        private string ambit_name_inmediato = "";
        private string _break = "";
        private string _continue = "";
        public bool ambit_null;
        private bool change_continue = false;
        private int size = 1;

        //VARIABLES PARA AMBITO DE FUNCION
        private bool isFunction = false;
        private string temp_return = "";
        private string _exit = "";
        private DataType tipo_fun;

        public Ambit(Ambit a, string n, string ni, bool isnull, bool isf)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.arrays = new Dictionary<string, Arrays>();
            this.ambit_name = n;
            this.ambit_name_inmediato = ni;
            this.anterior = a;
            this.ambit_null = isnull;
            this.size = a.Size;
            this._break = a.Break;
            this._exit = a.Exit;
            this._continue = a.Continue;
            this.isFunction = isf;
            this.Tipo_fun = a.Tipo_fun;
            this.temp_return = a.temp_return;
        }

        //CONSTRUCTOR PARA FUNCIONES
        public Ambit(Ambit a, string n, string ni, string tempoReturn, string exit_tag, bool isf, DataType dt)
        {
            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.arrays = new Dictionary<string, Arrays>();
            this.ambit_name = n;
            this.ambit_name_inmediato = ni;
            this.anterior = a;
            this.ambit_null = false;
            this.size = 1;
            this.temp_return = tempoReturn;
            this.isFunction = isf;
            this._exit = exit_tag;
            this.tipo_fun = dt;
        }

        public Ambit()
        {

            this.variables = new Dictionary<string, Identifier>();
            this.functions = new Dictionary<string, Function>();
            this.arrays = new Dictionary<string, Arrays>();
            //this.procedures = new Dictionary<string, string>();
            this.ambit_null = true;
            this.ambit_name = "General";
            this.ambit_name_inmediato = "General";
            this.size = 1;
            this.IsFunction = false;
            this.temp_return = "";

        }


        #region VARIABLES

        public Identifier save(string id, object valor, string valor_Def, DataType type, bool esconstante,
            bool isAssigned, bool isheap, bool isrefer, string tipo_dato)
        {
            Ambit amb = this;

            Identifier ident = new Identifier(valor.ToString(), valor_Def, id, type, esconstante, 
                isAssigned, this.size++, (anterior == null), isheap, isrefer, tipo_dato);
            
            
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

        public void setVariableInAmbit(string id, string valor, string val_Def, DataType type, int pos, bool isrefer, string tipo_Dato)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Variables.ContainsKey(id.ToLower()))
                {
                    env.Variables[id.ToLower()] = new Identifier(valor, val_Def, id, type, false, false,  pos, false, false, isrefer, tipo_Dato);
                }
                env = env.anterior;
            }
        }
        public void setVariableFuncion(string id, string valor, string valdef, DataType type, int posi, bool isrefe, string tipo_Dato)
        {
            Ambit env = this;

            if (env.Variables.ContainsKey(id.ToLower()))
            {
                env.Variables[id.ToLower()] = new Identifier(valor, valdef, id, type, false, false, posi, false, false, isrefe, tipo_Dato);
            }
        }
        public void setVariable(string id, string valor, string valdef, DataType type, bool isAssigned, 
            int posi, bool isglobal, bool isrefe, string tipo_Dato)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Variables.ContainsKey(id.ToLower()))
                {
                    env.Variables[id.ToLower()] = new 
                        Identifier(valor, valdef, id, type, false, isAssigned, posi, isglobal, false, isrefe, tipo_Dato);
                    return;
                }
                env = env.anterior;
            }
        }
        
      
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


  #region FUNCIONES
        public void saveVarFunction(Identifier ident)
        {
            Ambit amb = this;

            if (!amb.variables.ContainsKey(ident.Id.ToLower()))
            {
                amb.variables[ident.Id.ToLower()] = (ident);
            }

        }
        public void saveVarFunction(string id, string valor, string valdef, DataType type, bool isrefe, string tipo_Dato)
        {
            Ambit amb = this;

            if (!amb.variables.ContainsKey(id))
            {
                amb.variables[id] = (new Identifier(valor, valdef, id, type, false, false, Size++, false, false, isrefe, tipo_Dato));
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

        #region ARREGLOS

        public void saveArray(string id, Arrays arrays)
        {
            Ambit amb = this;

            if (!amb.Arrayss.ContainsKey(id.ToLower()))
            {
                amb.Arrayss[id.ToLower()] = arrays;
            }
        }


        public Arrays getArray(string id)
        {
            Ambit amb = this;

            while (amb != null)
            {
                if (amb.Arrayss.ContainsKey(id.ToLower()))
                {
                    return amb.Arrayss[id.ToLower()];
                }
                amb = amb.anterior;
            }


            return null;
        }

        public void setArray(string id, Arrays tipo_dato)
        {
            Ambit env = this;

            while (env != null)
            {
                if (env.Arrayss.ContainsKey(id.ToLower()))
                {
                    env.Arrayss[id.ToLower()] = tipo_dato;
                    return;
                }
                env = env.anterior;
            }
        }



        #endregion

        internal Dictionary<string, Identifier> Variables { get => variables; set => variables = value; }
        public Dictionary<string, Function> Functions { get => functions; set => functions = value; }
        public string Ambit_name_inmediato { get => ambit_name_inmediato; set => ambit_name_inmediato = value; }
        public string Ambit_name { get => ambit_name; set => ambit_name = value; }
        public string Break { get => _break; set => _break = value; }
        public string Continue { get => _continue; set => _continue = value; }
        public string Temp_return { get => temp_return; set => temp_return = value; }
        public string Exit { get => _exit; set => _exit = value; }
        public int Size { get => size; set => size = value; }
        public bool IsFunction { get => isFunction; set => isFunction = value; }
        public Ambit Anterior { get => anterior; set => anterior = value; }
        public DataType Tipo_fun { get => tipo_fun; set => tipo_fun = value; }
        public Dictionary<string, Arrays> Arrayss { get => arrays; set => arrays = value; }
    }
}
