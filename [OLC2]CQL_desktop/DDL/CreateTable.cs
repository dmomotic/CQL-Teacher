
using _OLC2_CQL_desktop.Arbol;

namespace _OLC2_CQL_desktop.DDL
{
    class CreateTable : IInstruccion
    {
        readonly bool ifNotExistsValidation;
        readonly string nombre;

        public void Ejecutar(Entorno e)
        {
            throw new System.NotImplementedException();
        }
    }
}
