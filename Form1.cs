using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Syntactic;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompiPascal
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Console.WriteLine("Archivo Nuevo");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Abrir Archivo");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Guardar Archivo");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void compilar_Click(object sender, EventArgs e)
        {
            Syntactic s = new Syntactic();
            C3DController.Instance.clearCode();
            
            s.analizer(areaanalizar.Text, "");
            
            if (ErrorController.Instance.containSemantycError())
            {
                consola.Text = ErrorController.Instance.getSemantycError(""); 

            } else
            {
                consola.Text = "";
                consola.Text = "#include <stdio.h>\nfloat Heap[100000]; //estructura heap\nfloat Stack[100000]; //estructura stack\n\n";
                consola.Text += "float SP; //puntero Stack pointer\nfloat HP; //puntero Heap pointer\n\n";
                consola.Text += C3DController.Instance.getTemps();

                consola.Text += "int main()\n{\n";

                consola.Text += C3DController.Instance.getCode();

                consola.Text += "\nreturn 0;\n}";
            }

        }
    }
}
