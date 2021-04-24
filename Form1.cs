using CompiPascalC3D.Analizer.C3D;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Syntactic;
using CompiPascalC3D.Optimize.Syntactic;
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
            this.optimo.Enabled = this.tablasimbolos.Enabled = this.errores.Enabled = false;
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
            ErrorController.Instance.Clean();
            s.analizer(areaanalizar.Text, Application.StartupPath);


            if (ErrorController.Instance.containLexicalError())
            {
                var texto = ErrorController.Instance.getLexicalError("");
                consola.Text += texto;

            }
            else if (ErrorController.Instance.containSemantycError())
            {
                consola.Text += ErrorController.Instance.getSemantycError(""); 
            }
            else if (ErrorController.Instance.containSyntacticError())
            {
                consola.Text += ErrorController.Instance.getSintactycError("");
            }
            else
            {
                consola.Text = "";
                consola.Text = "#include <stdio.h>\nfloat Heap[100000]; //estructura heap\nfloat Stack[100000]; //estructura stack\n\n";
                consola.Text += "int SP = 0; //puntero Stack pointer\nint HP = 0; //puntero Heap pointer\n\n";
                consola.Text += C3DController.Instance.getTemps();
                consola.Text += C3DController.Instance.get_Genenal();
                this.tablasimbolos.Enabled = this.errores.Enabled = true;

            }

        }

        private void tablasimbolos_Click(object sender, EventArgs e)
        {
            ReporteController.Instance.set_path(Application.StartupPath);
            ReporteController.Instance.generate_report();
        }

        private void errores_Click(object sender, EventArgs e)
        {
            ReporteController.Instance.set_path(Application.StartupPath);
            ReporteController.Instance.generate_error_retort();

        }

        private void optimizar_Click(object sender, EventArgs e)
        {
            SyntacticOptimize s = new SyntacticOptimize();
            C3DController.Instance.clearCode();
            ErrorController.Instance.Clean();
            ReporteController.Instance.Clean();

            s.get_C3D_to_optimize(areaOptimizar.Text, Application.StartupPath);

            consolaOptimizar.Text = "";
            if (ErrorController.Instance.containLexicalError())
            {
                var texto = ErrorController.Instance.getLexicalError("");
                consolaOptimizar.Text += texto;

            }
            else if (ErrorController.Instance.containSemantycError())
            {
                consolaOptimizar.Text += ErrorController.Instance.getSemantycError("");
            }
            else if (ErrorController.Instance.containSyntacticError())
            {
                consolaOptimizar.Text += ErrorController.Instance.getSintactycError("");
            } else
            {
                this.optimo.Enabled = true;
                consolaOptimizar.Text = "todo bien";
            }

        }

        private void optimo_Click(object sender, EventArgs e)
        {
            ReporteController.Instance.set_path(Application.StartupPath);
            ReporteController.Instance.set_optimizacion_reporte();
        }
    }
}
