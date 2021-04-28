using CompiPascalC3D.Optimize.Languaje.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Languaje.Block
{
    public class Blocks
    {
        private ArrayList instructions;
        private string inLabel;
        private int number;
        private string outLabel;

        public Blocks(int n)
        {
            this.inLabel = "";
            this.outLabel = "";
            this.number = n;
            this.instructions = new ArrayList();
        }

        public ArrayList Instructions { get => instructions; set => instructions = value; }
        public string InLabel { get => inLabel; set => inLabel = value; }
        public int Number { get => number; set => number = value; }
        public string OutLabel { get => outLabel; set => outLabel = value; }

        public void setInstruction(Instruction instruction)
        {
            this.instructions.Add(instruction);
        }
        public string get_instruction()
        {
            var text = "";
            foreach (Instruction inst in instructions)
            {
                var texto = inst.Code().Trim();
                texto = texto.Replace("\"", "");
                texto = texto.Replace("\"", "");
                texto = texto.Replace(";", "");
                texto = texto.Replace("<", "←");
                texto = texto.Replace(">", "→");

                text += texto + "<BR/>";
            }
            return text;
        }

        


    }
}
