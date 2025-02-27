﻿namespace _OLC2_CQL_desktop.Arbol
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

        public bool IsStruct()
        {
            return tipo.Equals(Tipos.STRUCT);
        }

        public bool IsObject()
        {
            return tipo.Equals(Tipos.OBJETO);
        }

        public bool IsTabla()
        {
            return tipo.Equals(Tipos.TABLA);
        }

        public bool IsMap()
        {
            return tipo.Equals(Tipos.MAP);
        }

        public bool IsList()
        {
            return tipo.Equals(Tipos.LIST);
        }
    }
}
