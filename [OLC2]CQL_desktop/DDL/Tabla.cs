
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.DML;
using _OLC2_CQL_desktop.Expresiones;
using _OLC2_CQL_desktop.Structs;
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
            var columnasConIdsPk = columnas.Where(co => co.ids_columnas_pk != null).FirstOrDefault();

            foreach (Columna col in columnas)
            {
                //Validacion para saltar la columna que solo tiene una lista de ids para llaves primarias
                if (col.ids_columnas_pk != null) continue;

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
                    if (col.primaryKey || (columnasConIdsPk!=null && columnasConIdsPk.ids_columnas_pk.Contains(col.nombre)))
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
            DataColumn[] primaryKeyColumns;
            int aux = 0;

            var items = columnas.Where(col => col.primaryKey==true);
            int cantidadDeLlavesPrimarias = items.Count();
            //Si no hay llaves primarias
            if (cantidadDeLlavesPrimarias <= 0)
            {
                //Si no hay ninguna columna con los ids de las llaves primarias
                var columnasConIdsPk = columnas.Where(col => col.ids_columnas_pk != null).FirstOrDefault();
                if (columnasConIdsPk == null) return;

                //Si hay una columna con los ids
                cantidadDeLlavesPrimarias = columnasConIdsPk.ids_columnas_pk.Count;
                primaryKeyColumns = new DataColumn[cantidadDeLlavesPrimarias];
                foreach(string nombreColuma in columnasConIdsPk.ids_columnas_pk)
                {
                    primaryKeyColumns[aux] = data.Columns[nombreColuma];
                    aux++;
                }
                data.PrimaryKey = primaryKeyColumns;
                return;
            }
            //Si hay llaves primarias
            primaryKeyColumns = new DataColumn[cantidadDeLlavesPrimarias];
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
            if (columna.tipo.Equals(Tipos.DATE) || columna.tipo.Equals(Tipos.TIME))
            {
                return typeof(DateTime);
            }
            if (columna.tipo.Equals(Tipos.STRING))
            {
                return typeof(string);
            }
            if (columna.tipo.Equals(Tipos.OBJETO))
            {
                return typeof(Objeto);
            }
            if (columna.tipo.Equals(Tipos.MAP))
            {
                return typeof(MapCollection);
            }
            if (columna.tipo.Equals(Tipos.LIST))
            {
                return typeof(ListCollection);
            }
            if (columna.tipo.Equals(Tipos.SET))
            {
                return typeof(SetCollection);
            }
            return typeof(Nullable);
        }

        public void Insert(LinkedList<Celda> celdas)
        {
            DataRow myDataRow = data.NewRow();
            string registro = "";
            foreach (Celda celda in celdas)
            {
                //Si es un map viene en un objeto
                Type tipoColumna = myDataRow.Table.Columns[celda.id].DataType;
                if (tipoColumna.Equals(typeof(MapCollection)) && celda.valor is Objeto objeto)
                {
                    //Aqui debo generar el MapCollection
                    MapCollection map = new MapCollection("")
                    {
                        valores = objeto.atributos
                    };
                    myDataRow[celda.id] = map;
                }
                //Si es una lista el valor es una List<object>
                else if(tipoColumna.Equals(typeof(ListCollection)) && celda.valor is List<object> valores)
                {
                    ListCollection list = new ListCollection("")
                    {
                        valores = valores
                    };
                    myDataRow[celda.id] = list;
                }
                //Si es un set el valor es una List<object>
                else if(tipoColumna.Equals(typeof(SetCollection)) && celda.valor is List<object> vals)
                {
                    SetCollection set = new SetCollection("")
                    {
                        valores = vals
                    };
                    myDataRow[celda.id] = set;
                }
                else
                {
                    myDataRow[celda.id] = celda.valor;
                    registro += celda.valor + " ";
                }
            }
            try
            {
                data.Rows.Add(myDataRow);
            }
            catch (Exception e)
            {
                Console.WriteLine("No se puede insertar el registro: " + registro + " posible llave primaria duplicada");
            }
            
        }

        public DataRow[] Select()
        {
            return data.Select();
        }
    }
}
