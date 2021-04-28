using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Arithmetics;
using CompiPascalC3D.Optimize.Languaje.Block;
using CompiPascalC3D.Optimize.Languaje.If;
using CompiPascalC3D.Optimize.Languaje.Jumps;
using CompiPascalC3D.Optimize.Languaje.Labels;

namespace CompiPascalC3D.Optimize.Languaje.Function
{
    class Function : Instruction
    {
        private string id;
        private ArrayList instrucciones;
        private LinkedList<Blocks> block_instructions;
        private string tipo;
        public Function(string id, string tipo, ArrayList instrucciones)
            : base("Function")
        {
            this.id = id;
            this.tipo = tipo;
            this.instrucciones = instrucciones;
            this.block_instructions = new LinkedList<Blocks>();
        }

        public ArrayList Instrucciones { get => instrucciones; set => instrucciones = value; }
        public string Id { get => id; set => id = value; }
        public LinkedList<Blocks> Block_instructions { get => block_instructions; set => block_instructions = value; }

        public override object Optimize()
        {

            var controller = ReporteController.Instance;

            #region Eliminación de código muerto

            #region REGLA 1
            ArrayList newInstructions = new ArrayList();
            var texto_eliminado = "";
            for (int i = 0; i < instrucciones.Count; i++)
            {
                var instrucion = instrucciones[i];
                //REGLA 1
                if (instrucion is Goto)
                {
                    Goto @goto = (Goto)instrucion;
                    newInstructions.Add(@goto);

                    if (i+1 <= instrucciones.Count)
                    {
                        var instruccion_temp = instrucciones[i + 1];

                        if (instruccion_temp is SetLabel)
                        {
                            newInstructions.Add(instruccion_temp);
                            i = i + 1;
                        } else
                        {
                            for (int j = i + 1; j < instrucciones.Count; j++)
                            {
                                var instruccion2 = instrucciones[j];
                                newInstructions.Add(instruccion2);
                                if (instruccion2 is SetLabel)
                                {
                                    SetLabel setLabel = (SetLabel)instruccion2;

                                    if (setLabel.Label.Name.Equals(@goto.Label.Name))
                                    {
                                        texto_eliminado += "goto " + setLabel.Label.Name + "<br>";
                                        for (int L = i + 1; L < j; L++)
                                        {
                                            var instruccion_eliminada = newInstructions[i + 1];
                                            texto_eliminado += ((Instruction)instruccion_eliminada).Code() + "<br>";
                                            newInstructions.RemoveAt(i + 1);
                                        }
                                        texto_eliminado += setLabel.Label.Name + ":";
                                        controller.set_optimizacion("Regla 1", texto_eliminado, "goto " + setLabel.Label.Name + "<br>"
                                            + setLabel.Label.Name + ":", @goto.Row, @goto.Column, id);
                                        texto_eliminado = "";

                                        i = j;
                                        break;
                                    }
                                    else
                                    {
                                        i = j;
                                        break;
                                    }
                                }
                            }

                        }

                    }

                
                }
                else
                {
                    newInstructions.Add(instrucion);
                }
            }

            this.instrucciones = newInstructions;
            #endregion

            #region REGLA 2, 3, 4
            newInstructions = new ArrayList();
            for (int i = 0; i < instrucciones.Count; i++)
            {
                var instrucion = instrucciones[i];
                //REGLRA 2
                if (instrucion is IF)
                {
                    IF _if = (IF)instrucion;


                    if (_if.Left.IsNumber && _if.Right.IsNumber)
                    {
                        bool resultado = _if.evaluate_condition();


                        if (i + 1 <= instrucciones.Count)
                        {
                            var instrucion2 = instrucciones[i + 1];
                            //VERIFICA QUE LO QUE VENGA DESPUES DEL IF SEA GOTO
                            if (instrucion2 is Goto)
                            {
                                Goto @goto = (Goto)instrucion2;
                                texto_eliminado =
                                    _if.Code() + "<br>" + @goto.Code();

                                Goto if_otimo = new Goto();
                                var regla = "";
                                //REGLA 3
                                if (resultado)
                                {
                                    regla = "Regla 3";
                                    _if.Regla3 = true;
                                    if_otimo = (Goto)_if.Optimize();
                                }
                                // REGLA 4
                                else
                                {
                                    regla = "Regla 4";
                                    _if.Regla4 = true;
                                    _if.Label = @goto.Label;
                                    if_otimo = (Goto)_if.Optimize();

                                }

                                controller.set_optimizacion(regla, texto_eliminado, if_otimo.Code(), if_otimo.Row, if_otimo.Column, id);

                                newInstructions.Add(if_otimo);
                                i = i + 1;


                            }
                            else
                            {
                                newInstructions.Add(_if);
                            }

                        }
                        else
                        {
                            newInstructions.Add(_if);
                        }

                    } 
                    else
                    {
                        if (i + 1 <= instrucciones.Count)
                        {
                            var instrucion2 = instrucciones[i + 1];

                            if (instrucion2 is Goto)
                            {


                                if (i + 2 <= instrucciones.Count)
                                {
                                    var instruccion3 = instrucciones[i + 2];

                                    if (instruccion3 is SetLabel)
                                    {
                                        SetLabel setLabel = (SetLabel)instruccion3;


                                        if (setLabel.Label.Name.Equals(_if.Label.Name))
                                        {
                                            Goto @goto = (Goto)instrucion2;

                                            var encontrado = false;

                                            if (i - 1 > 0)
                                            {
                                                for (int k = i-1; k >= 0; k--)
                                                {
                                                    var instruccion_evaual = instrucciones[k];

                                                    if (instruccion_evaual is IF)
                                                    {
                                                        IF @if = (IF)instruccion_evaual;

                                                        if (@if.Label.Name.Equals(setLabel.Label.Name))
                                                        {
                                                            encontrado = true;
                                                            break;
                                                        }
                                                    }
                                                    else if (instruccion_evaual is Goto)
                                                    {
                                                        Goto @goto1 = (Goto)instruccion_evaual;
                                                        if (@goto1.Label.Name.Equals(setLabel.Label.Name))
                                                        {
                                                            encontrado = true;
                                                            break;
                                                        }
                                                    }

                                                }
                                            }
                                            
                                            if (!encontrado)
                                            {
                                                texto_eliminado =
                                                    _if.Code() + "<br>" + @goto.Code() + "<br>" + setLabel.Code();

                                                _if.Regla2 = true;
                                                _if.Label = @goto.Label;
                                                IF if_otimo = (IF)_if.Optimize();

                                                controller.set_optimizacion("Regla 2", texto_eliminado, if_otimo.Code(), if_otimo.Row, if_otimo.Column, id);


                                                newInstructions.Add(if_otimo);
                                                i = i + 2;
                                            }
                                            else
                                            {
                                                newInstructions.Add(_if);
                                            }
                                        }
                                        else
                                        {
                                            newInstructions.Add(_if);
                                        }
                                    }
                                    else
                                    {
                                        newInstructions.Add(_if);
                                    }
                                }
                                else
                                {
                                    newInstructions.Add(_if);
                                }
                            }
                            else
                            {
                                newInstructions.Add(_if);
                            }
                        }
                    }

                } else
                {
                    newInstructions.Add(instrucion);
                }
            }
            this.instrucciones = newInstructions;
            #endregion
            #endregion
            
            #region Eliminación de instrucciones redundantes de carga y almacenamiento
            newInstructions = new ArrayList();
            for (int i = 0; i < instrucciones.Count; i++)
            {
                var instruccion = instrucciones[i];
                if (newInstructions.Count == 0)
                {
                    newInstructions.Add(instruccion);
                }
                else
                {
                    //REGLA 5;
                    if (instruccion is Expresion)
                    {
                        //EXPRESION QUE VOY A BUSCAR
                        Expresion expresion = (Expresion)instruccion;
                        var encontrado = false;
                        if (expresion.Right == null && !expresion.Left.IsNumber)
                        {

                            for (int j = i-1; j > 0; j--)
                            {
                                var instruccion_temporal = instrucciones[j];
                                if (instruccion_temporal is Expresion)
                                {
                                    Expresion expresion_temporal = (Expresion)instruccion_temporal;


                                    if (expresion_temporal.Temp.Equals(expresion.Left.Value))
                                    {
                                        if (expresion_temporal.Right == null && !expresion_temporal.Left.IsNumber)
                                        {
                                            if (expresion.Left.Value.Equals(expresion_temporal.Temp) && expresion.Temp.Equals(expresion_temporal.Left.Value))
                                            {
                                                controller.set_optimizacion("Regla 5", expresion.Code(), "", expresion.Row, expresion.Column, id);
                                                encontrado = true;
                                                break;
                                            }
                                        } else
                                        {
                                            encontrado = false;
                                            break;
                                        }
                                    }

                                }
                                else if (instruccion_temporal is SetLabel)
                                {
                                    break;
                                }
                            }

                            if (!encontrado)
                            {
                                newInstructions.Add(expresion);
                            }

                        }
                        else
                        {
                            newInstructions.Add(instruccion);
                        }
                    }
                    else
                    {
                        newInstructions.Add(instruccion);
                    }
                }


            }
            this.instrucciones = newInstructions;
            #endregion


            #region Simplificación algebraica y reducción por fuerza
            newInstructions = new ArrayList();
            foreach (Instruction instruction in instrucciones)
            {
                if (instruction is Expresion)
                {
                    Expresion arithmetic = (Expresion)instruction;
                    arithmetic.set_ambit(Id);
                    var result = arithmetic.Optimize();
                    newInstructions.Add(result);
                    //newDictionary[i] = (Instruction)result;       
                    //i++;
                }
                else
                {
                    newInstructions.Add(instruction);
                }

            }
            this.instrucciones = newInstructions;
            #endregion


            int number_block = 1;
            Blocks blocks = new Blocks(number_block);
            foreach (Instruction inst in instrucciones)
            {

                if (inst.Name.Equals("If") || inst.Name.Equals("Goto"))
                {
                    blocks.setInstruction(inst);
                    blocks.OutLabel = ( (inst is IF) ? ((IF)inst).Label.Name : ((Goto)inst).Label.Name) ;
                    block_instructions.AddLast(blocks);
                    number_block++;
                    blocks = new Blocks(number_block);
                }
                
                else if (inst.Name.Equals("SetLabel"))
                {
                    if (blocks.Instructions.Count > 0)
                    {
                        block_instructions.AddLast(blocks);
                        number_block++;
                        blocks = new Blocks(number_block);
                    }
                    blocks.setInstruction(inst);
                    blocks.InLabel = ((SetLabel)inst).Label.Name;
                } else
                {
                    blocks.setInstruction(inst);
                }
            }
            block_instructions.AddLast(blocks);



            return this;
        }

        public override string Code()
        {
            var cadena = tipo +" " + id + "(){\n";

            foreach (Instruction instruction in instrucciones)
            {
                if (instruction is Expresion)
                {
                    var exp = (Expresion)instruction;
                    if (!exp.IsEmpty)
                    {
                        cadena += "  " + instruction.Code();
                    }
                }
                else
                {
                    cadena += "  " + instruction.Code();
                }
            }

            cadena += "\n}\n\n";
            return cadena;
        }
    }
}
