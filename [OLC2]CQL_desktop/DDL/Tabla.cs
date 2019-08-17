
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.DML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            SetPrimaryKey();
        }

        private void CrearColumnas()
        {
            foreach(Columna col in columnas)
            {
                DataColumn dtc = new DataColumn
                {
                    ColumnName = col.nombre,
                    DataType = ObtenerTipo(col),
                    ReadOnly = false,
                };
                //Si la columna es tipo COUNTER
                if (col.tipo.Equals(Tipos.COUNTER))
                {
                    //Solo se puede asignar counter a las llaves primarias
                    if (col.primaryKey)
                    {
                        dtc.AutoIncrement = true;
                        dtc.AutoIncrementSeed = 1;
                        dtc.AutoIncrementStep = 1;
                    }
                    else
                    {
                        Console.WriteLine("Error!! no se puede asignar un tipo de columna COUNTER al atributo " + col.nombre + " porque no es una llave primaria");
                    }
                }
                data.Columns.Add(dtc);
            }
        }

        public void SetPrimaryKey()
        {
            var items = columnas.Where(col => col.primaryKey==true);
            int cantidadDeLlavesPrimarias = items.Count();
            //Si no hay llaves primarias
            if (cantidadDeLlavesPrimarias <= 0) return;
            //Si hay llaves primarias
            DataColumn[] primaryKeyColumns = new DataColumn[cantidadDeLlavesPrimarias];
            int aux = 0;
            foreach (Columna col in columnas)
            {
                if (col.primaryKey)
                {
                    primaryKeyColumns[aux] = data.Columns[col.nombre];
                    aux++;
                }
            }
            data.PrimaryKey = primaryKeyColumns;
        }

        private Type ObtenerTipo(Columna columna)
        {
            if (columna.tipo.Equals(Tipos.INT) || columna.tipo.Equals(Tipos.COUNTER))
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
