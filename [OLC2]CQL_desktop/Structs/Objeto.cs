using _OLC2_CQL_desktop.Arbol;

namespace _OLC2_CQL_desktop.Structs
{
    class Objeto : Simbolo
    {
        public string idStructGenerador;
        public Entorno atributos;

        public Objeto(string idStructGenerador, Entorno atributos) : base(idStructGenerador,Tipos.OBJETO)
        {
            this.idStructGenerador = idStructGenerador;
            this.atributos = atributos;
        }

        public bool TieneAtributo(string atributo)
        {
            return atributos.Existe(atributo);
        }

        public Simbolo GetAtributo(string atributo)
        {
            return atributos.Obtener(atributo);
        }

        public void SetAtributo(string atributo, object valor)
        {
            atributos[atributo] = valor;
        }
    }
}
