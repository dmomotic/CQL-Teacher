using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Structs;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Collections
{
    class SetCollection : Simbolo
    {
        public Tipos tipoLista;
        public List<object> valores;

        public SetCollection(string identificador, Tipos tipoLista) : base(identificador, Tipos.SET)
        {
            this.tipoLista = tipoLista;
            this.valores = new List<object>();
        }

        public SetCollection(string identificador) : base (identificador, Tipos.SET)
        {

        }

        public void Insert(object valor)
        {
            valores.Add(valor);
            if (!tipoLista.Equals(Tipos.OBJETO)) valores.Sort();
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
            //Si es un objeto
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
            string salida = "{ ";
            foreach(object o in valores)
            {
                salida += o + " ";
            }
            salida += "}";
            return salida;
        }
    }
}
