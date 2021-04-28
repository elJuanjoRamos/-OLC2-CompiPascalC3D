namespace CompiPascal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.areaanalizar = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.errores = new System.Windows.Forms.Button();
            this.tablasimbolos = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.compilar = new System.Windows.Forms.Button();
            this.consola = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.graph = new System.Windows.Forms.Button();
            this.consolaOptimizar = new System.Windows.Forms.RichTextBox();
            this.areaOptimizar = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.optimo = new System.Windows.Forms.Button();
            this.optimizar = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 28);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 24);
            this.toolStripMenuItem1.Text = "Nuevo";
            // 
            // areaanalizar
            // 
            this.areaanalizar.Location = new System.Drawing.Point(46, 107);
            this.areaanalizar.Name = "areaanalizar";
            this.areaanalizar.Size = new System.Drawing.Size(664, 626);
            this.areaanalizar.TabIndex = 2;
            this.areaanalizar.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(29, 72);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1606, 863);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.errores);
            this.tabPage1.Controls.Add(this.tablasimbolos);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.compilar);
            this.tabPage1.Controls.Add(this.consola);
            this.tabPage1.Controls.Add(this.areaanalizar);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1598, 830);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "C3D Generator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // errores
            // 
            this.errores.BackColor = System.Drawing.Color.White;
            this.errores.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errores.Image = ((System.Drawing.Image)(resources.GetObject("errores.Image")));
            this.errores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.errores.Location = new System.Drawing.Point(725, 393);
            this.errores.Name = "errores";
            this.errores.Size = new System.Drawing.Size(156, 50);
            this.errores.TabIndex = 11;
            this.errores.Text = "      Reporte Error";
            this.errores.UseVisualStyleBackColor = false;
            this.errores.Click += new System.EventHandler(this.errores_Click);
            // 
            // tablasimbolos
            // 
            this.tablasimbolos.BackColor = System.Drawing.Color.White;
            this.tablasimbolos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tablasimbolos.Image = ((System.Drawing.Image)(resources.GetObject("tablasimbolos.Image")));
            this.tablasimbolos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tablasimbolos.Location = new System.Drawing.Point(725, 335);
            this.tablasimbolos.Name = "tablasimbolos";
            this.tablasimbolos.Size = new System.Drawing.Size(156, 52);
            this.tablasimbolos.TabIndex = 10;
            this.tablasimbolos.Text = "      Tabla Simbolos";
            this.tablasimbolos.UseVisualStyleBackColor = false;
            this.tablasimbolos.Click += new System.EventHandler(this.tablasimbolos_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(897, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 40);
            this.label3.TabIndex = 9;
            this.label3.Text = "Salida";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(897, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "Consola de traduccion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(40, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 40);
            this.label2.TabIndex = 7;
            this.label2.Text = "Entrada";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(46, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Editor de Texto";
            // 
            // compilar
            // 
            this.compilar.BackColor = System.Drawing.Color.White;
            this.compilar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.compilar.Image = ((System.Drawing.Image)(resources.GetObject("compilar.Image")));
            this.compilar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.compilar.Location = new System.Drawing.Point(725, 270);
            this.compilar.Name = "compilar";
            this.compilar.Size = new System.Drawing.Size(156, 50);
            this.compilar.TabIndex = 4;
            this.compilar.Text = "Compilar";
            this.compilar.UseVisualStyleBackColor = false;
            this.compilar.Click += new System.EventHandler(this.compilar_Click);
            // 
            // consola
            // 
            this.consola.BackColor = System.Drawing.SystemColors.MenuBar;
            this.consola.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.consola.Location = new System.Drawing.Point(897, 107);
            this.consola.Name = "consola";
            this.consola.Size = new System.Drawing.Size(650, 626);
            this.consola.TabIndex = 3;
            this.consola.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.graph);
            this.tabPage2.Controls.Add(this.consolaOptimizar);
            this.tabPage2.Controls.Add(this.areaOptimizar);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.optimo);
            this.tabPage2.Controls.Add(this.optimizar);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1598, 830);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Optimizer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // graph
            // 
            this.graph.BackColor = System.Drawing.Color.White;
            this.graph.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graph.Image = ((System.Drawing.Image)(resources.GetObject("graph.Image")));
            this.graph.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.graph.Location = new System.Drawing.Point(721, 425);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(156, 50);
            this.graph.TabIndex = 21;
            this.graph.Text = "Graph";
            this.graph.UseVisualStyleBackColor = false;
            this.graph.Click += new System.EventHandler(this.graph_Click);
            // 
            // consolaOptimizar
            // 
            this.consolaOptimizar.BackColor = System.Drawing.SystemColors.MenuBar;
            this.consolaOptimizar.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.consolaOptimizar.Location = new System.Drawing.Point(900, 102);
            this.consolaOptimizar.Name = "consolaOptimizar";
            this.consolaOptimizar.Size = new System.Drawing.Size(650, 626);
            this.consolaOptimizar.TabIndex = 20;
            this.consolaOptimizar.Text = "";
            // 
            // areaOptimizar
            // 
            this.areaOptimizar.Location = new System.Drawing.Point(49, 102);
            this.areaOptimizar.Name = "areaOptimizar";
            this.areaOptimizar.Size = new System.Drawing.Size(664, 626);
            this.areaOptimizar.TabIndex = 19;
            this.areaOptimizar.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(900, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 40);
            this.label5.TabIndex = 18;
            this.label5.Text = "Salida";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(900, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 21);
            this.label6.TabIndex = 17;
            this.label6.Text = "Consola de traduccion";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(46, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 40);
            this.label7.TabIndex = 16;
            this.label7.Text = "Entrada";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(49, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 21);
            this.label8.TabIndex = 15;
            this.label8.Text = "Editor de Texto";
            // 
            // optimo
            // 
            this.optimo.BackColor = System.Drawing.Color.White;
            this.optimo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.optimo.Image = ((System.Drawing.Image)(resources.GetObject("optimo.Image")));
            this.optimo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.optimo.Location = new System.Drawing.Point(721, 369);
            this.optimo.Name = "optimo";
            this.optimo.Size = new System.Drawing.Size(156, 50);
            this.optimo.TabIndex = 14;
            this.optimo.Text = "     Optimizacion";
            this.optimo.UseVisualStyleBackColor = false;
            this.optimo.Click += new System.EventHandler(this.optimo_Click);
            // 
            // optimizar
            // 
            this.optimizar.BackColor = System.Drawing.Color.White;
            this.optimizar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.optimizar.Image = ((System.Drawing.Image)(resources.GetObject("optimizar.Image")));
            this.optimizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.optimizar.Location = new System.Drawing.Point(721, 299);
            this.optimizar.Name = "optimizar";
            this.optimizar.Size = new System.Drawing.Size(156, 50);
            this.optimizar.TabIndex = 13;
            this.optimizar.Text = "Optimizar";
            this.optimizar.UseVisualStyleBackColor = false;
            this.optimizar.Click += new System.EventHandler(this.optimizar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 942);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.RichTextBox areaanalizar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox consola;
        private System.Windows.Forms.Button compilar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button tablasimbolos;
        private System.Windows.Forms.Button errores;
        private System.Windows.Forms.Button optimo;
        private System.Windows.Forms.Button optimizar;
        private System.Windows.Forms.RichTextBox consolaOptimizar;
        private System.Windows.Forms.RichTextBox areaOptimizar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button graph;
    }
}

