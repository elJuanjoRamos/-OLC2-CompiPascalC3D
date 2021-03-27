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

        public void save_comment(string comment)
        {
            this.code.Add(this.isFunc + "/***** "+ comment +" *****/");
        }

        //TEMPORALES

        public string newTemporal() {
            var temp = "T" + this.temporal_number++;
            this.tempStorage.Add(temp);
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

        public int save_Temps(Ambit ambit){

            if(this.tempStorage.Count > 0){

                var temp = this.newTemporal(); 
                this.freeTemp(temp);
            
                var size = 0;

                this.save_comment("Inicia guardado de temporales");

                
                this.addExpression(temp, "p", ambit.Size.ToString(), "+");

                foreach (var item in this.tempStorage)
                {
                    size++;
                    this.set_stack(temp, item.ToString());
                    if (size != this.tempStorage.Count)
                    {
                        this.addExpression(temp, temp, "1", "+");
                    }
                        
                }
                this.save_comment("Fin guardado de temporales");
            }
            var ptr = ambit.Size;
            ambit.Size = ptr + this.tempStorage.Count;
            return ptr;
        }








        //LABELS
        public string newLabel(){
            return "L" + this.label_number++;
        }


        public void addLabel(string label)
        {
            var text = this.isFunc + label + ":";
            this.code.Add(text);
        }


        ///STACK
        public void set_stack(string index, string value)
        {
            var texto = this.isFunc + "Stack[" + index + "] = " + value;

            this.code.Add(texto);
        }
        public void get_stack(string target, string index)
        {
            var texto = this.isFunc + target + " = Stack[" + index + "]"; 
            this.code.Add(texto);
        }

        //IF
        public void add_If(string left, string right, string operatorr, string label)
        {
            this.code.Add(this.isFunc+ "if (" + left + " " + operatorr  + " "  + right + ") goto " + label);
        }
        //GOTO
        public void add_Goto(string label)
        {
            this.code.Add(this.isFunc + "goto " + label);
        }

        public void addExpression(string target, string left, string right, string symbol_operator)
        {
            var text = this.isFunc + target + " = " + left + " " + symbol_operator + " " + right ;
            this.code.Add(text);
        }


        public string getCode()
        {
            var texto = "";

            foreach (var item in code)
            {
                texto += item.ToString() + "\n"; 
            }

            return texto;
        }



    }
}
