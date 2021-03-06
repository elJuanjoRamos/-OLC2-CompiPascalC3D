using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Analizer.Grammar;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.AST;
using CompiPascalC3D.Analizer.Languaje.Ambits;
using CompiPascalC3D.Analizer.Controller;
using CompiPascalC3D.Analizer.Languaje.Sentences;

namespace CompiPascalC3D.Analizer.Syntactic
{
    class Syntactic
    {
        //VARIABLES GLOBALES
        public Ambit general = new Ambit();

        public void analizer(String cadena, string paths)
        {

            ReporteController.Instance.set_path(paths);


            GrammarPascal grammar = new GrammarPascal();
            LanguageData languageData = new LanguageData(grammar);
            //ArrayList elemetos_heredados = new ArrayList();


            var i = new LanguageData(grammar);
            foreach (var item in i.Errors)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
            Parser parser = new Parser(new LanguageData(grammar));
            ParseTree tree = parser.Parse(cadena);
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
            //PROGRAM BODY -> GRAMATICA
            
            var program_body = root.ChildNodes[0].ChildNodes[3];

            //LISTADO DE TYPES Y ARREGLOS
            LinkedList<Instruction> lista_types = new LinkedList<Instruction>();
            lista_types = (new TypeAST()).TYPE_LIST(program_body.ChildNodes[0], lista_types, 1);

            //LISTADO DE DECLARACIONES
            LinkedList<Instruction> lista_declaraciones = new LinkedList<Instruction>();
            ArrayList elemetos_heredados = new ArrayList();
            lista_declaraciones = (new DeclarationAST()).LIST_DECLARATIONS(program_body.ChildNodes[1], lista_declaraciones, elemetos_heredados, 1);

            //LISTA DE FUNCIONES
            LinkedList<Instruction> lista_funciones = new LinkedList<Instruction>();
            elemetos_heredados.Clear();
            lista_funciones = (new FunctionAST()).FUNCTION_LIST(program_body.ChildNodes[2], lista_funciones, elemetos_heredados, 0);

            //LISTADO DE SENTENCIAS SENTENCIAS
            LinkedList<Instruction> listaInstrucciones = (new InstructionAST()).INSTRUCTIONS_BODY(program_body.ChildNodes[3], 1);


            execute(lista_declaraciones, lista_funciones, listaInstrucciones);
        }

        public void execute(LinkedList<Instruction> variables,
            LinkedList<Instruction> funciones,
            LinkedList<Instruction> instrucciones)
        {

            ReporteController.Instance.save_ambit(general, general.Ambit_name);
            
            var main = C3D.C3DController.Instance.save_code("\n\nint main(){\n", 0);

            main += C3D.C3DController.Instance.save_comment("Inicia declaracion variables", 1, false);

            
            foreach (var item in variables)
            {
                try
                {
                    var result = item.Execute(general);
                    if (result == null)
                    {
                        continue;
                    }
                    main += result; 
                }
                catch (Exception)
                {

                    throw;
                }
            }
            main += C3D.C3DController.Instance.save_comment("Fin declaracion variables", 1, true);

            foreach (Function funcion in funciones)
            {
                funcion.saveFunc(general);
            }

           
            foreach (var item in instrucciones)
            {
                try
                {
                    var result = item.Execute(general);
                    if (result == null)
                    {
                        continue;
                    }
                    main += result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            main += C3D.C3DController.Instance.save_code("\n return 0;\n}", 0);

            var funcion_String = "";
            foreach (var funcion in funciones)
            {
                var result = funcion.Execute(general);
                if (result == null)
                {
                    continue;
                }
                funcion_String += result;
            }


            #region IMPRIMIR NATIVAS

            var nativas = C3D.C3DController.Instance.print_natives();

            //var funcion_String = C3D.C3DController.Instance.get_functions();
            #endregion

            C3D.C3DController.Instance.save_Genenal(nativas, funcion_String, main);

            ReporteController.Instance.save_ambit(general, general.Ambit_name);

        }
    }
}
