using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Analizer.Grammar;

namespace CompiPascalC3D.Analizer.Syntactic
{
    class Syntactic
    {
        public void analizer(String cadena, string paths)
        {
            GrammarC3D grammar = new GrammarC3D();
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

                        //ErrorController.Instance.LexicalError(err.Message, err.Location.Line + 1, err.Location.Column);
                    }
                    //Errores sintacticos
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(err);
                        //ErrorController.Instance.SyntacticError(err.Message, err.Location.Line + 1, err.Location.Column);
                    }
                }
                return;
            }
            if (root == null)
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine("funciono");
        }
    }
}
