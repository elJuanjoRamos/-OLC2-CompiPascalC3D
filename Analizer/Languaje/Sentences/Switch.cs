using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Switch : Instruction
    {
        private Expresion condicion;
        private ArrayList cases;
        private Case else_case;
        private int row;
        private int column;
        private int cant_tabs;

        public Switch(Expresion condicion, ArrayList cases, Case else_case, int ro, int col, int ct) :
            base("Case")
        {
            this.condicion = condicion;
            this.cases = cases;
            this.else_case = else_case;
            this.row = ro;
            this.column = col;
            this.cant_tabs = ct;
        }

        public override object Execute(Ambit ambit)
        {
            var generator = C3D.C3DController.Instance;

            var switch_string = generator.save_comment("Inicia CASE", cant_tabs, false);

            //Condicion de switch
            var conSwitch = condicion.Execute(ambit);
            
            switch_string += conSwitch.Texto_anterior;


            

            var switchAmbit = new Ambit(ambit, ambit.Ambit_name+"_Case", "Case", false, ambit.IsFunction);

            switchAmbit.Break = generator.newLabel();

            for (int i = 0; i < cases.Count; i++)
            {
                var @case = (Case)cases[i];

                var condCase = @case.getCaseCondition(ambit);

                //CONDICION
                var condicion = new Relational(new Literal(conSwitch.Value.ToString(), GetDataType(conSwitch.getDataType), row, column, cant_tabs), 
                    new Literal(condCase.Value.ToString(), GetDataType(condCase.getDataType), row, column, cant_tabs), "=", row, column, cant_tabs);

                var cond = condicion.Execute(ambit);

                switch_string += cond.Texto_anterior;

                switch_string += generator.addLabel(condicion.TrueLabel, cant_tabs);
                var resultado = @case.Execute(switchAmbit);

                if (resultado == null)
                {
                    return null;
                }
                

                switch_string += resultado;
                switch_string += generator.addLabel(condicion.FalseLabel, cant_tabs);
            }

            if (!else_case.IsNull)
            {
                var element = else_case.Execute(ambit);
                if (element == null)
                {
                    return null;
                }
                switch_string += element;
            }
            switch_string += generator.addLabel(switchAmbit.Break, cant_tabs);
            switch_string = generator.replace_temp(switchAmbit.Break, "LTEMP", switch_string);

            switch_string += generator.save_comment("Fin CASE", cant_tabs, true);
            return switch_string;
        }


        public int GetDataType(DataType data)
        {



            if (data == DataType.INTEGER)
            {
                return 1;
            }
            if (data == DataType.STRING)
            {
                return 2;
            }
            if (data == DataType.BOOLEAN)
            {
                return 3;
            }
            if (data == DataType.IDENTIFIER)
            {
                return 7;
            }
            return 0;
        }
     }
}
