using _OLC2_CQL_desktop.Arbol;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class AsignacionAtributo : IInstruccion
    {

        readonly string id;
        readonly LinkedList<string> atributos;
        readonly IExpresion valor;

        public AsignacionAtributo(string id, LinkedList<string> atributos, IExpresion valor)
        {
            this.id = id;
            this.atributos = atributos;
            this.valor = valor;
        }

        public void Ejecutar(Entorno e)
        {
            Simbolo s = e.Obtener(id);
            if (s == null)
            {
                Console.WriteLine("No existe el identificador " + id + " en este entorno");
                return;
            }
            //Si existe el simbolo
            if (!s.IsObject())
            {
                Console.WriteLine("No se puede acceder a los atributos de " + id + " porque no es un objeto");
                return;
            }
            //Si es un objeto
            Objeto o = (Objeto)s;
            Simbolo aux = null;
            foreach(string atributo in atributos)
            {
                aux = o.GetAtributo(atributo);
                if(aux == null)
                {
                    Console.WriteLine("No se puede acceder al atributo " + atributo);
                    return;
                }
                //Si encontro el atributo verifico si es un objeto para actualizar mi objecto de acceso
                if (aux.IsObject())
                {
                    o = (Objeto)aux;
                }
            }
            //Una vez tengo el simbolo solicitado, actualizo su valor

            /******************************************
                AQUI ME FALTA LA COMPROBACION DE TIPOS
             *******************************************/

            Object val = valor.GetValor(e);
            if (aux != null)
            {
                aux.valor = val;
            }
        }
    }
}
