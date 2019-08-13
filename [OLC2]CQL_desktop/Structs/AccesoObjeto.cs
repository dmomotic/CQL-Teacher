
using _OLC2_CQL_desktop.Arbol;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class AccesoObjeto : IExpresion
    {

        readonly string id;
        readonly LinkedList<string> atributos;

        public AccesoObjeto(string id, LinkedList<string> atributos)
        {
            this.id = id;
            this.atributos = atributos;
        }

        public Tipos GetTipo(Entorno e)
        {
            Simbolo simbolo = e.Obtener(id);
            if (simbolo != null)
            {
                if (simbolo.IsObject())
                {
                    Objeto objeto = (Objeto)simbolo;
                    Simbolo aux = null;
                    foreach (string atributo in atributos)
                    {
                        aux = objeto.GetAtributo(atributo);
                        if (aux == null)
                        {
                            return Tipos.NULL;
                        }
                        if (aux.IsObject())
                        {
                            objeto = (Objeto)aux;
                        }
                    }
                    if (aux == null)
                    {
                        return Tipos.NULL;
                    }
                    return aux.tipo;
                }
            }
            return Tipos.NULL;
        }

        public object GetValor(Entorno e)
        {
            Simbolo simbolo = e.Obtener(id);
            if (simbolo == null)
            {
                Console.WriteLine("No se encontro el identificador " + id + "en este entorno D:");
                return null;
            }

            //Si se encontro el simbolo
            if (!simbolo.IsObject())
            {
                Console.WriteLine("El identificador " + id + " no es un objeto");
                return null;
            }

            //Si es un objeto
            Objeto objeto = (Objeto)simbolo;
            Simbolo aux = null;
            foreach(string atributo in atributos)
            {
                aux = objeto.GetAtributo(atributo);
                if(aux == null)
                {
                    Console.WriteLine("El objeto " + id + " no contiene ningun atributo " + atributo);
                    return null;
                }
                if (aux.IsObject())
                {
                    objeto = (Objeto)aux;
                }
            }
            //Si nuestro auxiliar es null
            if (aux == null)
            {
                Console.WriteLine("Error al acceder a los atributos de " + id);
                return null;
            }

            return aux.valor;
        }
    }
}
