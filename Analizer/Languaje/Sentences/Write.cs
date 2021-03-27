using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class Write : Instruction
    {
        private LinkedList<Expresion> value;
        private bool isln;
        private int row;
        private int column;
        private int cant_tabs;
        public Write(LinkedList<Expresion> v, bool s, int r, int c, int ct) :
            base("Write")
        {
            this.value = v;
            this.isln = s;
            this.row = r;
            this.column = c;
            this.cant_tabs = ct;
        }

        public override string Execute(Ambit ambit)
        {
            //INSTANCIA GENERADOR C3D
            var generator = C3DController.Instance;

            //FOREACH DE LAS EXPRESIONES A HACER PRINT
            foreach (var el in value)
            {
                var element = el.Execute(ambit);

                if (element.getDataType == DataType.ERROR)
                {
                    return null;
                }
                switch (element.getDataType)
                {
                    case DataType.INTEGER:
                        generator.generate_print("i", element.getValue(), "(int)", cant_tabs);
                        break;
                    case DataType.STRING:

                        break;
                    case DataType.BOOLEAN:
                        break;
                    case DataType.REAL:
                        generator.generate_print("d", element.getValue(), "(float)", cant_tabs);
                        break;
                    case DataType.IDENTIFIER:
                        break;
                    default:
                        break;
                }



            }
            if (this.isln)
            {
                generator.generate_print("c", "10", "(int)", cant_tabs);
            }
            return "executed";
        }
    }
}
