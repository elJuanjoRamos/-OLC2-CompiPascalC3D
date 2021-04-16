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

        public override object Execute(Ambit ambit)
        {
            //INSTANCIA GENERADOR C3D
            var generator = C3DController.Instance;
            var label_print = "";
            var write_Str = "";
            write_Str += generator.save_comment("Inicia Print PRINTTEMP", cant_tabs, false);

            //FOREACH DE LAS EXPRESIONES A HACER PRINT
            foreach (Expresion el in value)
            {


                if (el is Literal)
                {
                    if (((Literal)el).Type == 3)
                    {
                        var res = ((Literal)el).Value.ToString();
                        write_Str += generator.print_boolean(cant_tabs, res);
                        continue;
                    }
                    if (((Literal)el).Type == 2)
                    {
                        var res = ((Literal)el).Value.ToString();
                        if (res.ToLower().Equals("true") || res.ToLower().Equals("false"))
                        {
                            write_Str += generator.print_boolean(cant_tabs, res);
                            continue;
                        }
                    }

                }
                if (el is Access)
                {
                    label_print = ":" + ((Access)el).Id;
                }

                var element = el.Execute(ambit);
                if (element.IsTemporal)
                {
                    //generator.free_temps(element.Value.ToString());
                }

                write_Str += element.Texto_anterior;

                if (element.getDataType == DataType.ERROR)
                {
                    return null;
                }
                switch (element.getDataType)
                {
                    case DataType.INTEGER:
                        write_Str += generator.generate_print("i", element.getValue(), "(int)", cant_tabs);
                        break;
                    case DataType.STRING:
                        
                        generator.Native_str = true;

                        var temp_stack = element.Value.ToString();
                        write_Str += generator.addExpression("T1", temp_stack, "", "", cant_tabs);

                        write_Str += generator.save_code("native_print_str();", cant_tabs);

                        /*var label_temp = generator.newLabel();
                        generator.addLabel(label_temp, cant_tabs);
                        var temp_heap = generator.newTemporal();

                        generator.get_Heap(temp_heap, temp_stack, cant_tabs);

                        var true_label = generator.newLabel();

                        generator.add_If(temp_heap, "-1", "==", true_label, cant_tabs);

                        generator.generate_print("c", temp_heap, "(int)", cant_tabs);

                        generator.addExpression(temp_stack, temp_stack, "1", "+", cant_tabs);

                        generator.add_Goto(label_temp, cant_tabs);
                        generator.addLabel(true_label, cant_tabs);*/

                        break;
                    case DataType.BOOLEAN:

                        write_Str += generator.addLabel(element.TrueLabel, cant_tabs);
                        write_Str += generator.addLabel(element.FalseLabel, cant_tabs);
                        write_Str += generator.print_boolean(cant_tabs, element.Valor_original);                        
                        break;
                    case DataType.REAL:
                        write_Str += generator.generate_print("f", element.getValue(), "(float)", cant_tabs);
                        break;
                    case DataType.IDENTIFIER:
                        break;
                    default:
                        break;
                }
            }
            if (this.isln)
            {
                write_Str += generator.generate_print("c", "10", "(int)", cant_tabs);
            }
            write_Str += generator.save_comment("Fin Print PRINTTEMP", cant_tabs, true);
            write_Str = generator.replace_temp(label_print, "PRINTTEMP", write_Str);
            return write_Str;
        }
    }
}
