
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Clases
{
    class AccesoColumna
    {
        public string nombre;
        public LinkedList<string> atributos;

        public AccesoColumna(string nombre, LinkedList<string> atributos)
        {
            this.nombre = nombre;
            this.atributos = atributos;
        }

        public AccesoColumna(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
