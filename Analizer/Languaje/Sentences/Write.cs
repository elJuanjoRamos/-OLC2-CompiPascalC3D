using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Expressions;
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
        public Write(LinkedList<Expresion> v, bool s, int r, int c, int canttabs) :
            base("Write")
        {
            this.value = v;
            this.isln = s;
            this.row = r;
            this.column = c;
            this.cant_tabs = canttabs;
        }

        public override string Execute(Ambit ambit)
        {
            //INSTANCIA GENERADOR C3D
            var generator = C3DController.Instance;

            //FOREACH DE LAS EXPRESIONES A HACER PRINT
            foreach (Expresion el in value)
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
                        var id = "";
                        if (el is Access)
                        {
                            id = ((Access)el).Id;
                        }

                        generator.save_comment("Inicia Print: " + id, cant_tabs);


                        var temp_stack = element.Value.ToString();

                        var label_temp = generator.newLabel();
                        generator.addLabel(label_temp, cant_tabs);
                        var temp_heap = generator.newTemporal();

                        generator.get_Heap(temp_heap, temp_stack, cant_tabs);

                        var true_label = generator.newLabel();

                        generator.add_If(temp_heap, "-1", "==", true_label, cant_tabs);

                        generator.generate_print("c", temp_heap, "(int)", cant_tabs);

                        generator.addExpression(temp_stack, temp_stack, "1", "+", cant_tabs);

                        generator.add_Goto(label_temp, cant_tabs);
                        generator.addLabel(true_label, cant_tabs);

                        generator.save_comment("Fin Print: " + id, cant_tabs);

                        break;
                    case DataType.BOOLEAN:
                        if (element.Value.ToString().Equals("false"))
                        {
                            generator.print_boolean(cant_tabs, false);
                        } else
                        {
                            generator.print_boolean(cant_tabs, true);
                        }
                        
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
