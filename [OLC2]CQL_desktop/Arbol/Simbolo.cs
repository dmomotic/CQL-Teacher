using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC2_CQL_desktop.Arbol
{
    class Simbolo
    {
        public string Identificador;
        public Tipos Tipo;
        public object Valor;

        public Simbolo(string identificador, Tipos tipo, object valor)
        {
            Identificador = identificador;
            Tipo = tipo;
            Valor = valor;
        }

        public Simbolo(string identificador, Tipos tipo)
        {
            Identificador = identificador;
            Tipo = tipo;
        }

    }
}
