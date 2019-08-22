using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.FCL
{
    class List : IInstruccion
    {
        readonly LinkedList<string> ids;
        public Tipos tipo;
        readonly LinkedList<IExpresion> valores;

        public List(LinkedList<string> ids, Tipos tipo)
        {
            this.ids = ids;
            this.tipo = tipo;
        }

        public List(LinkedList<string> ids, LinkedList<IExpresion> valores)
        {
            this.ids = ids;
            this.valores = valores;
            this.tipo = Tipos.NULL;
        }

        public void Ejecutar(Entorno e)
        {
            if (ids == null)
            {
                Console.WriteLine("Error, no existen identificadores validos para crear la collection List");
                return;
            }
            ListCollection list = null;
            //Creo la lista
            foreach(string id in ids)
            {
                if(e.Obtener(id) != null)
                {
                    Console.WriteLine("No se puede declarar la List con id " + id + " porque ya existe una variable con ese nombre en este entorno");
                    continue;
                }
                //Si traen un tipo
                if (!tipo.Equals(Tipos.NULL))
                {
                    list = new ListCollection(id, tipo);
                    e.InsertarList(id, list);
                }
                //Si no se trae el tipo
                else
                {
                    if (valores != null)
                    {
                        tipo = valores.First.Value.GetTipo(e);
                        list = new ListCollection(id, tipo);
                        e.InsertarList(id,list);
                    }
                }
            }
            if (valores == null) return;
            //Si trae los valores
            foreach(IExpresion expresion in valores)
            {
                object valor = expresion.GetValor(e);
                Tipos t = expresion.GetTipo(e);
                if (!t.Equals(tipo))
                {
                    Console.WriteLine("No se puede insertar un tipo " + t + " en una lista tipo " + tipo);
                    continue;
                }
                //Si es un objeto
                if(valor is Entorno atributos && t.Equals(Tipos.OBJETO))
                {
                    list.Insert(new Objeto("",atributos));
                }
                //Si es un primitivo
                else
                {
                    list.Insert(valor);
                }
            }

        }
    }
}
