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
            linenumber.Font = areaanalizar.Font;
            linenumber1.Font = areaOptimizar.Font;
            areaanalizar.Select();
            areaOptimizar.Select();
            AddLineNumbers(areaanalizar, linenumber);
            AddLineNumbers(areaOptimizar, linenumber1);

            this.optimo.Enabled = this.tablasimbolos.Enabled = this.graph.Enabled = this.errores.Enabled = false;
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
            this.errores.Enabled = this.tablasimbolos.Enabled = false;

            consola.Text = "";
            if (ErrorController.Instance.containLexicalError())
            {
                consola.Text += ErrorController.Instance.getLexicalError();
                this.errores.Enabled = true;
            }
            else if (ErrorController.Instance.containSemantycError())
            {
                consola.Text += ErrorController.Instance.getSemantycError();
                this.errores.Enabled = true;
            }
            else if (ErrorController.Instance.containSyntacticError())
            {
                consola.Text += ErrorController.Instance.getSintactycError();
                this.errores.Enabled = true;
            }
            else
            {
                consola.Text = "#include <stdio.h>\nfloat Heap[100000]; //estructura heap\nfloat Stack[100000]; //estructura stack\n\n";
                consola.Text += "int SP = 0; //puntero Stack pointer\nint HP = 0; //puntero Heap pointer\n\n";
                consola.Text += C3DController.Instance.getTemps();
                consola.Text += C3DController.Instance.get_Genenal();
                this.tablasimbolos.Enabled = true;
            }

        }

        private void tablasimbolos_Click(object sender, EventArgs e)
        {
            ReporteController.Instance.set_path(Application.StartupPath);
            var resultado = ReporteController.Instance.generate_report();
            showPopUp(resultado, "tabla de simbolos");   
        }

        private void errores_Click(object sender, EventArgs e)
        {
            ReporteController.Instance.set_path(Application.StartupPath);
            var resultado = ReporteController.Instance.generate_error_retort();
            showPopUp(resultado, "errores");

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
                consolaOptimizar.Text += ErrorController.Instance.getLexicalError();
            }
            else if (ErrorController.Instance.containSemantycError())
            {
                consolaOptimizar.Text += ErrorController.Instance.getSemantycError();
            }
            else if (ErrorController.Instance.containSyntacticError())
            {
                consolaOptimizar.Text += ErrorController.Instance.getSintactycError();
            } else
            {
                this.optimo.Enabled = true;
                this.graph.Enabled = true;
                this.errores.Enabled = true;
                consolaOptimizar.Text = s.get_texto();
            }

        }

        private void optimo_Click(object sender, EventArgs e)
        {
            ReporteController.Instance.set_path(Application.StartupPath);
            var resultado = ReporteController.Instance.set_optimizacion_reporte();
            showPopUp(resultado, "optimizacion");


        }

        private void graph_Click(object sender, EventArgs e)
        {
            ReporteController.Instance.set_path(Application.StartupPath);
            var resultado = ReporteController.Instance.graph_blocks();
            showPopUp(resultado, "bloques");
        }


        private void showPopUp(bool resultado, string report)
        {
            if (resultado)
            {
                MessageBox.Show("Reporte de " + report + " generado en la carpeta /bin");
            }
            else
            {
                MessageBox.Show("Error al generar el reporte " + report);
            }
        }




        public void AddLineNumbers(RichTextBox area, RichTextBox lineNumbers)
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = area.GetCharIndexFromPosition(pt);
            int First_Line = area.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = area.GetCharIndexFromPosition(pt);
            int Last_Line = area.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            lineNumbers.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            lineNumbers.Text = "";
            lineNumbers.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                lineNumbers.Text += i + 1 + "\n";
            }
        }
        #region ANALIZAR
        private void areaanalizar_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = areaanalizar.GetPositionFromCharIndex(areaanalizar.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers(areaanalizar, linenumber);
            }
        }
        private void areaanalizar_VScroll(object sender, EventArgs e)
        {
            linenumber.Text = "";
            AddLineNumbers(areaanalizar, linenumber);
            linenumber.Invalidate();
        }
        private void areaanalizar_TextChanged(object sender, EventArgs e)
        {
            if (areaanalizar.Text == "")
            {
                AddLineNumbers(areaanalizar, linenumber);
            }
        }

        private void areaanalizar_FontChanged(object sender, EventArgs e)
        {
            linenumber.Font = areaanalizar.Font;
            areaanalizar.Select();
            AddLineNumbers(areaanalizar, linenumber);
        }
        private void linenumber_MouseDown(object sender, MouseEventArgs e)
        {
            linenumber.Select();
            linenumber.DeselectAll();
        }


        #endregion

        #region OPTIMIZAR
        private void areaOptimizar_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = areaOptimizar.GetPositionFromCharIndex(areaOptimizar.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers(areaOptimizar, linenumber1);
            }
        }
        private void areaOptimizar_VScroll(object sender, EventArgs e)
        {
            linenumber1.Text = "";
            AddLineNumbers(areaOptimizar, linenumber1);
            linenumber1.Invalidate();
        }
        private void areaOptimizar_TextChanged(object sender, EventArgs e)
        {
            if (areaOptimizar.Text == "")
            {
                AddLineNumbers(areaOptimizar, linenumber1);
            }
        }
        private void areaOptimizar_FontChanged(object sender, EventArgs e)
        {
            linenumber1.Font = areaOptimizar.Font;
            areaOptimizar.Select();
            AddLineNumbers(areaOptimizar, linenumber1);
        }
        private void linenumber1_MouseDown(object sender, MouseEventArgs e)
        {
            linenumber1.Select();
            linenumber1.DeselectAll();
        }
        #endregion

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = areaanalizar.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)areaanalizar.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)areaanalizar.Font.Size;
            }
            else
            {
                w = 50 + (int)areaanalizar.Font.Size;
            }

            return w;
        }

    }
}
