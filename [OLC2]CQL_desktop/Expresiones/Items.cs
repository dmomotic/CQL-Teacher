using _OLC2_CQL_desktop.Arbol;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Expresiones
{
    //Clase utilizada para el manejo de items del tipo [item1,item2...]
    class Items : IExpresion
    {

        public Tipos tipoItems;
        readonly LinkedList<IExpresion> valores;

        public Items(LinkedList<IExpresion> valores)
        {
            this.valores = valores;
        }

        public Tipos GetTipo(Entorno e)
        {
            return Tipos.LIST;
        }

        public object GetValor(Entorno e)
        {
            List<object> list = new List<object>();
            foreach(IExpresion valor in valores)
            {
                list.Add(valor.GetValor(e));
            }
            return list;
        }
    }
}
