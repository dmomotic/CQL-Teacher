﻿
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.DDL
{
    class Columna
    {
        public string nombre;
        public Tipos tipo;
        public bool primaryKey;
        //Variable auxiliar utilizada para declarar un conjunto de columnas como PrimaryKey
        public LinkedList<string> ids_columnas_pk;
        //Variable utilizada para las columnas de tipo objeto
        public string idStruct;

        public Columna(LinkedList<string> ids_columnas_pk)
        {
            this.ids_columnas_pk = ids_columnas_pk;
        }

        public Columna(string nombre, Tipos tipo)
        {
            this.nombre = nombre;
            this.tipo = tipo;
        }

        public Columna(string nombre, Tipos tipo, bool primaryKey)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.primaryKey = primaryKey;
        }

        public Columna(string nombre, string idStruct)
        {
            this.nombre = nombre;
            this.idStruct = idStruct;
            this.tipo = Tipos.OBJETO;
        }

    }
}
