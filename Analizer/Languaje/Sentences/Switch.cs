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

        public override string Execute(Ambit ambit)
        {


            //Condicion de switch
            var conSwitch = condicion.Execute(ambit);
            var generator = C3D.C3DController.Instance;

            

            var switchAmbit = new Ambit(ambit, ambit.Ambit_name+"_Case", "Case", false);

            switchAmbit.Break = generator.newLabel();

            for (int i = 0; i < cases.Count; i++)
            {
                var @case = (Case)cases[i];

                var condCase = @case.getCaseCondition(ambit);

                //CONDICION
                var condicion = new Relational(new Literal(conSwitch.Value.ToString(), GetDataType(conSwitch.getDataType), row, column, cant_tabs), 
                    new Literal(condCase.Value.ToString(), GetDataType(condCase.getDataType), row, column, cant_tabs), "=", row, column, cant_tabs);

                var cond = condicion.Execute(ambit);

                generator.addLabel(condicion.TrueLabel, cant_tabs);
                var resultado = @case.Execute(switchAmbit);

                if (resultado == null)
                {
                    return null;
                }

                if (resultado.Equals("Break") || resultado.Equals("Exit"))
                {
                    return resultado;
                }
                generator.addLabel(condicion.FalseLabel, cant_tabs);
            }

            if (!else_case.IsNull)
            {
                var element = else_case.Execute(ambit);
                if (element == null)
                {
                    return null;
                }
            }
            generator.addLabel(switchAmbit.Break, cant_tabs);
            generator.replace_temp(switchAmbit.Break, "LTEMP");



            return "executed";
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
