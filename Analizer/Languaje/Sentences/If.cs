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
        private Instruction sentences;
        private Instruction elif;
        private bool isNull;
        public int row;
        public int column;
        public int tabs;
        public If(Expresion condition, Instruction sentences, Instruction elif, int ro, int co, int ct)
           : base("If")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.elif = elif;
            this.IsNull = false;
            this.row = ro;
            this.column = co;
            this.tabs = ct;
        }

        public If()
            : base("IF")
        {
            this.IsNull = true;
        }


        public override string Execute(Ambit ambit)
        {
            var generator = C3DController.Instance;
            generator.save_comment("Inicia If");

            
            //AMBITO IF
            var ifAmbit = new Ambit(ambit, ambit.Ambit_name, "If", false);
            //CONDICION
            var condicion = condition.Execute(ambit);
            //VERIFICA QUE LLA CONDICION SEA BOOLEANA
            if (condicion.getDataType != DataType.BOOLEAN)
            {
                setError("Semantico - La condicion del If no es booleana", row, column);
                return "null";
            }

            generator.addLabel(condicion.TrueLabel);

            if (sentences.IsNull)
            {
                return "executed";
            }
            //SENTENCIAS
            var if_sentencias = this.sentences.Execute(ifAmbit);
            if (if_sentencias == "null")
            {
                return "null";
            }

            if (!this.elif.IsNull)
            {
                var tempLbl = generator.newLabel();
                generator.add_Goto(tempLbl);
                generator.addLabel(condicion.FalseLabel);

                var else_sentence = this.elif.Execute(ifAmbit);
                generator.addLabel(tempLbl);
                if (else_sentence == "null")
                {
                    return "null";
                }
            } else
            {
                generator.addLabel(condicion.FalseLabel);
            }

            return "executed";
        }
        public void setError(string text, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(text, row, column);
            ConsolaController.Instance.Add(text + " - Row: " + row + " - Col: " + column + "\n");
        }
        public string add_tabs(string text)
        {
            var tabu = "";
            for (int i = 0; i < tabs; i++)
            {
                tabu = tabu + " ";
            }
            return tabu + text;
        }
    }
}
