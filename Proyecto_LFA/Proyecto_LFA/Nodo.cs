using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    class Nodo
    {
        //Estrutura normal
        public string caracter;
        public Nodo izq;
        public Nodo der;

        //Manejo primera tabla
        public bool N = false;
        public string First = "";
        public string Last = "";
    }
}
