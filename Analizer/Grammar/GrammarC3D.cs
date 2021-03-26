using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Grammar
{
    class GrammarC3D : Irony.Parsing.Grammar
    {
        public GrammarC3D()
         : base(caseSensitive: false)
        {
            #region Lexical structure


            //COMENTARIOS
            CommentTerminal LINE_COMMENT = new CommentTerminal("LINE_COMMENT", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal MULTI_LINE_COMMENT = new CommentTerminal("MULTI_LINE_COMMENT", "(*", "*)");
            CommentTerminal MULTI_LINE_COMMENT_LLAVE = new CommentTerminal("MULTI_LINE_COMMENT_LLAVE", "{", "}");

            NonGrammarTerminals.Add(LINE_COMMENT);
            NonGrammarTerminals.Add(MULTI_LINE_COMMENT);
            NonGrammarTerminals.Add(MULTI_LINE_COMMENT_LLAVE);


            #endregion



            #region ER
            var REAL = new RegexBasedTerminal("REAL", "[0-9]+[.][0-9]+");
            var NUMERO = new NumberLiteral("NUMERO");
            var IDENTIFIER = TerminalFactory.CreateCSharpIdentifier("IDENTIFIER");
            var IDENTIFIER_ARRAY_TYPE = TerminalFactory.CreateCSharpIdentifier("IDENTIFIER_ARRAY_TYPE");
            var CADENA = new StringLiteral("CADENA", "\'");
            #endregion

            #region Terminales
            var PUNTO_COMA = ToTerm(";", "TK_PUNTO_COMA");
            var PUNTO = ToTerm(".", "TK_PUNTO");
            var COMA = ToTerm(",", "TK_COMA");
            var DOS_PUNTOS = ToTerm(":", "TK_DOS_PUNTOS");
            var PAR_IZQ = ToTerm("(", "TK_PAR_IZQ");
            var PAR_DER = ToTerm(")", "TK_PAR_DER");
            var COR_IZQ = ToTerm("[", "TK_COR_IZQ");
            var COR_DER = ToTerm("]", "TK_COR_DER");
            //Aritmethic    
            var PLUS = ToTerm("+", "TK_PLUS");
            var MIN = ToTerm("-", "TK_MIN");
            var POR = ToTerm("*", "TK_POR");
            var DIVI = ToTerm("/", "TK_DIVI");
            var MODULE = ToTerm("%", "TK_MODULE");
            //Logic
            var AND = ToTerm("and", "TK_AND");
            var OR = ToTerm("or", "TK_OR");
            var NOT = ToTerm("not", "TK_NOT");
            //Relational
            var HIGHER = ToTerm(">", "TK_HIGHER");
            var LESS = ToTerm("<", "TK_LESS");
            var HIGHER_EQUAL = ToTerm(">=", "TK_HIGHER_EQUAL");
            var LESS_EQUAL = ToTerm("<=", "TK_LESS_EQUAL");
            var EQUALS = ToTerm("=", "TK_EQUALS");
            var DISCTINCT = ToTerm("<>", "TK_DISCTINCT");
            //Reservadas

            var RESERV_INT = ToTerm("integer", "RESERV_INT");
            var RESERV_STR = ToTerm("string", "RESERV_STR");
            var RESERV_REAL = ToTerm("real", "RESERV_REAL");
            var RESERV_BOL = ToTerm("boolean", "RESERV_BOL");
            var RESERV_TYPE = ToTerm("type", "RESERV_TYPE");
            var RESERV_OBJ = ToTerm("object", "RESERV_OBJ");
            var RESERV_PROGRAM = ToTerm("program", "RESERV_PROGRAM");
            var RESERV_VAR = ToTerm("var", "RESERV_VAR");
            var RESERV_BEGIN = ToTerm("begin", "RESERV_BEGIN");
            var RESERV_END = ToTerm("end", "RESERV_END");
            var RESERV_CONST = ToTerm("const", "RESERV_CONST");
            var RESERV_TRUE = ToTerm("true", "RESERV_TRUE");
            var RESERV_FALSE = ToTerm("false", "RESERV_FALSE");
            var RESERV_ARRAY = ToTerm("array", "RESERV_ARRAY");
            var RESERV_OF = ToTerm("of", "RESERV_OF");

            #region IF TERMINALES
            var RESERV_IF = ToTerm("if", "RESERV_IF");
            var RESERV_THEN = ToTerm("then", "RESERV_THEN");
            var RESERV_ELSE = ToTerm("else", "RESERV_ELSE");
            #endregion

            #region CASE TERMINALES
            var RESERV_CASE = ToTerm("case", "RESERV_CASE");
            #endregion

            #region WHILE TERMINALES
            var RESERV_WHILE = ToTerm("while", "RESERV_WHILE");
            var RESERV_DO = ToTerm("do", "RESERV_DO");
            #endregion

            #region REPEAT TERMINALES
            var RESERV_REPEAT = ToTerm("repeat", "RESERV_REPEAT");
            var RESERV_UNTIL = ToTerm("until", "RESERV_UNTIL");
            #endregion

            #region FOR TERMINALES
            var RESERV_FOR = ToTerm("for", "RESERV_FOR");
            var RESERV_TO = ToTerm("to", "RESERV_TO");
            var RESERV_DOWN = ToTerm("downto", "RESERV_DOWN");
            var RESERV_BREAK = ToTerm("break", "RESERV_BREAK");
            var RESERV_CONTINUE = ToTerm("continue", "RESERV_CONTINUE");

            #endregion

            #region FUNCTION Y PROCEDURE TERMINALES
            var RESERV_FUNCTION = ToTerm("function", "RESERV_FUNCTION");
            var RESERV_PROCEDURE = ToTerm("procedure", "RESERV_PROCEDURE");


            #endregion

            #region FUNCIONES NATIVAS TERMINALES
            var RESERV_WRITE = ToTerm("write", "RESERV_WRITE");
            var RESERV_WRITEN = ToTerm("writeln", "RESERV_WRITEN");
            var RESERV_EXIT = ToTerm("exit", "RESERV_EXIT");
            var RESERV_GRAF = ToTerm("graficar_ts", "RESERV_GRAFICAR");

            #endregion


            RegisterOperators(1, Associativity.Left, PLUS, MIN);
            RegisterOperators(2, Associativity.Left, POR, DIVI);
            RegisterOperators(3, Associativity.Left, MODULE);
            RegisterOperators(4, Associativity.Left, HIGHER_EQUAL, LESS_EQUAL, LESS, HIGHER);
            RegisterOperators(5, Associativity.Left, EQUALS, DISCTINCT);
            RegisterOperators(6, Associativity.Left, AND, OR, NOT);
            RegisterOperators(7, Associativity.Left, PAR_IZQ, PAR_DER);

            #endregion

            #region No Terminales
            NonTerminal init = new NonTerminal("init");
            NonTerminal INSTRUCTION = new NonTerminal("INSTRUCTION");
            NonTerminal INSTRUCTIONS = new NonTerminal("INSTRUCTIONS");
            NonTerminal INSTRUCTIONS_BODY = new NonTerminal("INSTRUCTIONS_BODY");
            NonTerminal PROGRAM_BODY = new NonTerminal("PROGRAM_BODY", "PROGRAM_BODY");

            NonTerminal start = new NonTerminal("start");

            #region EXPLOGICA
            NonTerminal EXPRESION = new NonTerminal("EXPRESION", "EXPRESION");
            NonTerminal EXPRESION_PRIMA = new NonTerminal("EXPRESION_PRIMA", "EXPRESION_PRIMA");
            NonTerminal EXPLOGICA = new NonTerminal("EXPLOGICA", "EXPLOGICA");
            NonTerminal EXPLOGICA_PRIMA = new NonTerminal("EXPLOGICA_PRIMA", "EXPLOGICA_PRIMA");
            NonTerminal EXPRELACIONAL = new NonTerminal("EXPRELACIONAL", "EXPRELACIONAL");
            NonTerminal EXPRELACIONAL_PRIMA = new NonTerminal("EXPRELACIONAL_PRIMA", "EXPRELACIONAL_PRIMA");
            NonTerminal TERMINO = new NonTerminal("TERMINO", "TERMINO");
            NonTerminal TERMINO_PRIMA = new NonTerminal("TERMINO_PRIMA ", "TERMINO_PRIMA");
            NonTerminal FACTOR = new NonTerminal("FACTOR", "FACTOR");
            NonTerminal DATA_TYPE = new NonTerminal("DATA_TYPE", "DATA_TYPE");
            NonTerminal ID_TIPE = new NonTerminal("ID_TIPE", "ID_TIPE");

            #endregion


            #region VAR Y CONST 
            NonTerminal DECLARATION_LIST = new NonTerminal("DECLARATION_LIST", "DECLARATION_LIST");
            NonTerminal VAR_DECLARATION = new NonTerminal("VAR_DECLARATION", "VAR_DECLARATION");
            NonTerminal CONST_DECLARATION = new NonTerminal("CONST_DECLARATION", "CONST_DECLARATION");

            NonTerminal DECLARATION = new NonTerminal("DECLARATION", "DECLARATION");
            NonTerminal DECLARATION_BODY = new NonTerminal("DECLARATION_BODY", "DECLARATION_BODY");
            NonTerminal MORE_ID = new NonTerminal("MORE_ID", "MORE_ID");

            NonTerminal ASSIGNATION = new NonTerminal("ASSIGNATION", "ASSIGNATION");
            NonTerminal VAR_ASSIGNATE = new NonTerminal("VAR_ASSIGNATE", "VAR_ASSIGNATE");


            #endregion

            #region TYPES Y ARREGLOS
            NonTerminal TYPE_LIST = new NonTerminal("TYPE_LIST", "TYPE_LIST");
            NonTerminal TYPE = new NonTerminal("TYPE", "TYPE");
            NonTerminal TYPE_P = new NonTerminal("TYPE_P", "TYPE_P");
            NonTerminal ARRAY = new NonTerminal("ARRAY", "ARRAY");
            NonTerminal OBJECT = new NonTerminal("OBJECT", "OBJECT");
            NonTerminal MORE_ARRAY = new NonTerminal("MORE_ARRAY", "MORE_ARRAY");
            NonTerminal MORE_ACCES = new NonTerminal("MORE_ACCES", "MORE_ACCES");
            #endregion

            #region IF-THEN NO TERMINALES
            NonTerminal IFTHEN = new NonTerminal("IF-THEN", "IF-THEN");
            NonTerminal IF_SENTENCE = new NonTerminal("IF_SENTENCE", "IF_SENTENCE");
            NonTerminal ELIF = new NonTerminal("ELIF", "ELIF");
            #endregion

            #region CASE NO TERMINALES
            NonTerminal SENTENCE_CASE = new NonTerminal("SENTENCE_CASE", "SENTENCE_CASE");
            NonTerminal CASE_ELSE = new NonTerminal("CASE_ELSE", "CASE_ELSE");
            NonTerminal CASES = new NonTerminal("CASES", "CASES");
            NonTerminal CASE = new NonTerminal("CASE", "CASE");

            #endregion

            #region WHILE DO
            NonTerminal WHILE = new NonTerminal("WHILE", "WHILE");
            #endregion

            #region REPEAT UNTIL
            NonTerminal REPEAT_UNTIL = new NonTerminal("REPEAT_UNTIL", "REPEAT_UNTIL");
            NonTerminal CONTINUE = new NonTerminal("CONTINUE", "CONTINUE");

            #endregion

            #region FOR
            NonTerminal FOR = new NonTerminal("FOR", "FOR");
            NonTerminal TODOWN = new NonTerminal("TODOWN", "TODOWN");
            NonTerminal BREAK = new NonTerminal("BREAK", "BREAK");

            #endregion


            #region  FUNCIONES NATIVAS NO TERMINALES
            NonTerminal WRITE = new NonTerminal("WRITE", "WRITE");
            NonTerminal WRHITE_PARAMETER = new NonTerminal("WRHITE_PARAMETER", "WRHITE_PARAMETER");
            NonTerminal MORE_WRHITE_PARAMETER = new NonTerminal("WRHITE_PARAMETER", "WRHITE_PARAMETER");
            NonTerminal EXIT = new NonTerminal("EXIT", "EXIT");

            #endregion

            #region FUNCIONS NO TERMINALES
            NonTerminal FUNCTION_LIST = new NonTerminal("FUNCTION_LIST", "FUNCTION_LIST");
            NonTerminal FUNCTION = new NonTerminal("FUNCTION", "FUNCTION");
            NonTerminal PROCEDURE = new NonTerminal("PROCEDURE", "PROCEDURE");
            NonTerminal PARAMETER = new NonTerminal("PARAMETER", "PARAMETER");
            NonTerminal PARAMETER_BODY = new NonTerminal("PARAMETER_BODY", "PARAMETER_BODY");
            NonTerminal PARAMETER_END = new NonTerminal("PARAMETER_END", "PARAMETER_END");
            NonTerminal CALL = new NonTerminal("CALL", "CALL");
            NonTerminal CALL_FUNCTION_PROCEDURE = new NonTerminal("CALL_FUNCTION_PROCEDURE", "CALL_FUNCTION_PROCEDURE");
            NonTerminal CALL_PARAMETERS = new NonTerminal("CALL_PARAMETERS", "CALL_PARAMETERS");
            NonTerminal FUNCION_HIJA = new NonTerminal("FUNCION_HIJA", "FUNCION_HIJA");
            NonTerminal DECLARATION_LIST_HIJA = new NonTerminal("DECLARATION_LIST", "DECLARATION_LIST");

            //NonTerminal ARGUMENTS = new NonTerminal("ARGUMENTS", "ARGUMENTS");
            //NonTerminal REFERENCIA_VALOR = new NonTerminal("REFERENCIA_VALOR", "REFERENCIA_VALOR");
            #endregion

            
            #endregion

            #region Gramatica
            init.Rule = start;

            start.Rule = RESERV_PROGRAM + IDENTIFIER + PUNTO_COMA + PROGRAM_BODY;

            PROGRAM_BODY.Rule
                = TYPE_LIST
                + DECLARATION_LIST
                + FUNCTION_LIST
                + INSTRUCTIONS_BODY + PUNTO;

            INSTRUCTIONS_BODY.Rule
                = RESERV_BEGIN + INSTRUCTIONS + RESERV_END
                ;


            INSTRUCTIONS.Rule = MakePlusRule(INSTRUCTIONS, INSTRUCTION);

            INSTRUCTION.Rule
                = VAR_ASSIGNATE
                | IFTHEN
                | SENTENCE_CASE
                | WHILE
                | REPEAT_UNTIL
                | FOR
                | BREAK
                | CONTINUE
                | WRITE
                | CALL
                | EXIT
                ;

            INSTRUCTION.ErrorRule
                = SyntaxError + PUNTO_COMA
                | SyntaxError + RESERV_END
                ;


            #region DECLARACION & ASIGNACION

            DECLARATION_LIST.Rule
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST
               | RESERV_CONST + IDENTIFIER + EQUALS + EXPRESION + PUNTO_COMA + CONST_DECLARATION + DECLARATION_LIST
               | Empty
               ;

            DECLARATION_LIST.ErrorRule
                = SyntaxError + PUNTO_COMA;


            VAR_DECLARATION.Rule = IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION
                | Empty
                ;

            CONST_DECLARATION.Rule = IDENTIFIER + EQUALS + EXPRESION + PUNTO_COMA + CONST_DECLARATION
                | Empty
                ;

            DECLARATION_BODY.Rule
                = DOS_PUNTOS + DATA_TYPE + ASSIGNATION + PUNTO_COMA
                | COMA + IDENTIFIER + MORE_ID + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                ;

            ASSIGNATION.Rule
                = EQUALS + EXPRESION
                | Empty;
            ;

            MORE_ID.Rule = COMA + IDENTIFIER + MORE_ID
                | Empty
                ;



            VAR_ASSIGNATE.Rule = IDENTIFIER + DOS_PUNTOS + EQUALS + EXPRESION + PUNTO_COMA;


            DATA_TYPE.Rule = RESERV_REAL
                | RESERV_STR
                | RESERV_TYPE
                | RESERV_INT
                | RESERV_BOL
                | IDENTIFIER
                ;

            #endregion




            #region TYPES Y ARRAY
            TYPE_LIST.Rule
                = TYPE + TYPE_LIST
                | Empty
                ;

            TYPE.Rule = RESERV_TYPE + IDENTIFIER_ARRAY_TYPE + EQUALS + TYPE_P;

            TYPE_P.Rule
                = OBJECT
                | ARRAY
                ;


            ARRAY.Rule = RESERV_ARRAY
                + COR_IZQ + EXPLOGICA + PUNTO + PUNTO + EXPLOGICA + MORE_ARRAY + COR_DER + RESERV_OF + DATA_TYPE + PUNTO_COMA;
            
            MORE_ARRAY.Rule
                = COMA +  EXPLOGICA + PUNTO + PUNTO + EXPLOGICA + MORE_ARRAY
                | Empty;
            

            OBJECT.Rule =
                RESERV_OBJ + DECLARATION_LIST + RESERV_END + PUNTO_COMA;

            #endregion






            #endregion

            #region SENTENCIAS DE CONTROL

            #region IF-THEN
            IFTHEN.Rule
                = RESERV_IF + EXPRESION
                    + RESERV_THEN
                        + IF_SENTENCE
                    + ELIF;

            IF_SENTENCE.Rule = INSTRUCTIONS_BODY
                | Empty
                ;

            ELIF.Rule
                = RESERV_ELSE + IF_SENTENCE + PUNTO_COMA
                | RESERV_ELSE + IFTHEN
                | Empty
                ;


            #endregion

            #region CASE
            SENTENCE_CASE.Rule = RESERV_CASE + EXPRESION + RESERV_OF + CASES + CASE_ELSE + RESERV_END + PUNTO_COMA;

            CASES.Rule
                = CASE + CASES
                | Empty
                ;
            CASE.Rule = EXPRESION + DOS_PUNTOS + INSTRUCTIONS;


            CASE_ELSE.Rule = RESERV_ELSE + INSTRUCTIONS
                | Empty
                ;
            #endregion

            #region WHILE DO
            WHILE.Rule = RESERV_WHILE + EXPRESION + RESERV_DO + INSTRUCTIONS_BODY + PUNTO_COMA;
            #endregion

            #region REPEAT UNTIL
            REPEAT_UNTIL.Rule = RESERV_REPEAT + INSTRUCTIONS + RESERV_UNTIL + EXPRESION + PUNTO_COMA;
            #endregion

            #region FOR
            FOR.Rule
                = RESERV_FOR + IDENTIFIER + DOS_PUNTOS + EQUALS + EXPRESION + TODOWN + EXPRESION
                    + RESERV_DO
                        + INSTRUCTIONS_BODY + PUNTO_COMA
                ;

            TODOWN.Rule
                = RESERV_TO
                | RESERV_DOWN
                ;
            #endregion

            #endregion

            #region SENTENCIAS DE TRANSFERENCIA
            CONTINUE.Rule
               = RESERV_CONTINUE + PUNTO_COMA
               ;

            BREAK.Rule
               = RESERV_BREAK + PUNTO_COMA
               ;


            #endregion

            #region FUNCIONES Y PROCEDIMIENTOS



            FUNCTION_LIST.Rule
                = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                + DECLARATION_LIST_HIJA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST

                | RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                + DECLARATION_LIST_HIJA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCTION_LIST
                | Empty
                ;
            FUNCTION_LIST.ErrorRule
                = SyntaxError + PUNTO_COMA;

            PARAMETER.Rule
                = RESERV_VAR + IDENTIFIER + PARAMETER_BODY + DOS_PUNTOS + DATA_TYPE + PARAMETER_END
                | IDENTIFIER + PARAMETER_BODY + DOS_PUNTOS + DATA_TYPE + PARAMETER_END
                | Empty;

            PARAMETER_BODY.Rule
                = COMA + IDENTIFIER + PARAMETER_BODY
                | Empty
                ;
            PARAMETER_END.Rule = PUNTO_COMA + PARAMETER
                | Empty
                ;


            CALL.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER + PUNTO_COMA;

            CALL_PARAMETERS.Rule
                = EXPRESION + CALL_PARAMETERS
                | COMA + EXPRESION + CALL_PARAMETERS
                | Empty
                ;

            CALL_FUNCTION_PROCEDURE.Rule = IDENTIFIER + PAR_IZQ + CALL_PARAMETERS + PAR_DER;



            FUNCTION.Rule =
                RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                ;

            PROCEDURE.Rule =
                RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                ;

            FUNCION_HIJA.Rule
                = RESERV_FUNCTION + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + DOS_PUNTOS + DATA_TYPE + PUNTO_COMA
                + DECLARATION_LIST_HIJA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCION_HIJA

                | RESERV_PROCEDURE + IDENTIFIER + PAR_IZQ + PARAMETER + PAR_DER + PUNTO_COMA
                + DECLARATION_LIST_HIJA
                + FUNCION_HIJA
                + INSTRUCTIONS_BODY
                + PUNTO_COMA
                + FUNCION_HIJA
                | Empty
                ;

            DECLARATION_LIST_HIJA.Rule
               = RESERV_VAR + IDENTIFIER + DECLARATION_BODY + VAR_DECLARATION + DECLARATION_LIST_HIJA
               | RESERV_CONST + IDENTIFIER + EQUALS + EXPRESION + PUNTO_COMA + CONST_DECLARATION + DECLARATION_LIST_HIJA
               | Empty
               ;
            #endregion

            #region FUNCIONES NATIVAS

            WRITE.Rule = RESERV_WRITE + PAR_IZQ + WRHITE_PARAMETER + PAR_DER + PUNTO_COMA
                | RESERV_WRITEN + PAR_IZQ + WRHITE_PARAMETER + PAR_DER + PUNTO_COMA
                ;

            WRHITE_PARAMETER.Rule
                = EXPRESION + MORE_WRHITE_PARAMETER
                | Empty
                ;
            MORE_WRHITE_PARAMETER.Rule
                = COMA + EXPRESION + MORE_WRHITE_PARAMETER
                | Empty
                ;

            EXIT.Rule = RESERV_EXIT + PAR_IZQ + EXPRESION + PAR_DER + PUNTO_COMA;

            #endregion


            #region EXPRESION

            EXPLOGICA.Rule
                = EXPRELACIONAL + EXPLOGICA_PRIMA
                | NOT + EXPRELACIONAL + EXPLOGICA_PRIMA;

            EXPLOGICA_PRIMA.Rule
                = AND + EXPRELACIONAL + EXPLOGICA_PRIMA
                | OR + EXPRELACIONAL + EXPLOGICA_PRIMA
                | Empty
                ;



            EXPRELACIONAL.Rule = EXPRESION + EXPRELACIONAL_PRIMA;

            EXPRELACIONAL_PRIMA.Rule
                = LESS + EXPRESION + EXPRELACIONAL_PRIMA
                | HIGHER + EXPRESION + EXPRELACIONAL_PRIMA
                | LESS_EQUAL + EXPRESION + EXPRELACIONAL_PRIMA
                | HIGHER_EQUAL + EXPRESION + EXPRELACIONAL_PRIMA
                | EQUALS + EXPRESION + EXPRELACIONAL_PRIMA
                | DISCTINCT + EXPRESION + EXPRELACIONAL_PRIMA
                | Empty
                ;



            EXPRESION.Rule = TERMINO + EXPRESION_PRIMA;

            EXPRESION_PRIMA.Rule
                = PLUS + TERMINO + EXPRESION_PRIMA
                | MIN + TERMINO + EXPRESION_PRIMA
                | Empty
                ;


            TERMINO.Rule = FACTOR + TERMINO_PRIMA;

            TERMINO_PRIMA.Rule
                = POR + FACTOR + TERMINO_PRIMA
                | DIVI + FACTOR + TERMINO_PRIMA
                | MODULE + FACTOR + TERMINO_PRIMA
                | Empty
                ;

            FACTOR.Rule
                = PAR_IZQ + EXPLOGICA + PAR_DER
                | REAL
                | CADENA
                | NUMERO
                | IDENTIFIER + ID_TIPE
                | RESERV_TRUE
                | RESERV_FALSE
                | CALL_FUNCTION_PROCEDURE
                | MIN + FACTOR
                ;

            ID_TIPE.Rule
                = COR_IZQ + EXPLOGICA + COR_DER
                | Empty;

            /*EXPRESION.Rule
                = EXPRESION + PLUS + EXPRESION
                | EXPRESION + MIN + EXPRESION
                | EXPRESION + POR + EXPRESION
                | EXPRESION + DIVI + EXPRESION
                | EXPRESION + MODULE + EXPRESION
                | EXPRESION + LESS + EXPRESION
                | EXPRESION + HIGHER + EXPRESION
                | EXPRESION + LESS_EQUAL + EXPRESION
                | EXPRESION + HIGHER_EQUAL + EXPRESION
                | EXPRESION + EQUALS + EXPRESION
                | EXPRESION + DISCTINCT + EXPRESION
                | EXPRESION + AND + EXPRESION
                | EXPRESION + OR + EXPRESION
                | NOT + EXPRESION
                | CALL_FUNCTION_PROCEDURE
                | IDENTIFIER
                | NUMERO
                | CADENA
                | REAL
                | RESERV_TRUE
                | RESERV_FALSE
                | PAR_IZQ + EXPRESION + PAR_DER
                ;*/
            #endregion


            #region Preferencias
            this.Root = init;
            #endregion

        }
    }
}
