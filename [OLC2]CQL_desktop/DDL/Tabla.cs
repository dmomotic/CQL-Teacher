
using _OLC2_CQL_desktop.Arbol;
using System.Data;

namespace _OLC2_CQL_desktop.DDL
{
    class Tabla : Simbolo
    {

        public DataTable data;

        public Tabla(string nombre) : base(nombre, Tipos.TABLA)
        {
            data = new DataTable(nombre);
        }


    }
}
