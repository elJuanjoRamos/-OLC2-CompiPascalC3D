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
        public override string Execute(Ambit ambit)
        {
            //INSTANCIA DEL GENERADOR C3D
            var generator = C3D.C3DController.Instance;

            //AMBITO DEL REPEAT
            var repeatAmbit = new Ambit(ambit, ambit.Ambit_name, "Repeat", false);
            generator.save_comment("Inicia Repeat", cant_tabs);

            //SETEO Continue y break por defecto
            repeatAmbit.Continue = condition.TrueLabel = generator.newLabel();
            repeatAmbit.Break = condition.FalseLabel = generator.newLabel();

            //IMPRIMIR ETIQUETA RECURRENCIA
            generator.addLabel(condition.FalseLabel, cant_tabs);


            //INSTRUCCIONES
            var result = sentences.Execute(repeatAmbit);
            if (result == null)
            {
                return null;
            }

            //CONDICION
            var condicion = condition.Execute(repeatAmbit);
            //VERIFICA QUE SEA BOOL
            if (condicion.getDataType != DataType.BOOLEAN)
            {
                set_error("La condicion del repeat no es booleana", row, column);
                return null;
            }
            //GENERAR EL goto
            generator.addLabel(condicion.FalseLabel, cant_tabs);
            generator.add_Goto(repeatAmbit.Break, cant_tabs + 1);

            //IMPRIMIR ETIQUETA VERDADERA
            generator.addLabel(condicion.TrueLabel, cant_tabs);
            generator.save_comment("Fin Repeat", cant_tabs);



            return "executed";
        }

        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - Row: " + row + "- Col: " + column + "\n");
        }
    }
}
