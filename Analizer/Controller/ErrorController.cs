using CompiPascalC3D.Analizer.Languaje.Symbols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Controller
{
class ErrorController
{


        private readonly static ErrorController _instance = new ErrorController();

        private ErrorController()
        {
        }

        public static ErrorController Instance
        {
            get
            {
                return _instance;
            }
        }


        ArrayList syntacticErrors = new ArrayList();
        ArrayList lexicalErrors = new ArrayList();
        ArrayList semantycErrors = new ArrayList();



        public void SyntacticError(string message, int row, int col)
        {
            syntacticErrors.Add(new Error(message, row, col));
        }

        public void LexicalError(string message, int row, int col)
        {
            lexicalErrors.Add(new Error(message, row, col));
        }
        public void SemantycErrors(string message, int row, int col)
        {
            semantycErrors.Add(new Error(message, row, col));
        }

        public void Clean()
        {
            semantycErrors.Clear();
            syntacticErrors.Clear();
            lexicalErrors.Clear();
        }


        public ArrayList returnLexicalErrors()
        {
            return this.lexicalErrors;
        }
        public ArrayList returnSintacticErrors()
        {
            return this.syntacticErrors;
        }
        public ArrayList returnSemanticErrors()
        {
            return this.semantycErrors;
        }
        public string getLexicalError()
        {
            return getText(lexicalErrors, "Lexico");
        }
        public bool containLexicalError()
        {
            return (lexicalErrors.Count > 0);
        }

        public string getSintactycError()
        {
            return getText(syntacticErrors, "Sintactico");
        }

        public bool containSyntacticError()
        {
            return (syntacticErrors.Count > 0);
        }

        public string getSemantycError()
        {
            return getText(semantycErrors, "Sintactico");
        }
        public bool containSemantycError()
        {
            return (semantycErrors.Count > 0);
        }





        public string getText(ArrayList ar, string tipo)
        {
            var text = "";
            if (ar.Count > 0)
            {
                foreach (var item in ar)
                {
                    var err = (Error)item;
                    text = text + tipo + ": " + err.toString() + "\n";
                }
                text = "\n" + text;
            }

            return text;

        }
    }
   
}
