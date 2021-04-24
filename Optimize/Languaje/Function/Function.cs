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
        private ArrayList block_instructions;
        public Function(string id, ArrayList block_instructions)
        {
            this.id = id;
            this.block_instructions = block_instructions;
        }

        public ArrayList Block_instructions { get => block_instructions; set => block_instructions = value; }
        public string Id { get => id; set => id = value; }

        public override object Optimize()
        {

            var controller = ReporteController.Instance;

            //Eliminación de código muerto

            #region REGLA 1
            ArrayList newInstructions = new ArrayList();
            var texto_eliminado = "";
            for (int i = 0; i < block_instructions.Count; i++)
            {
                var instrucion = block_instructions[i];
                //REGLA 1
                if (instrucion is Goto)
                {
                    Goto @goto = (Goto)instrucion;
                    newInstructions.Add(@goto);

                    if (i+1 <= block_instructions.Count)
                    {
                        var instruccion_temp = block_instructions[i + 1];

                        if (instruccion_temp is SetLabel)
                        {
                            newInstructions.Add(instruccion_temp);
                            i = i + 1;
                        } else
                        {
                            for (int j = i + 1; j < block_instructions.Count; j++)
                            {
                                var instruccion2 = block_instructions[j];
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

            this.block_instructions = newInstructions;
            #endregion


            #region REGLA 2
            newInstructions = new ArrayList();
            for (int i = 0; i < block_instructions.Count; i++)
            {
                var instrucion = block_instructions[i];
                //REGLRA 2
                if (instrucion is IF)
                {
                    IF _if = (IF)instrucion;


                    if (_if.Left.IsNumber && _if.Right.IsNumber)
                    {
                        bool resultado = _if.evaluate_condition();


                        if (i + 1 <= block_instructions.Count)
                        {
                            var instrucion2 = block_instructions[i + 1];
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
                                newInstructions.Add(instrucion2);
                                i = i + 1;
                                break;
                            }

                        }

                    } else
                    {
                        if (i + 1 <= block_instructions.Count)
                        {
                            var instrucion2 = block_instructions[i + 1];

                            if (instrucion2 is Goto)
                            {

                                if (i + 2 <= block_instructions.Count)
                                {
                                    var instruccion3 = block_instructions[i + 2];

                                    if (instruccion3 is SetLabel)
                                    {
                                        SetLabel setLabel = (SetLabel)instruccion3;


                                        if (setLabel.Label.Name.Equals(_if.Label.Name))
                                        {
                                            Goto @goto = (Goto)instrucion2;

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
                                        newInstructions.Add(instruccion3);
                                        i = i + 2;
                                    }
                                }
                            }
                            else
                            {
                                newInstructions.Add(instrucion2);
                                i = i + 1;
                            }

                        }

                    }

                } else
                {
                    newInstructions.Add(instrucion);
                }
            }
            this.block_instructions = newInstructions;
            #endregion


            //Simplificación algebraica y reducción por fuerza
            newInstructions = new ArrayList();
            foreach (Instruction instruction in block_instructions)
            {
                if (instruction is Expresion)
                {
                    Expresion arithmetic = (Expresion)instruction;
                    arithmetic.set_ambit(Id);
                    var result = arithmetic.Optimize();
                    newInstructions.Add(result);
                    //newDictionary[i] = (Instruction)result;       
                    //i++;
                } else
                {
                    newInstructions.Add(instruction);
                }

            }
            this.block_instructions = newInstructions;

            return true;
        }

        public override string Code()
        {
            var cadena = "void " + id + "(){\n";

            foreach (Instruction instruction in block_instructions)
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

            /*foreach (Blocks blocks in block_instructions)
            {
                foreach (Instruction instruction in blocks.Instructions.Values)
                {
                    if (instruction is Expresion && (!((Expresion)instruction).IsEmpty))
                    {
                        cadena += "  " + instruction.Code();
                    }
                }
            }*/
            cadena += "\n}";
            return cadena;
        }
    }
}
