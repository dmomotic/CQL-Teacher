
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.Structs;
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
            Simbolo encontrado = e.Obtener(id);
            if (encontrado is MapCollection map) return map.tipoValor;
            if (encontrado is ListCollection list) return list.tipoLista;
            return Tipos.NULL;
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
            //Si es una list
            else if(encontrado is ListCollection list)
            {
                object posicion = valor.First.Value.GetValor(e);
                object res = list.Get(Convert.ToInt32(posicion));
                if(res == null)
                {
                    Console.WriteLine("La list " + id + " no tiene ningun valor en la posicion " + posicion);
                    return null;
                }
                //Si es un objeto
                if(res is Objeto objeto)
                {
                    return objeto.atributos;
                }
                //Si es un primitivo
                return res;
            }
            return null;
        }
    }
}
