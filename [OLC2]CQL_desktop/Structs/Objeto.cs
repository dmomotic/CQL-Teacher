using _OLC2_CQL_desktop.Arbol;
using System.Collections;

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
            Simbolo s = (Simbolo)atributos[atributo];
            if (s!=null) s.valor = valor;
        }

        public int GetNumeroDeAtributos()
        {
            return atributos.Count;
        }

        public override string ToString()
        {
            string salida = "{ ";
            foreach(DictionaryEntry pair in atributos)
            {
                salida += "\""+pair.Key+"\": ";
                salida += pair.Value + " ";
            }
            salida += "}";
            return salida;
        }
    }
}
