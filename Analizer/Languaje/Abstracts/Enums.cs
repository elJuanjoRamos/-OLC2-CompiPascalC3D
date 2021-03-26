using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Abstracts
{
    public enum DataType
    {
        ANY = 0,
        INTEGER = 1,
        STRING = 2,
        BOOLEAN = 3,
        REAL = 4,
        TYPE = 5,
        ARRAY = 6,
        IDENTIFIER = 7,
        ERROR = 8,
        CONST = 9
    }
    public enum OpRelational
    {
        EQUALS,
        DISCTINCT,
        LESS,
        LESS_EQUALS,
        HIGHER,
        HIGHER_EQUALS
    }

    public enum OpLogical
    {
        AND,
        OR,
        NOT
    }

    public enum OpArithmetic
    {
        SUM,
        SUBTRACTION,
        MULTIPLICATION,
        DIVISION,
        MODULE
    }
    public enum Transfer
    {
        BREAK,
        CONTINUE,
        RETURN,
        RETURNDATA
    }
    class Enums
    {
        public Enums()
        {

        }
    }
}
