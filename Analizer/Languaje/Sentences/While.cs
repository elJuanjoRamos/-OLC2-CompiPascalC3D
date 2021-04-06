using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class While : Instruction
    {
        private Expresion condition;
        private Sentence sentences;
        private int row;
        private int column;
        private int cant_tabs;
        public While(Expresion condition, Sentence sentences, int row, int col, int cant_tabs)
            : base("While")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.row = row;
            this.column = col;
            this.cant_tabs = cant_tabs;
        }
        public override string Execute(Ambit ambit)
        {
            var generator = C3D.C3DController.Instance;
            var whileAmbit = new Ambit(ambit, ambit.Ambit_name+"_While", "While", false);
            var label_recurrencia = generator.newLabel();
    
            generator.save_comment("Inicia While", cant_tabs);
            generator.addLabel(label_recurrencia, cant_tabs);

            //CONDICION
            var cond = condition.Execute(whileAmbit);

            if (cond.getDataType != DataType.BOOLEAN)
            {
                set_error("La condicion del While no es booleana", row, column);
                return null;
            }

            whileAmbit.Break = cond.FalseLabel;
            whileAmbit.Continue = label_recurrencia;

            //TAG VERDADERA
            generator.addLabel(cond.TrueLabel, cant_tabs);
            

            //EJECUTA SENTENCIAS
            var result = this.sentences.Execute(ambit);

            if (result == null)
            {
                return null;
            }
            
            generator.add_Goto(label_recurrencia, cant_tabs + 1);

            generator.addLabel(cond.FalseLabel, cant_tabs);
            generator.save_comment("Fin while", cant_tabs);
            


            return "executed";
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
