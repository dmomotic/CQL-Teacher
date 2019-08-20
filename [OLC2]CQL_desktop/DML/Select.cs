using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.DDL;
using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections.Generic;
using System.Data;

namespace _OLC2_CQL_desktop.DML
{
    class Select : IInstruccion
    {

        readonly LinkedList<string> ids;
        readonly string nombreTabla;

        public Select(string nombreTabla)
        {
            this.nombreTabla = nombreTabla;
        }

        public void Ejecutar(Entorno e)
        {
            Tabla tabla = e.ObtenerTabla(nombreTabla);
            if(tabla == null)
            {
                Console.WriteLine("No se efectuo la instruccion select porque no se encontro la tabla " + nombreTabla);
                return;
            }
            //Si la tabla existe
            if (ids == null)
            {
                foreach(DataRow row in tabla.Select())
                {
                    foreach(DataColumn column in tabla.data.Columns)
                    {
                        Console.Write(row[column] + " ");
                    }
                    Console.WriteLine("");
                }
            }
        }
    }
}
