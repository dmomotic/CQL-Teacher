
using _OLC2_CQL_desktop.Arbol;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class AlterType : IInstruccion
    {
        readonly string idStruct;
        readonly LinkedList<IInstruccion> declaraciones;
        readonly bool isAdd;
        readonly LinkedList<string> atributos;


        public AlterType(string idStruct, LinkedList<IInstruccion> declaraciones)
        {
            this.idStruct = idStruct;
            this.declaraciones = declaraciones;
            this.isAdd = true;
        }

        public AlterType(string idStruct, LinkedList<string> atributos)
        {
            this.idStruct = idStruct;
            this.atributos = atributos;
            this.isAdd = false;
        }

        public void Ejecutar(Entorno e)
        {
            //Verificamos existencia del struct
            Struct s = e.ObtenerStruct(idStruct);
            if (s == null)
            {
                Console.WriteLine("No se puede encontrar el struct con id " + idStruct + " para realizar la modificacion");
                return;
            }
            //Si se encontró el struct

            //Si es una adicion de atributos
            if (isAdd)
            {
                foreach(IInstruccion declaracion in declaraciones)
                {
                    s.declaraciones.AddLast(declaracion);
                }
            }
            //Si es una eliminacion de atributos
            else
            {
                foreach(string atributo in atributos)
                {
                    s.EliminarAtributo(atributo);
                }
            }
        }
    }
}
