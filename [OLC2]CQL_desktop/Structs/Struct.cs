using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Instrucciones;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class Struct : Simbolo
    {
        public LinkedList<IInstruccion> declaraciones;

        public Struct(string identificador, LinkedList<IInstruccion> declaraciones) : base(identificador, Tipos.STRUCT)
        {
            this.declaraciones = declaraciones;
        }

        public int GetNumeroDeAtributos()
        {
            return declaraciones.Count;
        }

    }
}
