using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Optimize.AST;
using CompiPascalC3D.Optimize.Grammar;
using CompiPascalC3D.Optimize.Languaje.Abstract;
using CompiPascalC3D.Optimize.Languaje.Function;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Optimize.Syntactic
{
    class SyntacticOptimize
    {
        string texto_general = "";
        public void get_C3D_to_optimize(string c3d_code, string paths)
        {
            ReporteController.Instance.set_path(paths);

            GrammarC3D grammar = new GrammarC3D();

            var i = new LanguageData(grammar);
            foreach (var item in i.Errors)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
            Parser parser = new Parser(i);
            ParseTree tree = parser.Parse(c3d_code);
            ParseTreeNode root = tree.Root;



            if (tree.ParserMessages.Count > 0)
            {
                foreach (var err in tree.ParserMessages)
                {
                    //Errores lexicos
                    if (err.Message.Contains("Invalid character"))
                    {

                        ErrorController.Instance.LexicalError(err.Message, err.Location.Line + 1, err.Location.Column);
                    }
                    //Errores sintacticos
                    else
                    {
                        ErrorController.Instance.SyntacticError(err.Message, err.Location.Line + 1, err.Location.Column);
                    }
                }
                return;
            }
            if (root == null)
            {
                return;
            }

            var start = root.ChildNodes[0];

            var funciones = start.ChildNodes[27];

            LinkedList<Instruction> lista_funciones = new LinkedList<Instruction>();
            lista_funciones = (new FunctionOptimize()).FUNCTION_LIS(funciones, lista_funciones);
            get_encabezado(start);
            get_code_to_optimize(lista_funciones);

        }


        public void get_encabezado(ParseTreeNode actual)
        {
            texto_general += actual.ChildNodes[0].Token.Text + "\n";

            for (int i = 1; i < 25; i++)
            {
                var result = actual.ChildNodes[i].Token.Text;

                result += (actual.ChildNodes[i].Term.ToString().Contains("RESERV")) ? " " : "";

                result += (result.Equals(";")) ? "\n" : "";

                texto_general += result;
            }

            var lista_temp = actual.ChildNodes[25];
            get_lista_temp(lista_temp);

            texto_general += ";\n" + "\n";
        }
        public void get_lista_temp(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count > 0)
            {
                texto_general += actual.ChildNodes[0].Token.Text;
                texto_general += actual.ChildNodes[1].Token.Text;
                get_lista_temp(actual.ChildNodes[2]);
            }
        }


        public void get_code_to_optimize(LinkedList<Instruction> lista_actual)
        {
            LinkedList<Instruction> newList = new LinkedList<Instruction>();
            foreach (Function func in lista_actual)
            {
                var res = func.Optimize();
                newList.AddLast((Instruction)res);
            }

            foreach (Function func in newList)
            {
                texto_general += func.Code();
            }

            ReporteController.Instance.get_funciones(lista_actual);

        }

        public string get_texto()
        {
            return texto_general;
        }
    }
}
