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

        public void EliminarAtributo(string atributo)
        {
            foreach(IInstruccion declaracion in declaraciones)
            {
                if(declaracion is Declaracion)
                {
                    Declaracion dec = ((Declaracion)declaracion);
                    if (dec.identificadores.Contains(atributo))
                    {
                        declaraciones.Remove(declaracion);
                        return;
                    }
                }
                if(declaracion is DeclaracionStructComoAtributo)
                {
                    DeclaracionStructComoAtributo dec = (DeclaracionStructComoAtributo)declaracion;
                    if (dec.id.Equals(atributo, System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        declaraciones.Remove(declaracion);
                        return;
                    }
                }
            }
        }

    }
}
