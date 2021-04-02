using CompiPascalC3D.Analizer.Languaje.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Languaje.Symbols
{
    class TablaTipo
    {
        public DataType[,] aritmeticos = new DataType[10, 10] {
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.INTEGER, DataType.STRING,    DataType.ERROR,     DataType.REAL,  DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.STRING,  DataType.STRING,    DataType.STRING,    DataType.STRING,DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.STRING,    DataType.BOOLEAN,   DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.REAL,    DataType.STRING,    DataType.ERROR,     DataType.REAL,  DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR}
        };


        //OPERACIONES LOGICAS
        public DataType[,] relacionales = new DataType[10, 10] {
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.INTEGER, DataType.ERROR,     DataType.ERROR,     DataType.REAL,  DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.REAL,    DataType.ERROR,     DataType.ERROR,     DataType.REAL,  DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR},
            { DataType.ERROR, DataType.ERROR,   DataType.ERROR,     DataType.ERROR,     DataType.ERROR, DataType.ERROR,  DataType.ERROR,  DataType.ERROR, DataType.ERROR, DataType.ERROR}
        };





        public DataType getTipoRel(DataType izquierda, DataType derecha)
        {

            var iz = (int)izquierda;
            var der = (int)derecha;
            var a = relacionales[iz, der];
            return a;
        }

        public DataType getTipoArith(DataType izquierda, DataType derecha)
        {

            var iz = (int)izquierda;
            var der = (int)derecha;
            var a = aritmeticos[iz, der];
            return a;
        }
    }
}
