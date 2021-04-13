using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Expressions;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Sentences
{
    class For : Instruction
    {

        private string initId;
        private Expresion inicializacion;
        private Expresion actualizacion;
        private LinkedList<Instruction> sentences;
        private string direccion;
        private int row;
        private int column;
        private int cant_tabs;

        public For(string initId, Expresion inicializacion, Expresion actualizacion, 
            LinkedList<Instruction> sentence, string dir, int ro, int col, int ct)
            : base("For")
        {
            this.initId = initId;
            this.inicializacion = inicializacion;
            this.actualizacion = actualizacion;
            this.sentences = sentence;
            this.direccion = dir;
            this.row = ro;
            this.column = col;
            this.cant_tabs = ct;
        }

        public override object Execute(Ambit ambit)
        {
            var forAmbit = new Ambit(ambit, ambit.Ambit_name, "For", false, ambit.IsFunction);



            //SE HACE LA ASIGNACION INICIAL
            Assignation assignation = new Assignation(initId, inicializacion, row, column, cant_tabs);

            Identifier identifier = ambit.getVariable(initId);

            var for_str = "";
             //VERIFICA QUE EL IDENTIFICADOR EXISTA  
            if (!identifier.IsNull)
            {
                //VERIFICA QUE LA VARIABLE NO HAYA SIDO ASIGNADA EN LA DECLARACION
                if (!identifier.IsAssiged)
                {

                    var generator = C3D.C3DController.Instance;

                    var simb2 = (direccion.ToLower().Equals("to")) ? "+" : "-";
                    var simb = (direccion.ToLower().Equals("to")) ? ">" : "<";


                    for_str += generator.save_comment("Inicia For", cant_tabs, false);

                    //ETIQUETA DE RECURRENCIA
                    //var recurrence_label = generator.newLabel();


                    //SE ASIGNA EL NUEVO VALOR A LA VARIABLE
                    for_str += assignation.Execute(forAmbit);


                    //IMPRIMRE TAG RECURRENCIA

                    //for_str += generator.save_comment(recurrence_label+ ": //Etiqueta Recurrencia", cant_tabs, false);


                    //SE CREA LA CONDICION 
                    //Relational cond_temp = new Relational((new Access(initId, row, column, cant_tabs)), actualizacion, simb, row, column, cant_tabs);

                    var trueLabel = generator.newLabel();

                    var continueLabel = generator.newLabel();

                    var falseLabel = generator.newLabel();

                    var actual = actualizacion.Execute(forAmbit);

                    for_str += actual.Texto_anterior;
                    for_str += generator.addLabel(trueLabel, cant_tabs);

                    var acceso = (new Access(initId, row, column, cant_tabs)).Execute(forAmbit);
                    for_str += acceso.Texto_anterior;

                    for_str += generator.add_If(acceso.Value.ToString(), actual.Value, simb, falseLabel, cant_tabs);

                    forAmbit.Continue = continueLabel;
                    forAmbit.Break = falseLabel;

                    //SE IMPRIMEN LAS INSTRUCCIONES

                    foreach (Instruction item in sentences)
                    {
                        var element = item.Execute(forAmbit);
                        if (element == null)
                        {
                            return null;
                        }
                        for_str += element.ToString();
                    }



                    for_str += generator.addLabel(continueLabel, cant_tabs);

                    for_str += generator.addExpression(acceso.Value.ToString(), 
                        acceso.Value.ToString(), "1", simb2, cant_tabs);


                    Assignation assignation1 = new Assignation(initId, 
                        new Literal(acceso.Value.ToString(), 1, row, column, cant_tabs), row, column, cant_tabs);


                    for_str += (assignation1.Execute(forAmbit));


                    //goto a etiqueta verdadera
                    for_str += generator.add_Goto(trueLabel, cant_tabs);

                    //etiqueta falsa
                    for_str += generator.addLabel(falseLabel, cant_tabs);
                }
                else
                {
                    set_error("Variable de contador ilegal: La variable '" + initId + "' no debe estar inicializada al momento de su declaracion",row, column);
                    return null;
                }

            }
            else
            {
                set_error("La variable '" + initId + "' no esta declarada.", row, column);
                return null;
            }



            return for_str;
        }


        public void set_error(string texto, int row, int column)
        {
            ErrorController.Instance.SemantycErrors(texto, row, column);
            ConsolaController.Instance.Add(texto + " - row: " + row + "- Col: " + column + "\n");
        }
    }
}
