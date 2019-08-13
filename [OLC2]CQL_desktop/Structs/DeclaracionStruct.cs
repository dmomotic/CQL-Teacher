using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Instrucciones;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class DeclaracionStruct : IInstruccion
    {

        readonly string idStructGenerador;
        readonly LinkedList<string> identificadores;

        public DeclaracionStruct(string idStructGenerador, LinkedList<string> identificadores)
        {
            this.idStructGenerador = idStructGenerador;
            this.identificadores = identificadores;
        }

        public void Ejecutar(Entorno e)
        {
            Struct s = e.ObtenerStruct(idStructGenerador);
            if (s == null)
            {
                Console.WriteLine("No existe el struct " + idStructGenerador);
                return;
            }
            //Si se encontro el struct
            foreach(String identificador in identificadores)
            {
                //Si ya existe un identificador con el mismo nombre
                if (e.Existe(identificador))
                {
                    Console.WriteLine("Error ya existe un identificador con el nombre: " + identificador);
                    continue;
                }
                //Si no existe el identificador
                Entorno atributos = new Entorno();
                foreach(Declaracion declaracion in s.declaraciones)
                {
                    declaracion.Ejecutar(atributos);
                }
                e.InsertarObjeto(identificador, new Objeto(idStructGenerador, atributos));
            }
        }
    }
}
