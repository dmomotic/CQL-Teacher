
using _OLC2_CQL_desktop.Arbol;

namespace _OLC2_CQL_desktop.Clases
{
    class ClaveValor
    {
        public IExpresion clave;
        public IExpresion valor;

        public ClaveValor(IExpresion clave, IExpresion valor)
        {
            this.clave = clave;
            this.valor = valor;
        }
    }
}
