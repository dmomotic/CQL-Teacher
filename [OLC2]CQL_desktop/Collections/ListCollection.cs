using _OLC2_CQL_desktop.Arbol;
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
            return valores.Contains(valor);
        }
        
    }
}
