using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.InstruccionesCollections
{
    class CollectionRemove : IInstruccion
    {
        readonly string id;
        readonly LinkedList<IExpresion> valores;

        public CollectionRemove(string id, LinkedList<IExpresion> valores)
        {
            this.id = id;
            this.valores = valores;
        }

        public void Ejecutar(Entorno e)
        {
            Simbolo encontrado = e.Obtener(id);
            if(encontrado == null)
            {
                Console.WriteLine("No se puede realizar la operacion remove porque no se encontro a la collection " + id);
                return;
            }
            //Si es un map
            if(encontrado is MapCollection map)
            {
                object clave = valores.First.Value.GetValor(e);
                if (!map.TieneLaClave(clave))
                {
                    Console.WriteLine("No se efectuo la operacion remove del map " + id + " porque no contenia la clave " + clave);
                    return;
                }
                map.Remove(clave);
            }
        }
    }
}
