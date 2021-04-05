using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Case : Instruction
    {
        private Expresion condition;
        private Sentence sentence;
        public int row;
        public int column;
        private bool isNull;
        private bool isElse;
        private int cant_tabs;

        public bool IsNull { get => isNull; set => isNull = value; }
        public bool IsElse { get => isElse; set => isElse = value; }

        public Case(Expresion condition, Sentence code, int ro, int col, int cant_tabs)
           : base("Case")
        {
            this.condition = condition;
            this.sentence = code;
            this.isNull = false;
            this.isElse = false;
            this.row = ro;
            this.column = col;
            this.cant_tabs = cant_tabs;
        }
        public Case(int row, int cl)
            : base("Case")
        {
            this.isElse = false;
            this.isNull = true;
            this.row = row;
            this.column = cl;
            this.cant_tabs = 0;
        }
        //ESTE ES EL ELSE-CASE
        public Case(Sentence code, int row, int col, int cant_t)
            : base("Case")
        {
            this.sentence = code;
            this.isNull = false;
            this.isElse = true;
            this.row = row;
            this.column = col;
            this.cant_tabs = cant_t;
        }
        public override string Execute(Ambit ambit)
        {

            var generator = C3D.C3DController.Instance;

            //VERIFICA QUE LAS SENTNECIAS NO VENGAN VACIAS
            if (!sentence.IsNull)
            {


                var element = sentence.Execute(ambit);

                if (element != null)
                {
                    if (element.Equals("Break"))
                    {

                    } else if (element.Equals("Exit"))
                    {
                        return element;
                    }
                    
                }
                else
                {
                    return null;
                }
            }
            generator.add_Goto("LTEMP", cant_tabs);
            return "executed";
        }

        public Returned getCaseCondition(Ambit ambit)
        {
            return this.condition.Execute(ambit);
        }
    }
}
