
using _OLC2_CQL_desktop.Arbol;
using System;

namespace _OLC2_CQL_desktop.Expresiones
{
    class Casteo : IExpresion
    {
        readonly Tipos tipoPrincipal;
        readonly Tipos tipoaAsignar;
        readonly object valor;

        public Casteo(Tipos tipoPrincipal, Tipos tipoaAsignar, object valor)
        {
            this.tipoPrincipal = tipoPrincipal;
            this.tipoaAsignar = tipoaAsignar;
            this.valor = valor;
        }

        public Tipos GetTipo(Entorno e)
        {
            //Tipo principal INT
            if (tipoPrincipal.Equals(Tipos.INT))
            {
                //Tipo a asignar double
                if (tipoaAsignar.Equals(Tipos.DOUBLE))
                {
                    return Tipos.INT;
                }
            }
            //Tipo principal DOUBLE
            if (tipoPrincipal.Equals(Tipos.DOUBLE))
            {
                //Tipo a asinar int
                if (tipoaAsignar.Equals(Tipos.INT))
                {
                    return Tipos.DOUBLE;
                }
            }
            return Tipos.NULL;
        }

        public object GetValor(Entorno e)
        {
            Tipos tipoNuevo = GetTipo(e);
            if (tipoNuevo.Equals(Tipos.NULL))
            {
                Console.WriteLine("No se pudo operar el casta entre los tipos " + tipoPrincipal + " y " + tipoaAsignar);
                return null;
            }
            //Si es un cast valido
            if (valor == null)
            {
                Console.WriteLine("No se puede convertir un valor null a ningun otro tipo");
                return null;
            }
            if (tipoNuevo.Equals(Tipos.INT))
            {
                return Convert.ToInt32(valor);
            }
            if (tipoNuevo.Equals(Tipos.DOUBLE))
            {
                return Convert.ToDouble(valor);
            }
            return null;
        }
    }
}
