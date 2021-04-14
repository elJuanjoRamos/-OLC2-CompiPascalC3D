using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Languaje.Sentences;
using CompiPascalC3D.Analizer.Languaje.Symbols;
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



        public void generate_report()
        {
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
           "    <span class=\"navbar-brand mb-0 h1\">Lenguajes formales</span>\n" +
           "  </nav>";

            string body1 = "<div class=\"container\">\n" +
          "    <div class=\"jumbotron jumbotron-fluid\">\n" +
          "      <div class=\"container\">\n" +
          "        <h1 class=\"display-4\"> Tabla de Simbolos</h1>\n" +
          "        <p class=\"lead\">Listado de variables, funciones y procedimientos detectados por el analizador</p>\n" +
          "      </div>\n" +
          "    </div>\n" +
          "    <div class=\"row\">\n" +
          "    <table id=\"data\"  cellspacing=\"0\" style=\"width: 100 %\" class=\"table table-striped table-bordered table-sm\">\n" +
          "      <thead class=\"thead-dark\">\n" +
          "        <tr>\n"+
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

            
            string body2 = "</tbody>\n" +
           "    </table>\n" +
           "</div>\n" +
           "  </div>";

            string script =
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

            string html;

            html = head + body1 + cadena + body2 +
            script +
            "</body>" +
            "</html>";



            /*creando archivo html*/

            string path1 = this.path + "\\1.Tabla_Simbolos.html";

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path1))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(html);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
    }
}
