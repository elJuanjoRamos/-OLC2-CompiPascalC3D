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
        public override object Execute(Ambit ambit)
        {
            var while_string = "";
            var generator = C3D.C3DController.Instance;
            var whileAmbit = new Ambit(ambit, ambit.Ambit_name, "While", false, ambit.IsFunction);
            var label_recurrencia = generator.newLabel();

            while_string += generator.save_comment("Inicia While", cant_tabs, false);
            while_string += generator.addLabel(label_recurrencia, cant_tabs);

            
            //CONDICION
            var cond = condition.Execute(whileAmbit);
            while_string += cond.Texto_anterior;

            if (cond.getDataType != DataType.BOOLEAN)
            {
                set_error("La condicion del While no es booleana", row, column);
                return null;
            }

            whileAmbit.Break = cond.FalseLabel;
            whileAmbit.Continue = label_recurrencia;

            //TAG VERDADERA
            while_string += generator.addLabel(cond.TrueLabel, cant_tabs);
            

            //EJECUTA SENTENCIAS
            var result = this.sentences.Execute(whileAmbit);

            if (result == null)
            {
                return null;
            }
            while_string += result;

            while_string += generator.add_Goto(label_recurrencia, cant_tabs + 1);

            while_string += generator.addLabel(cond.FalseLabel, cant_tabs);
            while_string += generator.save_comment("Fin while", cant_tabs, true);
            


            return while_string;
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
