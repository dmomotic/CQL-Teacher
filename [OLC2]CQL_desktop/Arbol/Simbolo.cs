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

        public bool IsStruct()
        {
            return tipo.Equals(Tipos.STRUCT);
        }
    }
}
