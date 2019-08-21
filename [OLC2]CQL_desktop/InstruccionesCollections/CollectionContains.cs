using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.InstruccionesCollections
{
    class CollectionContains : IExpresion
    {

        readonly string id;
        readonly LinkedList<IExpresion> valores;

        public CollectionContains(string id, LinkedList<IExpresion> valores)
        {
            this.id = id;
            this.valores = valores;
        }

        public Tipos GetTipo(Entorno e)
        {
            return Tipos.BOOLEAN;
        }

        public object GetValor(Entorno e)
        {
            Simbolo encontrado = e.Obtener(id);
            if (encontrado == null)
            {
                Console.WriteLine("No se puede realizar la operacion contains porque no existe la collection con id " + id);
                return null;
            }
            //Si es un map
            if(encontrado is MapCollection map)
            {
                object clave = valores.First.Value.GetValor(e);
                return map.Contains(clave);
            }
            return null;
        }
    }
}
