using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Clases;

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


    }
}
