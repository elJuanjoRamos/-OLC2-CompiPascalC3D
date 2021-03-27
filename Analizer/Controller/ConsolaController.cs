using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CompiPascalC3D.Analizer.Controller
{
    class ConsolaController
    {
        private readonly static ConsolaController _instance = new ConsolaController();
        private ArrayList salida = new ArrayList();


        private ConsolaController()
        {
        }

        public static ConsolaController Instance
        {
            get
            {
                return _instance;
            }
        }




        public void Add(string message)
        {
            salida.Add(message);
        }

        public void clean()
        {
            salida.Clear();
        }

        public string getText()
        {
            var a = "";
            foreach (var item in salida)
            {
                a = a + item;
            }

            return a;
        }
    }
}
