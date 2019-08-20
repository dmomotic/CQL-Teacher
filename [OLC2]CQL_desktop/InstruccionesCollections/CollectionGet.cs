
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.InstruccionesCollections
{
    class CollectionGet : IExpresion
    {

        readonly string id;
        readonly LinkedList<IExpresion> valor;

        public CollectionGet(string id, LinkedList<IExpresion> valor)
        {
            this.id = id;
            this.valor = valor;
        }

        public Tipos GetTipo(Entorno e)
        {
            return valor.First.Value.GetTipo(e);
        }

        public object GetValor(Entorno e)
        {
            Simbolo encontrado = e.Obtener(id);
            if (encontrado == null)
            {
                Console.WriteLine("Error, no se encontro ninguna collection con el id: " + id + " para realizar el get");
                return null;
            }
            //Si se encontro el simbolo y es una collection
            if(encontrado is MapCollection map)
            {
                object clave = valor.First.Value.GetValor(e);
                object res = map.Get(clave);
                if (res == null)
                {
                    Console.WriteLine("El map " + id + " no tiene ninguna clave " + clave);
                    return null;
                }
                //Si se obtuvo el valor del map
                return res;
            }
            return null;
        }
    }
}
