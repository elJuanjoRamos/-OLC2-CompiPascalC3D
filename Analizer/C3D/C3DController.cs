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

        public bool Native_str { get => native_str; set => native_str = value; }
        public bool Native_compare { get => native_compare; set => native_compare = value; }
        public bool Native_equals { get => native_equals; set => native_equals = value; }


        //VARIABLES
        private int temporal_number;
        private int label_number;
        private ArrayList code;
        private ArrayList code_function;
        private ArrayList tempStorage;
        private ArrayList tempNatives;
        private bool native_str;
        private bool native_compare;
        private bool native_equals;
        private string texto_general;
        private Stack micola;

        private C3DController()
        {
            this.temporal_number = 15;
            this.code = this.code_function = new ArrayList();
            this.tempStorage = this.tempNatives = new ArrayList();
            this.native_compare = this.native_str = false;
            this.micola = new Stack();
        }


        public void clearCode()
        {
            this.temporal_number = this.label_number = 15;
            this.code.Clear();
            this.code_function.Clear();
            this.tempStorage.Clear();
            this.tempNatives.Clear();
            this.native_compare = this.native_str = this.native_equals = false;
            this.micola.Clear();
        }

        public string save_comment(string comment, int cant_tabs, bool isclose)
        {
            var texto = getTabs(cant_tabs, false) + "/***** " + comment + " *****/";
            if (isclose)
            {
                texto += "\n";
            }
            //this.code.Add(texto);
            return texto + "\n";
        }
        

        public string save_code(string comment, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + comment;
            //this.code.Add(texto);
            this.texto_general += "\n" + texto;
            return texto + "\n";
        }

        //TEMPORALES

        public string newTemporal() {
            var temp = "";

            if (micola.Count == 0)
            {
                temp = "T" + this.temporal_number++;
                this.tempStorage.Add(temp);
            } else
            {
                temp = micola.Pop().ToString();
            }

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

        public void free_temps(string temp)
        {
            this.micola.Push(temp);
        }  

        public int save_Temps(Ambit ambit, int cant_tabs){

            if(this.tempStorage.Count > 0){

                var temp = this.newTemporal(); 
                this.freeTemp(temp);
            
                var size = 0;

                this.save_comment("Inicia guardado de temporales", cant_tabs, false);

                
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
                this.save_comment("Fin guardado de temporales", cant_tabs, true);
            }
            var ptr = ambit.Size;
            ambit.Size = ptr + this.tempStorage.Count;
            return ptr;
        }


        public void recoverTemps(Ambit ambit, int pos, int cant_tabs)
        {
            if (this.tempStorage.Count > 0)
            {
                var temp = this.newTemporal(); 
                this.freeTemp(temp);
                var size = 0;

                this.save_comment("Inicia recuperado de temporales", cant_tabs, false);
                this.addExpression(temp, "p", pos.ToString(), "+", cant_tabs);

                foreach (string item in tempStorage)
                {
                    size++;
                    this.get_stack(item, temp, cant_tabs);
                    if (size != this.tempStorage.Count)
                    {
                        this.addExpression(temp, temp, "1", "+", cant_tabs);
                    }
                }
                
                this.save_comment("Finaliza recuperado de temporales", cant_tabs, true);
                ambit.Size = pos;
            }
        }





        //LABELS
        public string newLabel(){
            this.label_number++;
            return "L" + this.label_number;
        }


        public string addLabel(string label, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, true) + label + ":";
            //this.code.Add(texto);
            this.texto_general += "\n" + texto;
            return texto + "\n";
        }

        public string replace_temp(string label, string replace, string texto)
        {

            if (texto.Contains(replace))
            {
                texto = texto.Replace(replace, label);
            }

            return texto;
            /*for (int i = 0; i < this.code.Count; i++)
            {
                string element = this.code[i].ToString();
                if (element.Contains(replace))
                {
                    var temp = element.Replace(replace, label);
                    this.code[i] = temp;
                }
            }*/
        }

        ///STACK
        public string set_stack(string index, string value, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "Stack[" + IsNumeric(index) + "] = " + value + ";";
            //this.code.Add(texto);
            return texto + "\n";
        }
        public string get_stack(string target, string index,  int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + target + " = Stack[" + IsNumeric(index) + "];";
            //this.code.Add(texto);
            return texto + "\n";
        }

        public string next_Env(int size, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "SP = SP + " + size + ";";
            //this.code.Add(texto);
            return texto + "\n";
        }

        public string ant_Env(int size, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "SP = SP - " + size + ";";
            //this.code.Add(texto);
            return texto + "\n";
        }


        //HEAP
        public string next_Heap(int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "HP = HP + 1;";
            //this.code.Add(texto);
            return texto + "\n";
        }

        public string get_Heap(string target, string index, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + target + "= Heap[" + IsNumeric(index) + "];";
            this.code.Add(texto);
            return texto + "\n";
        }

        public string set_Heap(string index, string value, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "Heap[" + IsNumeric(index) + "] = " + value + ";";
            //this.code.Add(texto);
            return texto + "\n"; 
        }


        //AMBITOS
        public string add_next_ambit(int size, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "SP = SP + " + size + ";";
            //this.code.Add(texto);
            return texto + "\n";
        }

        //IF
        public string add_If(string left, string right, string operatorr, string label, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "if (" + left + " " + operatorr + " " + right + ") goto " + label + ";";
            //this.code.Add(texto);
            return texto + "\n";
        }
        //GOTO
        public string add_Goto(string label, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "goto " + label + ";";
            //this.code.Add(texto);
            return texto + "\n";
        }


        //EXPRESION
        public string addExpression(string target, string left, string right, string symbol_operator, int cant_tabs)
        {
            var text = getTabs(cant_tabs, false) + target + " = " + left + symbol_operator + right + ";";
            //this.code.Add(text);
            return text + "\n";
        }


        //PRINT
        public string generate_print(string format, string value, string type, int cant_tabs)
        {
            var texto = getTabs(cant_tabs, false) + "printf(\"%" + format + "\"," + type +  value + ");";
            //this.code.Add(texto);
            return texto + "\n";
        }
        public string print_boolean(int cant_tabs, string istrue)
        {
            var str = istrue;
            var tab_S = getTabs(cant_tabs, false);

            var texto = "";
            foreach (char cha in str)
            {
                //this.code.Add();
                texto += tab_S + "printf(\"%" + "c" + "\"," + (int)cha + ");\n";
            }

            return texto + "\n";
        }

        // OBTIENE LOS TEMPORALES
        public string getTemps()
        {
            var text = "float T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13";
            var cont = 0;

            if (tempStorage.Count == 0)
            {
                return text + ";";
            }

            foreach (var item in tempStorage)
            {
                text += "," +item.ToString();
                cont++;
                if (cont == tempStorage.Count)
                {
                    text += ";";
                }
            }

            return text + "\n\n";
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
            var texto = "";
            if (cant_tabs > 0)
            {
                for (int i = 0; i < cant_tabs; i++)
                {
                    texto += " ";
                }
            }

            return texto;
        }

        public string IsNumeric(string s)
        {
            if (s.Equals("HP") || s.Equals("SP"))
            {
                return s;
            }

            var _soyNumero = true;
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    _soyNumero = false;
                }
            }

            if (!_soyNumero)
            {
                return "(int)" + s;
            }
            return s;

        }


        public string print_natives()
        {


            var nativa_print = "";
            if (this.Native_str)
            {
                //this.tempNatives.Add("T1,T2");
                //NATIVA PARA IMRIMIR STRINGS
                nativa_print += save_code("void native_print_str(){", 0);
                nativa_print += addLabel("L1", 1);
                nativa_print += get_Heap("T2", "T1", 1);
                nativa_print += add_If("T2", "-1", "==", "L2", 1);
                nativa_print += generate_print("c", "T2", "(int)", 1);
                nativa_print += addExpression("T1", "T1", "1", "+", 1);
                nativa_print += add_Goto("L1", 1);
                nativa_print += addLabel("L2", 1);
                nativa_print += generate_print("c", "", "' " + "'", 1);
                nativa_print += save_code("}\n\n", 0);
            }

            var nativa_igual = "";
            if (this.native_equals)
            {
                
                
                //this.tempNatives.Add("T3,T4,T5,T6,T7");
                //NATIVA PARA COMPARAR STRING
                nativa_igual += save_code("int native_cmp_str(){", 0)
                +addLabel("L3", 1)
                +get_Heap("T5", "T3", 2)
                +get_Heap("T6", "T4", 2)
                +add_If("T5", "T6", "==", "L4", 2)
                +add_Goto("L5", 2)
                +addLabel("L4", 1)

                +addExpression("T7", "0", "1", "-", 2)
                +add_If("T5", "T7", "==", "L6", 2)
                +add_Goto("L7", 2)
                +addLabel("L6", 1)
                +add_If("T6", "T7", "==", "L8", 2)
                +add_Goto("L7", 2)
                +addLabel("L7", 1)
                +addExpression("T3", "T3", "1", "+", 2)
                +addExpression("T4", "T4", "1", "+", 2)
                +add_Goto("L3", 2)
                +addLabel("L8", 1)
                +save_code("return 1;", 2)
                +addLabel("L5", 1)
                +save_code("return 0;", 2)
                +save_code("}\n\n", 0);
            }
            var nativa_concat = "";
            
            if (this.Native_compare)
            {
                nativa_concat += save_code("void native_concat_str(){", 0)
                +save_code("T12 = HP; //valor de retorno", 1)
                +addLabel("L9", 1)
                +get_Heap("T11", "T9", 2)
                +add_If("T11", "-1", "==", "L10", 2)
                +set_Heap("HP", "T11", 2)
                +addExpression("T9", "T9", "1", "+", 2)
                +addExpression("HP", "HP", "1", "+", 2)
                +add_Goto("L9", 2)
                +addLabel("L10", 1)
                +get_Heap("T11", "T10", 2)
                +add_If("T11", "-1", "==", "L11", 2)
                +set_Heap("HP", "T11", 2)
                +addExpression("T10", "T10", "1", "+", 2)
                +addExpression("HP", "HP", "1", "+", 2)
                +add_Goto("L10", 2)
                +addLabel("L11", 1)
                +set_Heap("HP", "-1", 2)
                +addExpression("HP", "HP", "1", "+", 2)

                +save_code("}\n\n", 0);
            }

            return nativa_print + nativa_concat + nativa_igual;
        }



        public void save_Genenal(string nativas, string funciones, string general)
        {
            this.texto_general  = nativas + funciones + general;
        }
        public string get_Genenal()
        {
            return this.texto_general;
        }
    }
}
