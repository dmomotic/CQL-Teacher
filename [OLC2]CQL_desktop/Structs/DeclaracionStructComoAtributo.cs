
using _OLC2_CQL_desktop.Arbol;
using System;

namespace _OLC2_CQL_desktop.Structs
{
    class DeclaracionStructComoAtributo : IInstruccion
    {

        public string idStructGenerador;
        public string id;
        public Entorno auxiliar;

        public DeclaracionStructComoAtributo(string id, string idStructGenerador)
        {
            this.id = id;
            this.idStructGenerador = idStructGenerador;
        }

        public void Ejecutar(Entorno e)
        {
            Struct s = auxiliar.ObtenerStruct(idStructGenerador);
            if (s == null)
            {
                Console.WriteLine("No existe el struct " + idStructGenerador);
                return;
            }
            //Si se encontro el struct

            //Si ya existe un identificador con el mismo nombre
            if (e.Existe(id))
            {
                Console.WriteLine("Error ya existe un identificador con el nombre: " + id);
                return;
            }
            //Si no existe el identificador
            Entorno atributos = new Entorno();
            foreach (IInstruccion declaracion in s.declaraciones)
            {
                if (declaracion is DeclaracionStructComoAtributo)
                {
                    DeclaracionStructComoAtributo dec = (DeclaracionStructComoAtributo)declaracion;
                    dec.auxiliar = auxiliar;
                    dec.Ejecutar(atributos);
                }
                else
                {
                    declaracion.Ejecutar(atributos);
                }
            }
            e.InsertarObjeto(id, new Objeto(idStructGenerador, atributos));
        }
    }
}
