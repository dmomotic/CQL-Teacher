
namespace _OLC2_CQL_desktop.DDL
{
    class Columna
    {
        public string nombre;
        public Tipos tipo;
        public bool primaryKey;

        public Columna(string nombre, Tipos tipo)
        {
            this.nombre = nombre;
            this.tipo = tipo;
        }

        public Columna(string nombre, Tipos tipo, bool primaryKey)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.primaryKey = primaryKey;
        }

    }
}
