using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Repeat : Instruction
    {
        private Expresion condition;
        private Sentence sentences;
        private int row;
        private int column;
        private int cant_tabs;
        public Repeat(Expresion condition, Sentence sentences, int ro, int col, int cant_tabs)
            : base("Repeat")
        {
            this.condition = condition;
            this.sentences = sentences;
            this.row = ro;
            this.column = col;
            this.cant_tabs = cant_tabs;
        }
        public override object Execute(Ambit ambit)
        {
            var repeat_String = "";

            //INSTANCIA DEL GENERADOR C3D
            var generator = C3D.C3DController.Instance;

            //AMBITO DEL REPEAT
            var repeatAmbit = new Ambit(ambit, ambit.Ambit_name+"_Repeat", "Repeat", false, ambit.IsFunction);
            repeat_String += generator.save_comment("Inicia Repeat", cant_tabs, false);

            //SETEO Continue y break por defecto
            condition.TrueLabel = generator.newLabel();
            repeatAmbit.Break = "LBREAK";
            repeatAmbit.Continue = condition.FalseLabel = generator.newLabel();

            //IMPRIMIR ETIQUETA RECURRENCIA
            repeat_String += generator.addLabel("LTEMP", cant_tabs);


            //INSTRUCCIONES
            var result = sentences.Execute(repeatAmbit);
            
            if (result == null)
            {
                return null;
            }
            
            repeat_String += result.ToString();
            if (repeatAmbit.Change_continue)
            {
                repeat_String += generator.addLabel(repeatAmbit.Continue, cant_tabs);
            }

            //CONDICION
            var condicion = condition.Execute(repeatAmbit);
            repeat_String += condicion.Texto_anterior;

            repeatAmbit.Break = condicion.TrueLabel;
            repeatAmbit.Continue = condicion.FalseLabel;

            repeat_String = generator.replace_temp(condicion.TrueLabel, "LBREAK", repeat_String);
            repeat_String = generator.replace_temp(repeatAmbit.Continue, "LTEMP", repeat_String);
            //VERIFICA QUE SEA BOOL
            if (condicion.getDataType != DataType.BOOLEAN)
            {
                set_error("La condicion del repeat no es booleana", row, column);
                return null;
            }

            //IMPRIMIR ETIQUETA VERDADERA
            repeat_String += generator.addLabel(condicion.TrueLabel, cant_tabs);
            repeat_String += generator.save_comment("Fin Repeat", cant_tabs, true);



            return repeat_String;
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
