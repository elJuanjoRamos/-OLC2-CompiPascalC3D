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
            var funciones = root.ChildNodes[0].ChildNodes[0];

            LinkedList<Instruction> lista_funciones = new LinkedList<Instruction>();
            lista_funciones = (new FunctionOptimize()).FUNCTION_LIS(funciones, lista_funciones);

            get_code_to_optimize(lista_funciones);

        }



        public void get_code_to_optimize(LinkedList<Instruction> lista_actual)
        {
            
            foreach (Function func in lista_actual)
            {
                var res = func.Optimize();
            }


        }
    }
}
