using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Clases;
using System.Collections;

namespace _OLC2_CQL_desktop.Collections
{
    class MapCollection : Simbolo
    {

        public Entorno valores;
        public Tipos tipoClave;
        public Tipos tipoValor;

        public MapCollection(string identificador, Tipos tipoClave, Tipos tipoValor) : base(identificador, Tipos.MAP)
        {
            valores = new Entorno();
            this.tipoClave = tipoClave;
            this.tipoValor = tipoValor;
        }

        public MapCollection(string identificador) : base(identificador, Tipos.MAP)
        {

        }

        public void Insertar(object clave, object valor, Tipos tipoValor)
        {
            valores.Insertar(new Simbolo(clave.ToString(),tipoValor,valor));
        }

        public object Get(object clave)
        {
            if(valores[clave.ToString()] is Simbolo sim)
            {
                return sim.valor;
            }
            return null;
        }

        public bool TieneLaClave(object clave)
        {
            return valores[clave.ToString()] != null;
        }

        public void Set(object clave, object valor)
        {
            Simbolo s = valores.Obtener(clave.ToString());
            if(s!=null) s.valor = valor;
        }

        public void Remove(object clave)
        {
            valores.Remove(clave.ToString());
        }

        public int Size()
        {
            return valores.Count;
        }

        public void Clear()
        {
            valores.Clear();
        }

        public bool Contains(object clave)
        {
            return TieneLaClave(clave);
        }

        public override string ToString()
        {
            string salida = "{";
            foreach(DictionaryEntry pair in valores)
            {
                if (pair.Key is Simbolo sim) salida += sim.valor + ": ";
                else salida += pair.Key + ": ";

                if (pair.Value is Simbolo val) salida += val.valor + " ";
                else salida += pair.Value + " ";
            }
            salida += "}";
            return salida;
        }

    }
}
