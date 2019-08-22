
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.FCL
{
    class Set : IInstruccion
    {
        readonly LinkedList<string> ids;
        public Tipos tipo;
        readonly LinkedList<IExpresion> valores;

        public Set(LinkedList<string> ids, Tipos tipo)
        {
            this.ids = ids;
            this.tipo = tipo;
        }

        public Set(LinkedList<string> ids, LinkedList<IExpresion> valores)
        {
            this.ids = ids;
            this.valores = valores;
            this.tipo = Tipos.NULL;
        }

        public void Ejecutar(Entorno e)
        {
            if (ids == null)
            {
                Console.WriteLine("Error, no existen identificadores validos para crear la collection Set");
                return;
            }
            SetCollection set = null;
            //Creo la lista
            foreach (string id in ids)
            {
                if (e.Obtener(id) != null)
                {
                    Console.WriteLine("No se puede declarar el Set con id " + id + " porque ya existe una variable con ese nombre en este entorno");
                    continue;
                }
                //Si traen un tipo
                if (!tipo.Equals(Tipos.NULL))
                {
                    set = new SetCollection(id, tipo);
                    e.InsertarSet(id, set);
                }
                //Si no se trae el tipo
                else
                {
                    if (valores != null)
                    {
                        tipo = valores.First.Value.GetTipo(e);
                        set = new SetCollection(id, tipo);
                        e.InsertarSet(id, set);
                    }
                }
            }
            if (valores == null) return;
            //Si trae los valores
            foreach (IExpresion expresion in valores)
            {
                object valor = expresion.GetValor(e);
                Tipos t = expresion.GetTipo(e);
                if (!t.Equals(tipo))
                {
                    Console.WriteLine("No se puede insertar un tipo " + t + " en una collection Set tipo " + tipo);
                    continue;
                }
                //Si es un objeto
                if (valor is Entorno atributos && t.Equals(Tipos.OBJETO))
                {
                    //Solo lo inserto si no esta en la lista
                    if (set.Contains(atributos))
                    {
                        Console.WriteLine("No se pueden insertar elementos duplicados en el set " + set.identificador);
                        return;
                    }
                    set.Insert(new Objeto("", atributos));
                }
                //Si es un primitivo
                else
                {
                    //Solo lo inserto si no esta en la lista
                    if (set.Contains(valor))
                    {
                        Console.WriteLine("No se pueden insertar elementos duplicados en el set " + set.identificador);
                        return;
                    }
                    set.Insert(valor);
                }
            }
        }
    }
}
