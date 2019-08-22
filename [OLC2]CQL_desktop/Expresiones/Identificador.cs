using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.Structs;
using System;

namespace _OLC2_CQL_desktop.Expresiones
{
    class Identificador : IExpresion
    {

        readonly string id;

        public Identificador(string id)
        {
            this.id = id;
        }

        public object GetValor(Entorno e)
        {
            Simbolo s = e.Obtener(id);
            if (s == null)
            {
                Console.WriteLine("No se puede encontrar el id " + id + " en este entorno");
                return null;
            }
            //Si se encontro el simbolo
            if(s is Objeto objeto)
            {
                return objeto.atributos;
            }
            if(s is ListCollection list)
            {
                return list.valores;
            }
            return s.valor;
        }

        public Tipos GetTipo(Entorno e)
        {
            Simbolo s = e.Obtener(id);
            if (s != null)
            {
                if (s is Objeto) return Tipos.OBJETO;
                if (s is ListCollection) return Tipos.LIST;

                object valor = s.valor;
                if (valor != null)
                {
                    if (valor is int) return Tipos.INT;
                    if (valor is double) return Tipos.DOUBLE;
                    if (valor is bool) return Tipos.DOUBLE;
                    if (valor is string) return Tipos.STRING;
                }
            }
            return Tipos.NULL;
        }
    }
}
