
using _OLC2_CQL_desktop.Arbol;

namespace _OLC2_CQL_desktop.Structs
{
    class DeleteType : IInstruccion
    {
        readonly string idStruct;

        public DeleteType(string idStruct)
        {
            this.idStruct = idStruct;
        }

        public void Ejecutar(Entorno e)
        {
            e.DeleteStruct(idStruct);
        }
    }
}
