using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Clases;
using _OLC2_CQL_desktop.Structs;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Expresiones
{
    //Clase utilizada para retornar una nueva instancia de un objeto a partir de {"atributo":valor}
    class InstanciaObjecto : IExpresion
    {
        public LinkedList<ClaveValor> atributos;

        public InstanciaObjecto(LinkedList<ClaveValor> atributos)
        {
            this.atributos = atributos;
        }

        public Tipos GetTipo(Entorno e)
        {
            return Tipos.OBJETO;
        }

        public object GetValor(Entorno e)
        {
            Entorno atributosObjeto = new Entorno();
            foreach(ClaveValor cv in atributos)
            {
                object clave = cv.clave.GetValor(e);
                object valor = cv.valor.GetValor(e);
                Tipos tipo = cv.valor.GetTipo(e);
                atributosObjeto.Insertar(new Simbolo(clave.ToString(),tipo,valor));
            }
            return new Objeto("", atributosObjeto);
        }
    }
}
