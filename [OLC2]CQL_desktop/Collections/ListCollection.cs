using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Structs;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Collections
{
    class ListCollection : Simbolo
    {
        public Tipos tipoLista;
        public List<object> valores;

        public ListCollection(string identificador, Tipos tipoLista) : base (identificador, Tipos.LIST)
        {
            this.tipoLista = tipoLista;
            this.valores = new List<object>();
        }

        public ListCollection(string identificador) : base (identificador, Tipos.LIST)
        {

        }

        public void Insert(object valor)
        {
            valores.Add(valor);
        }

        public object Get(int posicion)
        {
            return valores[posicion];
        }

        public void Set(int posicion, object valor)
        {
            valores[posicion] = valor;
        }

        public void Remove(int posicion)
        {
            valores.RemoveAt(posicion);
        }

        public int Size()
        {
            return valores.Count;
        }

        public void Clear()
        {
            valores.Clear();
        }

        public bool Contains(object valor)
        {
            //Si son los atributos de un objeto
            if(valor is Entorno atributos)
            {
                foreach(object val in valores)
                {
                    if(val is Objeto objeto)
                    {
                        if (objeto.atributos.Equals(atributos)) return true;
                    }
                }
                return false;
            }
            //Si es un primitivo
            return valores.Contains(valor);
        }

        public override string ToString()
        {
            string salida = "[ ";
            foreach(object val in valores)
            {
                salida += val + " ";
            }
            salida += "]";
            return salida;
        }

    }
}
