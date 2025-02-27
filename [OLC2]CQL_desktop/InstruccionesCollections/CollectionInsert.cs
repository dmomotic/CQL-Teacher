﻿
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.InstruccionesCollections
{
    class CollectionInsert : IInstruccion
    {

        readonly string id;
        readonly LinkedList<IExpresion> valores;

        public CollectionInsert(string id, LinkedList<IExpresion> valores)
        {
            this.id = id;
            this.valores = valores;
        }

        public void Ejecutar(Entorno e)
        {
            Simbolo encontrado = e.Obtener(id);
            if (encontrado == null)
            {
                Console.WriteLine("No se encontro la collection: " + id + " para poder realizar la insercion");
                return;
            }
            //Si es un map
            if(encontrado is MapCollection map)
            {
                //La lista solo puede tener dos valores: clave y valor
                if(valores.Count != 2)
                {
                    Console.WriteLine("Error, para insertar en el map: " + id + " solo se requiere una clave y un valor");
                    return;
                }
                Object clave = valores.First.Value.GetValor(e);
                if (map.TieneLaClave(clave))
                {
                    Console.WriteLine("No se puede insertar al map: " + map.identificador + " con la clave: " + clave + " porque ya tiene un valor ingresado con la misma clave");
                    return;
                }
                Object valor = valores.Last.Value.GetValor(e);
                Tipos tipoValor = valores.Last.Value.GetTipo(e);
                map.Insertar(clave,valor,tipoValor);
            }
            //Si es una List
            else if(encontrado is ListCollection list)
            {
                //La lista solo puede tener un parametro
                if (valores.Count != 1)
                {
                    Console.WriteLine("Error, para insertar en la list " + id + " solo se requiere un valor como parametro");
                    return;
                }
                object valor = valores.First.Value.GetValor(e);
                Tipos tipo = valores.First.Value.GetTipo(e);
                //Si es un objeto
                if(valor is Entorno atributos && tipo.Equals(Tipos.OBJETO))
                {
                    list.Insert(new Objeto("",atributos));
                }
                //Si es un primitivo
                else
                {
                    list.Insert(valor);
                }
            }
            //Si es un Set
            else if(encontrado is SetCollection set)
            {
                //El set solo puede tener un parametro
                if (valores.Count != 1)
                {
                    Console.WriteLine("Error, para insertar en el set " + id + " solo se requiere un valor como parametro");
                    return;
                }
                object valor = valores.First.Value.GetValor(e);
                Tipos tipo = valores.First.Value.GetTipo(e);
                //Si es un objeto
                if(valor is Entorno atributos && tipo.Equals(Tipos.OBJETO))
                {
                    //Inserto solo si no se encuentra ya en la lista
                    if (set.Contains(atributos))
                    {
                        Console.WriteLine("Error no se pueden insertar datos duplicados al set " + id);
                        return;
                    }
                    set.Insert(new Objeto("", atributos));
                }
                //Si es un primitivo
                else
                {
                    //Inserto solo si no se encuentra ya en la lista
                    if (set.Contains(valor))
                    {
                        Console.WriteLine("Error no se pueden insertar datos duplicados al set " + id);
                        return;
                    }
                    set.Insert(valor);
                }
            }
        }
    }
}
