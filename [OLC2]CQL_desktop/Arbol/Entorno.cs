using System.Collections;

namespace _OLC2_CQL_desktop.Arbol
{
    class Entorno : Hashtable
    {
        private readonly Entorno Padre;

        public Entorno() : base ()
        {
            Padre = null;
        }

        public Entorno(Entorno padre) : base ()
        {
            Padre = padre;
        }

        public void Insertar(Simbolo simbolo)
        {
            this.Add(simbolo.Identificador,simbolo);
        }

        public Simbolo Obtener(string id)
        {
            for(Entorno e = this; e!=null; e = e.Padre)
            {
                Simbolo encontrado = (Simbolo)e[id];
                if (encontrado!=null)
                {
                    return encontrado;
                }
            }
            return null;
        }
    }
}
