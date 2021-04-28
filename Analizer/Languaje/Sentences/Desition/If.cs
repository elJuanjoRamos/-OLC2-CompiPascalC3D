using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class If : Instruction
    {
        private Expresion condition;
        private LinkedList<Instruction> sentences;
        private LinkedList<Instruction> elif;
        private string labelExit;
        private bool isNull;
        public int row;
        public int column;
        public int tabs;

        public bool IsNull { get => isNull; set => isNull = value; }
        public string LabelExit { get => labelExit; set => labelExit = value; }

        public If(Expresion condition, LinkedList<Instruction> sentences, LinkedList<Instruction> elif, int ro, int co, int ct)
           : base("If")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.elif = elif;
            this.isNull = false;
            this.row = ro;
            this.column = co;
            this.tabs = ct;
            this.labelExit = "";
        }

        public If()
            : base("IF")
        {
            this.IsNull = true;
        }


        public override object Execute(Ambit ambit)
        {
            var if_string = "";
            var generator = C3DController.Instance;
            if_string += generator.save_comment("Inicia If", tabs, false);

            
            //AMBITO IF
            var ifAmbit = new Ambit(ambit, ambit.Ambit_name+ "_If", "If", false, ambit.IsFunction);
            //CONDICION
            var condicion = condition.Execute(ambit);
            if_string += condicion.Texto_anterior;
            //VERIFICA QUE LLA CONDICION SEA BOOLEANA
            if (condicion.getDataType != DataType.BOOLEAN)
            {
                setError("Semantico - La condicion del If no es booleana", row, column);
                return "null";
            }

            if_string += generator.addLabel(condicion.TrueLabel, tabs);


            //SENTENCIAS
            var if_sentencias = "";
            foreach (Instruction instruction in sentences)
            {
                var result = instruction.Execute(ifAmbit);
                if (result == null)
                {
                    return null;
                }
                if_sentencias += result;
            }

           
            if_string += if_sentencias.ToString();

            if (this.elif.Count != 0)
            {
                var tempLbl = "";
                tempLbl = generator.newLabel();
                if_string += generator.add_Goto(tempLbl, tabs);
                if_string += generator.addLabel(condicion.FalseLabel, tabs);

                var else_sentence = "";

                foreach (Instruction inst in elif)
                {
                    if (inst is If)
                    {
                        ((If)inst).LabelExit = tempLbl;
                    }

                    var result = inst.Execute(ifAmbit);

                    if (result == null)
                    {
                        return null;
                    }
                    else_sentence += result;
                }


                if (else_sentence == null)
                {
                    return null;
                }
                if_string += else_sentence;
                if_string += generator.addLabel(tempLbl,tabs);
            } else
            {
                if_string += generator.addLabel(condicion.FalseLabel, tabs);
            }

            return if_string;
        }
        public void setError(string text, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(text, row, column);
            ConsolaController.Instance.Add(text + " - Row: " + row + " - Col: " + column + "\n");
        }
        
    }
}
