using _OLC2_CQL_desktop.Arbol;

namespace _OLC2_CQL_desktop.Expresiones
{
    class Literal : IExpresion
    {

        public object valor;

        public Literal(object valor)
        {
            this.valor = valor;
        }

        public Tipos GetTipo(Entorno e)
        {
            if(valor is int)
            {
                return Tipos.INT;
            }
            else if(valor is double)
            {
                return Tipos.DOUBLE;
            }
            else if (valor is bool)
            {
                return Tipos.BOOLEAN;
            }
            else if(valor is string)
            {
                return Tipos.STRING;
            }
            else
            {
                return Tipos.NULL;
            }
        }

        public object GetValor(Entorno e)
        {
            return valor;
        }
    }
}
