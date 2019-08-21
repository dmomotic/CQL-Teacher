using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using System;

namespace _OLC2_CQL_desktop.InstruccionesCollections
{
    class CollectionSize : IExpresion
    {

        readonly string id;

        public CollectionSize(string id)
        {
            this.id = id;
        }

        public Tipos GetTipo(Entorno e)
        {
            return Tipos.INT;
        }

        public object GetValor(Entorno e)
        {
            Simbolo encontrado = e.Obtener(id);
            if (encontrado == null)
            {
                Console.WriteLine("No se encontro ningun collection con id " + id + " para ejecutar el metodo size");
                return null;
            }
            //Si es un map
            if(encontrado is MapCollection map)
            {
                return map.Size();
            }
            return null;
        }
    }
}
