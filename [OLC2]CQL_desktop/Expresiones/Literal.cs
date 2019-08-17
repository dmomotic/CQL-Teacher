using _OLC2_CQL_desktop.Arbol;
using System;

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
            else if(valor is DateTime)
            {
                if(((DateTime)valor).TimeOfDay == Convert.ToDateTime("00:00:00").TimeOfDay)
                {
                    return Tipos.DATE;
                }
                else
                {
                    return Tipos.TIME;
                }
            }
            else
            {
                return Tipos.NULL;
            }
        }

        public object GetValor(Entorno e)
        {
            if(valor is DateTime)
            {
                if (((DateTime)valor).TimeOfDay == Convert.ToDateTime("00:00:00").TimeOfDay)
                    return ((DateTime)valor).ToShortDateString();
                else
                    return ((DateTime)valor).TimeOfDay;
            }
            return valor;
        }
    }
}
