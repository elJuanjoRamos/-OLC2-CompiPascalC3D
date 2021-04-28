using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;

namespace CompiPascalC3D.Optimize.Grammar
{
    class GrammarC3D : Irony.Parsing.Grammar
    {
        public GrammarC3D()
             : base(caseSensitive: false)
        {
            #region Constantes
            CommentTerminal LINE_COMMENT = new CommentTerminal("LINE_COMMENT", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal MULTI_LINE_COMMENT = new CommentTerminal("MULTI_LINE_COMMENT", "/*", "*/");
            CommentTerminal MULTI_LINE_COMMENT2 = new CommentTerminal("MULTI_LINE_COMMENT2", "(*", "*)");

            NonGrammarTerminals.Add(LINE_COMMENT);
            NonGrammarTerminals.Add(MULTI_LINE_COMMENT);

            var REAL = new RegexBasedTerminal("REAL", "[0-9]+[.][0-9]+");
            var NUMERO = new NumberLiteral("NUMERO");

            var TEMPORAL = new RegexBasedTerminal("TEMPORAL", "T[0-9]+");
            var LABEL = new RegexBasedTerminal("LABEL", "L[0-9]+");


            var IDENTIFIER = TerminalFactory.CreateCSharpIdentifier("IDENTIFIER");
            var CADENA = new StringLiteral("CADENA", "\"");
            var CADENA_SIMPLE = new StringLiteral("CADENA_SIMPLE", "\'");


            #endregion

            #region TERMINALES
            //PUNTEROS
            var HP = ToTerm("HP", "TK_HP");
            var SP = ToTerm("SP", "TK_SP");

            //PUNTUACION
            var DOS_PUNTOS = ToTerm(":", "TK_DOS_PUNTOS");
            var PUNTO_COMA = ToTerm(";", "TK_PUNTO_COMA");
            var PUNTO = ToTerm(".", "TK_PUNTO");
            var COMA = ToTerm(",", "TK_COMA");
            var PAR_IZQ = ToTerm("(", "TK_PAR_IZQ");
            var PAR_DER = ToTerm(")", "TK_PAR_DER");
            var KEY_IZQ = ToTerm("{", "LEY_IZQ");
            var KEY_DER = ToTerm("}", "KEY_DER");
            var COR_IZQ = ToTerm("[", "TK_COR_IZQ");
            var COR_DER = ToTerm("]", "TK_COR_DER");
            //ARITMETIC
            var PLUS = ToTerm("+", "TK_PLUS");
            var MIN = ToTerm("-", "TK_MIN");
            var POR = ToTerm("*", "TK_POR");
            var DIVI = ToTerm("/", "TK_DIVI");
            var MODULE = ToTerm("%", "TK_MODULE");
            //Relational
            var HIGHER = ToTerm(">", "TK_HIGHER");
            var LESS = ToTerm("<", "TK_LESS");
            var HIGHER_EQUAL = ToTerm(">=", "TK_HIGHER_EQUAL");
            var LESS_EQUAL = ToTerm("<=", "TK_LESS_EQUAL");
            var EQUALS = ToTerm("=", "TK_EQUALS");
            var NOEQUALS = ToTerm("!=", "TK_EQUALS");
            //Palabras Reservadas
            var RESERV_HEAP = ToTerm("Heap", "RESERV_HEAP");
            var RESERV_STACK = ToTerm("Stack", "RESERV_STACK");
            var RESERV_If = ToTerm("if", "RESERV_IF");
            var RESERV_GOTO = ToTerm("goto", "RESERV_GOTO");
            var RESERV_INT = ToTerm("int", "RESERV_INT");
            var RESERV_FLOAT = ToTerm("float", "RESERV_FLOAT");
            var RESERV_INCLUDE = ToTerm("#include <stdio.h>");
            var RESERV_STACKDEF = ToTerm("float Stack[100000];");
            var RESERV_HEAPDEF = ToTerm("float Heap[100000];");
            var RESERV_PRINT = ToTerm("printf");
            var RESERV_RETURN = ToTerm("return");

            //funcion
            var RESERV_VOID = ToTerm("void", "RESERV_VOID");

            #endregion


            #region NO TERMINALES
            NonTerminal init = new NonTerminal("init");
            NonTerminal INSTRUCTION = new NonTerminal("INSTRUCTION");
            NonTerminal INSTRUCTIONS = new NonTerminal("INSTRUCTIONS");

            NonTerminal FUNCTION_LIST = new NonTerminal("FUNCTION_LIST", "FUNCTION_LIST");
            NonTerminal TIPOFUNCTION = new NonTerminal("TIPOFUNCTION");

            NonTerminal start = new NonTerminal("start");


            #region EXPRESIONES
            NonTerminal EXPRESION = new NonTerminal("EXPRESION", "EXPRESION");
            NonTerminal TERMINAL = new NonTerminal("TERMINAL", "TERMINAL");
            #endregion

            #region FUNCTION
            NonTerminal RETURN = new NonTerminal("RETURN", "RETURN");
            NonTerminal CALL = new NonTerminal("CALL", "CALL");

            #endregion

            #region ASIGNACION AND ACCESS
            NonTerminal ASSIGNATION = new NonTerminal("TERMINAL", "TERMINAL");
            NonTerminal ACCESS = new NonTerminal("ACCESS", "ACCESS");
            NonTerminal INDEX = new NonTerminal("INDEX", "INDEX");

            #endregion

            #region ARITMETICAS
            NonTerminal ARITMETICA = new NonTerminal("ARITMETICA", "ARITMETICA");
            NonTerminal SIMB = new NonTerminal("SIMB", "SIMB");

            #endregion

            #region TAGS
            NonTerminal SET_LABEL = new NonTerminal("SET_LABEL", "SET_LABEL");

            #endregion

            #region PRINT
            NonTerminal PRINT = new NonTerminal("PRINT", "PRINT");
            NonTerminal PRINT_TERM = new NonTerminal("PRINT_TERM", "PRINT_TERM");

            #endregion

            #region IF
            NonTerminal IF = new NonTerminal("IF", "IF");
            NonTerminal GOTO = new NonTerminal("GOTO", "GOTO");


            #endregion

            #endregion

            RegisterOperators(1, Associativity.Left, PLUS, MIN);
            RegisterOperators(2, Associativity.Left, POR, DIVI);
            RegisterOperators(3, Associativity.Left, MODULE);
            RegisterOperators(4, Associativity.Left, HIGHER_EQUAL, LESS_EQUAL, LESS, HIGHER);
            RegisterOperators(5, Associativity.Left, EQUALS, NOEQUALS);
            RegisterOperators(6, Associativity.Left, PAR_IZQ, PAR_DER);



            #region GRAMATICA
            init.Rule = start;

            start.Rule
                = FUNCTION_LIST
                ;


            FUNCTION_LIST.Rule
               = TIPOFUNCTION + IDENTIFIER + PAR_IZQ + PAR_DER + KEY_IZQ + INSTRUCTIONS + KEY_DER
               + FUNCTION_LIST
               | Empty
               ;

            TIPOFUNCTION.Rule
                = RESERV_VOID
                | RESERV_INT
                ;

            INSTRUCTIONS.Rule = MakePlusRule(INSTRUCTIONS, INSTRUCTION);

            INSTRUCTION.Rule
                = IF
                | GOTO
                | ASSIGNATION
                | ARITMETICA
                | SET_LABEL
                | PRINT
                | RETURN
                | CALL
                ;




            ARITMETICA.Rule = TERMINAL + EQUALS + EXPRESION + PUNTO_COMA;

            #region EXPRESION
            EXPRESION.Rule
                = TERMINAL + PLUS + TERMINAL
                | TERMINAL + MIN + TERMINAL
                | TERMINAL + POR + TERMINAL
                | TERMINAL + DIVI + TERMINAL
                | TERMINAL
                | ACCESS
                ;
            TERMINAL.Rule
                = NUMERO
                | REAL
                | TEMPORAL
                | HP
                | SP
                | MIN + NUMERO
                | MIN + REAL
                ;
            #endregion

            #region IF
            IF.Rule =
                RESERV_If + PAR_IZQ + TERMINAL + SIMB + TERMINAL + PAR_DER + RESERV_GOTO + LABEL + PUNTO_COMA;
            GOTO.Rule =
                RESERV_GOTO + LABEL + PUNTO_COMA;

            SIMB.Rule
                = HIGHER
                | HIGHER_EQUAL
                | LESS
                | LESS_EQUAL
                | EQUALS + EQUALS
                | NOEQUALS
                ;
            #endregion

            #region ASIGNACION
            ASSIGNATION.Rule
                = RESERV_STACK + COR_IZQ + PAR_IZQ + RESERV_INT + PAR_DER + TERMINAL + COR_DER + EQUALS + TERMINAL + PUNTO_COMA
                | RESERV_HEAP + COR_IZQ + PAR_IZQ + RESERV_INT + PAR_DER + TERMINAL + COR_DER + EQUALS + TERMINAL + PUNTO_COMA
                ;
            #endregion

            #region ACCESS
            ACCESS.Rule
                = RESERV_HEAP + COR_IZQ + PAR_IZQ + RESERV_INT + PAR_DER + TERMINAL + COR_DER 
                | RESERV_STACK + COR_IZQ + PAR_IZQ + RESERV_INT + PAR_DER + TERMINAL + COR_DER 
                ;

            #endregion

            #region FUNCTION
            RETURN.Rule
                = RESERV_RETURN + TERMINAL + PUNTO_COMA
                | RESERV_RETURN + PUNTO_COMA
                ;
            CALL.Rule
                = IDENTIFIER + PAR_IZQ + PAR_DER + PUNTO_COMA;
            #endregion

            #region LABEL
            SET_LABEL.Rule
                = LABEL + DOS_PUNTOS;
            #endregion

            #region PRINT
            PRINT.Rule =
                RESERV_PRINT + PAR_IZQ + CADENA + COMA + PRINT_TERM + PAR_DER + PUNTO_COMA
                ;

            PRINT_TERM.Rule
                = 
                PAR_IZQ + RESERV_INT + PAR_DER+ TERMINAL
                | PAR_IZQ + RESERV_FLOAT + PAR_DER + TERMINAL
                | CADENA_SIMPLE
                ;

            #endregion

            #endregion

            #region Preferencias
            this.Root = init;
            #endregion
        }
    }
}
