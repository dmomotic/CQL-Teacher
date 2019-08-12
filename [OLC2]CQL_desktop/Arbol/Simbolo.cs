using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC2_CQL_desktop.Arbol
{
    class Simbolo
    {
        public string identificador;
        public Tipos tipo;
        public object valor;

        public Simbolo(string identificador, Tipos tipo, object valor)
        {
            this.identificador = identificador;
            this.tipo = tipo;
            this.valor = valor;
        }

        public Simbolo(string identificador, Tipos tipo)
        {
            this.identificador = identificador;
            this.tipo = tipo;
        }

    }
}
