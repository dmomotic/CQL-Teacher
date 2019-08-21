using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using System;

namespace _OLC2_CQL_desktop.InstruccionesCollections
{
    class CollectionClear : IInstruccion
    {

        readonly string id;

        public CollectionClear(string id)
        {
            this.id = id;
        }

        public void Ejecutar(Entorno e)
        {
            Simbolo encontrado = e.Obtener(id);
            if(encontrado == null)
            {
                Console.WriteLine("No se puede realizar la operacion clear porque no se encontro el collection con id " + id);
                return;
            }
            //Si es un map
            if(encontrado is MapCollection map)
            {
                map.Clear();
            }
            //Si es una list
            else if(encontrado is ListCollection list)
            {
                list.Clear();
            }
        }
    }
}
