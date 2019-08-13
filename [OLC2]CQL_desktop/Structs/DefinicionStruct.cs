using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Instrucciones;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class DefinicionStruct : IInstruccion
    {

        readonly string id;
        readonly LinkedList<IInstruccion> declaraciones;
        readonly bool notExistsValidation;


        public DefinicionStruct(string id, LinkedList<IInstruccion> declaraciones)
        {
            this.id = id;
            this.declaraciones = declaraciones;
        }

        public DefinicionStruct(string id, bool notExistsValidation, LinkedList<IInstruccion> declaraciones)
        {
            this.id = id;
            this.notExistsValidation = notExistsValidation;
            this.declaraciones = declaraciones;
        }

        public void Ejecutar(Entorno e)
        {
            if (e.TieneAlStruct(id))
            {
                if (!notExistsValidation)
                {
                    Console.WriteLine("Error!! se esta intentando declarar un tipo de usuario que ya existe en la tabla de simbolos");
                }
                return;
            }
            //Si no esta declarado el struct
            e.InsertarStruct(new Struct(id,declaraciones));
        }
    }
}
