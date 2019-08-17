
using _OLC2_CQL_desktop.Arbol;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.DDL
{
    class CreateTable : IInstruccion
    {
        readonly bool notExistsValidation;
        readonly string nombre;
        readonly LinkedList<Columna> columnas;

        public CreateTable(string nombre, LinkedList<Columna> columnas)
        {
            this.nombre = nombre;
            this.columnas = columnas;
        }

        public CreateTable(bool notExistsValidation, string nombre, LinkedList<Columna> columnas)
        {
            this.notExistsValidation = notExistsValidation;
            this.nombre = nombre;
            this.columnas = columnas;
        }

        public void Ejecutar(Entorno e)
        {
            Tabla tabla = e.ObtenerTabla(nombre);
            if (!notExistsValidation)
            {
                if (tabla != null)
                {
                    Console.WriteLine("Error!! Ya existe una tabla con el nombre: " + nombre);
                    return;
                }
            }
            if (tabla != null) return;
            //Si la tabla no existe
            tabla = new Tabla(nombre, columnas);
            e.InsertarTabla(tabla);
        }
    }
}
