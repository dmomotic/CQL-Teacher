using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Clases;
using _OLC2_CQL_desktop.DDL;
using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace _OLC2_CQL_desktop.DML
{
    class Select : IInstruccion
    {

        readonly LinkedList<AccesoColumna> columnas;
        readonly string nombreTabla;

        public Select(string nombreTabla)
        {
            this.nombreTabla = nombreTabla;
        }

        public Select(string nombreTabla, LinkedList<AccesoColumna> columnas)
        {
            this.nombreTabla = nombreTabla;
            this.columnas = columnas;
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
            if (columnas == null)
            {
                //Si es un select *
                foreach(DataRow row in tabla.Select())
                {
                    foreach(DataColumn column in tabla.data.Columns)
                    {
                        Console.Write(row[column] + " ");
                    }
                    Console.WriteLine("");
                }
                return;
            }
            //Si es un select con columnas en especifico
            string[] selectedColumns = columnas.Select(col => col.nombre).ToArray();
            DataTable dt = new DataView(tabla.data).ToTable(false, selectedColumns);
            foreach(DataRow row in dt.Select())
            {
                foreach(DataColumn column in dt.Columns)
                {
                    //Si es un acceso a un atributo de objeto
                    if(row[column] is Objeto objeto)
                    {
                        var query = columnas.Where(col => col.nombre.Equals(column.ColumnName)).FirstOrDefault();
                        if (query != null)
                        {
                            Simbolo aux = null;
                            foreach(String atributo in query.atributos)
                            {
                                aux = objeto.GetAtributo(atributo);
                                if (aux.IsObject())
                                {
                                    objeto = (Objeto)aux;
                                }
                            }
                            Console.Write(aux.valor + " ");
                        }
                    }
                    //Si es un atributo directo de la tabla
                    else
                    {
                        Console.Write(row[column] + " ");
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
