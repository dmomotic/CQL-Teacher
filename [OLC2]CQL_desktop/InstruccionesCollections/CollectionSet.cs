using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.InstruccionesCollections
{
    class CollectionSet : IInstruccion
    {

        readonly string id;
        readonly LinkedList<IExpresion> valores;

        public CollectionSet(string id, LinkedList<IExpresion> valores)
        {
            this.id = id;
            this.valores = valores;
        }

        public void Ejecutar(Entorno e)
        {
            Simbolo encontrado = e.Obtener(id);
            if(encontrado == null)
            {
                Console.WriteLine("No se puede realizar la operacion set porque no se encontro el collection con id " + id);
                return;
            }
            //Si es un map
            if(encontrado is MapCollection map)
            {
                if (valores.Count != 2)
                {
                    Console.WriteLine("Para realizar la operacion set sobre el map " + id + " solo se necesita una llave y un valor");
                    return;
                }
                object clave = valores.First.Value.GetValor(e);
                if (!map.TieneLaClave(clave))
                {
                    Console.WriteLine("No se puede realizar la operacion set porque el map " + id + " no tiene ninguna clave " + clave);
                    return;
                }
                object valor = valores.Last.Value.GetValor(e);
                map.Set(clave,valor);
            }
            //Si es una list
            else if(encontrado is ListCollection list)
            {
                if (valores.Count != 2)
                {
                    Console.WriteLine("Para realizar la operacion set sobre la list " + id + " solo se necesita la posicion y el valor");
                    return;
                }
                object posicion = valores.First.Value.GetValor(e);
                object valor = valores.Last.Value.GetValor(e);
                //Si es un objeto
                if(valor is Entorno atributos && valores.Last.Value.GetTipo(e).Equals(Tipos.OBJETO))
                {
                    list.Set(Convert.ToInt32(posicion), new Objeto("", atributos));
                }
                //Si es un primitivo
                else
                {
                    list.Set(Convert.ToInt32(posicion), valor);
                }
            }
        }
    }
}
