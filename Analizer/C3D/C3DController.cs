using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.C3D
{
    class C3DController
    {
        //SINGLETON
        private readonly static C3DController _instance = new C3DController();

        
        public static C3DController Instance
        {
            get
            {
                return _instance;
            }
        }


        //VARIABLES
        private int temporal_number;
        private int label_number;
        private ArrayList code;
        private ArrayList tempStorage;
        private string isFunc;

        private C3DController()
        {
            this.temporal_number = 1;
            this.label_number = 1;
            this.code = new ArrayList();
            this.tempStorage = new ArrayList();
            this.isFunc = "";
        }


        public void clearCode()
        {
            this.temporal_number = this.temporal_number = 1;
            this.code.Clear();
            this.tempStorage.Clear();
        }

        public void save_comment(string comment, int cant_tabs)
        {
            this.code.Add(getTabs(cant_tabs, false) + "/***** "+ comment +" *****/");
        }

        //TEMPORALES

        public string newTemporal() {
            var temp = "T" + this.temporal_number;
            this.tempStorage.Add(temp);
            this.temporal_number++;
            return temp;
        }


        public  void add_temps(string temp)
        {

            if (!this.tempStorage.Contains(temp))
            {
                this.tempStorage.Add(temp);
            }
        }
        public void freeTemp(string temp)
        {
            if (this.tempStorage.Contains(temp))
            {
                this.tempStorage.Remove(temp);
            }
        }

        public int save_Temps(Ambit ambit, int cant_tabs){

            if(this.tempStorage.Count > 0){

                var temp = this.newTemporal(); 
                this.freeTemp(temp);
            
                var size = 0;

                this.save_comment("Inicia guardado de temporales", cant_tabs);

                
                this.addExpression(temp, "p", ambit.Size.ToString(), "+", cant_tabs);

                foreach (var item in this.tempStorage)
                {
                    size++;
                    this.set_stack(temp, item.ToString(), cant_tabs);
                    if (size != this.tempStorage.Count)
                    {
                        this.addExpression(temp, temp, "1", "+", cant_tabs);
                    }
                        
                }
                this.save_comment("Fin guardado de temporales", cant_tabs);
            }
            var ptr = ambit.Size;
            ambit.Size = ptr + this.tempStorage.Count;
            return ptr;
        }








        //LABELS
        public string newLabel(){
            return "L" + this.label_number++;
        }


        public void addLabel(string label, int cant_tabs)
        {
            var text = getTabs(cant_tabs, true) + label + ":";
            this.code.Add(text);
        }


        ///STACK
        public void set_stack(string index, string value, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "Stack[" + index + "] = " + value;

            this.code.Add(texto);
        }
        public void get_stack(string target, string index,  int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + target + " = Stack[" + index + "]"; 
            this.code.Add(texto);
        }

        //IF
        public void add_If(string left, string right, string operatorr, string label, int cant_tabs)
        {
            this.code.Add(getTabs(cant_tabs, false) + "if (" + left + " " + operatorr  + " "  + right + ") goto " + label);
        }
        //GOTO
        public void add_Goto(string label, int cant_tabs)
        {
            this.code.Add(getTabs(cant_tabs, false) + "goto " + label);
        }


        //EXPRESION
        public void addExpression(string target, string left, string right, string symbol_operator, int cant_tabs)
        {
            var text = getTabs(cant_tabs, false) + target + " = " + left + " " + symbol_operator + " " + right ;
            this.code.Add(text);
        }

        //PRINT
        public void generate_print(string format, string value, string type, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "printf(%" + format + "," + type +  value + ");";  
            this.code.Add(texto);
        }

        // OBTIENE LOS TEMPORALES
        public string getTemps()
        {
            var text = "";
            var cont = 0;

            foreach (var item in tempStorage)
            {
                text += item.ToString();
                cont++;
                if (cont != tempStorage.Count)
                {
                    text += ",";
                } else
                {
                    text += ";";
                }
            }
            return "Int " +  text + "\n\n";
        }

        /// OBTIENE TODO EL CODIGO
        public string getCode()
        {
            var texto = "";

            foreach (var item in code)
            {
                texto += item.ToString() + "\n"; 
            }

            return texto;
        }

        private string getTabs(int cant_tabs, bool islabel)
        {
            this.isFunc = "";
            if (cant_tabs > 0)
            {
                for (int i = 0; i < cant_tabs; i++)
                {
                    this.isFunc += " ";
                }
            }

            return this.isFunc;
        }

    }
}
