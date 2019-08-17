
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.DML;
using System;
using System.Collections.Generic;
using System.Data;

namespace _OLC2_CQL_desktop.DDL
{
    class Tabla : Simbolo
    {

        public DataTable data;
        public LinkedList<Columna> columnas;

        public Tabla(string nombre, LinkedList<Columna> columnas) : base(nombre, Tipos.TABLA)
        {
            data = new DataTable(nombre);
            this.columnas = columnas;
            CrearColumnas();
        }

        private void CrearColumnas()
        {
            foreach(Columna col in columnas)
            {
                DataColumn dtc = new DataColumn
                {
                    ColumnName = col.nombre,
                    DataType = ObtenerTipo(col),
                    ReadOnly = false
                };
                data.Columns.Add(dtc);
            }
        }

        public void SetPrimaryKey()
        {

        }

        private Type ObtenerTipo(Columna columna)
        {
            if (columna.tipo.Equals(Tipos.INT))
            {
                return typeof(int);
            }
            if (columna.tipo.Equals(Tipos.DOUBLE))
            {
                return typeof(double);
            }
            if (columna.tipo.Equals(Tipos.BOOLEAN))
            {
                return typeof(bool);
            }
            if (columna.tipo.Equals(Tipos.STRING))
            {
                return typeof(string);
            }
            return typeof(Nullable);
        }

        public void Insert(LinkedList<Celda> celdas)
        {
            DataRow myDataRow = data.NewRow();
            foreach(Celda celda in celdas)
            {
                myDataRow[celda.id] = celda.valor;
            }
            data.Rows.Add(myDataRow);
        }

        public DataRow[] Select()
        {
            return data.Select();
        }
    }
}
