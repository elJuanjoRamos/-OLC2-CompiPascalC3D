using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using CompiPascalC3D.Analizer.Languaje.Symbols;
using CompiPascalC3D.Optimize.Languaje.Block;
using CompiPascalC3D.Optimize.Languaje.Symbols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CompiPascalC3D.Analizer.Controller
{
    class ReporteController
    {

        Dictionary<string, Ambit> ambit_list;
        ArrayList optimizacionAritmetica = new ArrayList();
        LinkedList<CompiPascalC3D.Optimize.Languaje.Abstract.Instruction> 
            functions = new LinkedList<CompiPascalC3D.Optimize.Languaje.Abstract.Instruction>();

        Ambit general;

        string path = "";
        private readonly static ReporteController _instance = new ReporteController();

        private ReporteController()
        {
            this.ambit_list = new Dictionary<string, Ambit>();
        }

        public static ReporteController Instance
        {
            get
            {
                return _instance;
            }
        }

        public string Tipo_encabezado { get => tipo_encabezado; set => tipo_encabezado = value; }

        string tipo_encabezado = "";

        //VARIABLES GLOBALES
        string head = "<!DOCTYPE html>\n" +
           "<html>\n" +
           "<head>\n" +
           "    <meta charset='utf-8'>\n" +
           "    <meta http-equiv='X-UA-Compatible' content='IE=edge'>\n" +
           "    <title> Repote </title>\n" +
           "    <meta name='viewport' content='width=device-width, initial-scale=1'>\n" +
           "    <link rel='stylesheet' type='text/css' media='screen' href='main.css'>\n" +
           "    <script src='main.js'></script>\n" +
           "    <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css\">\n" +
           "    <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\">\n" +
           "</head>" +
           "<body>\n" +
           "  <nav class=\"navbar navbar-light bg-light\">\n" +
           "    <span class=\"navbar-brand mb-0 h1\">Organizacion de Lenguajes y Compiladores 2</span>\n" +
           "  </nav>" + "<div class=\"container\">\n" +
            "    <div class=\"jumbotron jumbotron-fluid\">\n" +
            "      <div class=\"container\">\n";
            
        private string open_table =   
            "    <div class=\"row\">\n" +
            "    <table id=\"data\"  cellspacing=\"0\" style=\"width: 100 %\" class=\"table table-striped table-bordered table-sm\">\n" +
            "      <thead class=\"thead-dark\">\n" +
            "        <tr>\n";




        private string close_table = "</tbody>\n" +
                            "    </table>\n" +
                            "</div>\n" +
                            "  </div>";

        private string script =
            "  <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\" ></script>\n" +
            "  <script src=\"https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>\n" +
            "  <script src=\"https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js\"></script>\n" +
            "  <script src=\"https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js\" ></script>\n" +
            "<script>" +
            "$(document).ready(function () { " +
             "$('#data').DataTable(" +

             "{ \"aLengthMenu\" " + ":" + " [[10, 25, 50, 100, -1], [10, 25, 50, 100, \"All\"]], \"iDisplayLength\" : 10" +
             "}" +
             ");" +
             "}" +
             "); " +
           "</script>";




        //SET PATH PARA REPORTE
        public void set_path(string pt)
        {
            this.path = pt;
        }

        //SET AMBITOS
        public void set_ambit(Ambit ambit)
        {
            this.general = ambit;
        }


        public void save_ambit(Ambit ambit, string ambit_name)
        {
            this.ambit_list[ambit_name.ToLower()] = ambit;
        }
        public void clear_ambits()
        {
            this.ambit_list.Clear();
        }



        public bool generate_report()
        {
            tipo_encabezado =
            "        <h1 class=\"display-4\"> Tabla de Simbolos</h1>\n" +
            "        <p class=\"lead\">Listado de variables, funciones y procedimientos detectados por el analizador</p>\n" +
            "      </div>\n" +
            "    </div>\n";


            string body1 = 
          "<th scope =\"col\">No</th>\n" +
            "          <th scope=\"col\">Nombre</th>\n" +
            "          <th scope=\"col\">Tipo</th>\n" +
            "          <th scope=\"col\">Valor</th>\n" +
            "          <th scope=\"col\">Posicion</th>\n" +
            "          <th scope=\"col\">Tipo Objeto</th>\n" +
            "          <th scope=\"col\">Ambito</th>\n" +
            "          <th scope=\"col\">No parametros</th>\n" +
          "</tr>\n" +
          "      </thead>" +
          "<tbody>";



            string cadena = "";

            var i = 1;
            foreach (Ambit ambit in ambit_list.Values)
            {
                var lista_variables = ambit.Variables.Values;
                foreach (Identifier variable in lista_variables)
                {
                    cadena += "<tr><td>"+i+"</td><td>" + variable.Id + "</td><td>" + variable.DataType 
                        + "</td><td>" + variable.Default_value + "</td><td>" + variable.Position + "</td><td>" + variable.Tipo_dato + "</td><td>" 
                        + ambit.Ambit_name + "</td><td>--" + "</td></tr>";
                    i++;
                }
                var lista_funciones = ambit.Functions.Values;
                foreach (Function item in lista_funciones)
                {
                    cadena += "<tr><td>" + i + "</td><td>" + item.UniqId + "</td><td>" + item.Tipe
                        + "</td><td>" + item.Retorno + "</td><td>" + "--" + "</td><td>" + "Funcion" + "</td><td>"
                        + ambit.Ambit_name + "</td><td>"+ item.Parametos.Count + "</td></tr>";
                    i++;
                }

            }

            


            string html = head + tipo_encabezado + open_table+ body1 + cadena + close_table +
            script +
            "</body>" +
            "</html>";



            /*creando archivo html*/

            return print_file_report("1.Tabla_Simbolos.html", html);

        }


        public bool generate_error_retort()
        {
            tipo_encabezado =
            "        <h1 class=\"display-4\"> Listado de Errores</h1>\n" +
            "        <p class=\"lead\">Listado de errores detectador por el analizador</p>\n" +
            "      </div>\n" +
            "    </div>\n";

            string table_head =
            "<th scope =\"col\">No</th>\n" +
              "          <th scope=\"col\">Error</th>\n" +
              "          <th scope=\"col\">Tipo</th>\n" +
              "          <th scope=\"col\">Linea</th>\n" +
              "          <th scope=\"col\">Columna</th>\n" +
            "</tr>\n" +
            "      </thead>" +
            "<tbody>";


            var error_string = "";


            if (Controller.ErrorController.Instance.containLexicalError())
            {
                ArrayList array_lexico = Controller.ErrorController.Instance.returnLexicalErrors();
                error_string = get_errors(array_lexico, "Lexico");
            }
            else if (Controller.ErrorController.Instance.containSemantycError())
            {
                ArrayList array_semant = Controller.ErrorController.Instance.returnSemanticErrors();
                error_string = get_errors(array_semant, "Semantico"); ;
            } else
            {
                ArrayList array_sint = Controller.ErrorController.Instance.returnSintacticErrors();
                error_string = get_errors(array_sint, "Sintactico"); ;
            }




            string html = head  + tipo_encabezado + open_table + table_head + error_string + close_table +
            script +
            "</body>" +
            "</html>";


            return print_file_report("2.Reporte Errores.html", html);

        }

        public string get_errors(ArrayList lista_error, string tipo)
        {
            var cadena = "";

            int i = 1;

            if (lista_error.Count > 0)
            {
                foreach (Error error in lista_error)
                {
                    cadena += "<tr><td>" + i + "</td><td>" + error.Message + "</td><td> " + tipo +" </td><td>" + error.Row + "</td><td>" + error.Column + "</td></tr>";
                    i++;
                }
            }

            return cadena;
        }


        public bool print_file_report(string name_file, string text)
        {
            string path1 = this.path + "\\" + name_file;

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path1))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        //////////////////////////////////////////////
        //  REPORTES DE OPTIMIZACION

        public void get_funciones(LinkedList<CompiPascalC3D.Optimize.Languaje.Abstract.Instruction> linkedList)
        {
            this.functions = linkedList;
        }

        public bool graph_blocks()
        {
            var graph = "digraph G {\n\n";
            int i = 1;
            foreach (CompiPascalC3D.Optimize.Languaje.Function.Function func in functions)
            {
                graph += "\tsubgraph cluster_" + i+ " {\n\n \t\tblock" + i 
                    + "_start" + i+ " [shape=Mdiamond; label=\"start\"];\n\t\tblock" + i + "_end" + i+ " [shape=Msquare; label=\"end\"];\n\t\tcolor=blue\n";

                graph += "\t\tlabel = \" function " + func.Id +" \";\n";

                //NODOS
                foreach (Blocks blocks in func.Block_instructions)
                {
                    graph += "\t\tblock"+i+"_b" + blocks.Number + "[label =<" + blocks.get_instruction() + ">; shape=rectangle];\n";
                }

                //ENLACES SECUENCIALES
                int j = 0;
                var texo = "";
                foreach (Blocks blocks in func.Block_instructions)
                {
                    j++;
                    texo += (j == func.Block_instructions.Count) ? "block" + i + "_b" + blocks.Number + "->" + "block" + i + "_end" + i +";\n" : "block" + i + "_b" + blocks.Number + "->";
                }
                graph += "\t\t" + "block" + i + "_start" + i + "->" + texo;


                //ENLACES A TAGS
                texo = "";
                foreach (Blocks blocks in func.Block_instructions)
                {
                    if (blocks.OutLabel != "")
                    {
                        int index = get_index_block(blocks.OutLabel, func.Block_instructions);
                        texo += (index != -1) ? "block" + i + "_b" + blocks.Number + "->block" + i + "_b" + index+ ";" : ""; 
                    }
                }
                graph += "\t\t" + texo;


                graph += "\n\n\t}\n\n";
                i++;
            }
            graph += "\n}";

            return print_file_report("4.Reporte Bloques.dot", graph);
        }

        public int get_index_block(string label_name, LinkedList<Blocks> blocks)
        {
            foreach (Blocks blocks1 in blocks)
            {
                if (blocks1.InLabel.Equals(label_name))
                {
                    return blocks1.Number;
                }
            }
            return -1;
        }

        public void Clean()
        {
            this.optimizacionAritmetica.Clear();
        }

        public void set_optimizacion(string rule, string code_deleted, string code_added, int row, int col, string ambit)
        {
            this.optimizacionAritmetica.Add(new SymbolOptimizado("Bloque", rule, code_deleted, code_added, row, col, ambit));
        }

        public bool set_optimizacion_reporte()
        {

            tipo_encabezado =
            "        <h1 class=\"display-4\"> Listado de Optimizacion</h1>\n" +
            "        <p class=\"lead\">Reglas de Optimizacion detectadas por el analizador</p>\n" +
            "      </div>\n" +
            "    </div>\n";


            string table_head =
            "<th scope =\"col\">No</th>\n" +
              "          <th scope=\"col\">Tipo Optimizacion</th>\n" +
              "          <th scope=\"col\">Regla</th>\n" +
              "          <th scope=\"col\">Codigo Eliminado</th>\n" +
              "          <th scope=\"col\">Codigo Agregado</th>\n" +
              "          <th scope=\"col\">Ambito</th>\n" +
              "          <th scope=\"col\">Linea</th>\n" +
              "          <th scope=\"col\">Columna</th>\n" +
            "</tr>\n" +
            "      </thead>" +
            "<tbody>";

            var cadena = "";

            int i = 1;

            if (optimizacionAritmetica.Count > 0)
            {
                foreach (SymbolOptimizado error in optimizacionAritmetica)
                {
                    cadena += "<tr><td>" + i + "</td><td>" + error.Tipo + "</td><td> "+error.Regla+" </td><td>" + error.Codigo_eliminado+ "</td><td>" 
                        + error.Codigo_agregado + "</td><td>" + error.Ambit + "</td><td>" + error.Row + "</td><td>" + error.Column + "</td></tr>";
                    i++;
                }
            }
            string html = head + tipo_encabezado + open_table + table_head + cadena + close_table +
            script +
            "</body>" +
            "</html>";


            return print_file_report("3.Reporte Optimizacion.html", html);


        }


    }
}
