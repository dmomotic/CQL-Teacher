using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Instrucciones;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class Struct : Simbolo
    {
        public LinkedList<Declaracion> declaraciones;

        public Struct(string identificador, LinkedList<Declaracion> declaraciones) : base(identificador, Tipos.STRUCT)
        {
            this.declaraciones = declaraciones;
        }


    }
}
