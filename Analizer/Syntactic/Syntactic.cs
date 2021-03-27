﻿using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CompiPascalC3D.Analizer.Grammar;
using CompiPascalC3D.Analizer.Languaje.Abstracts;
using CompiPascalC3D.Analizer.AST;
using CompiPascalC3D.Analizer.Languaje.Ambits;

namespace CompiPascalC3D.Analizer.Syntactic
{
    class Syntactic
    {
        //VARIABLES GLOBALES
        public Ambit general = new Ambit();

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
            //PROGRAM BODY -> GRAMATICA
            
            var program_body = root.ChildNodes[0].ChildNodes[3];


            //LISTADO DE DECLARACIONES
            //LISTA DE DECLARCION DE VARIABLES
            LinkedList<Instruction> lista_declaraciones = new LinkedList<Instruction>();
            ArrayList elemetos_heredados = new ArrayList();

            lista_declaraciones = (new DeclarationAST()).LIST_DECLARATIONS(program_body.ChildNodes[1], lista_declaraciones, elemetos_heredados);

            //LISTADO DE SENTENCIAS SENTENCIAS
            LinkedList<Instruction> listaInstrucciones = (new InstructionAST()).INSTRUCTIONS_BODY(program_body.ChildNodes[3], 0);


            execute(lista_declaraciones, listaInstrucciones);
        }

        public void execute(LinkedList<Instruction> variables, LinkedList<Instruction> instrucciones)
        {
            foreach (var item in variables)
            {
                try
                {
                    var result = item.Execute(general);
                    if (result == null)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
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
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
