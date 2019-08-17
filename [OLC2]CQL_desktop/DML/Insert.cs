
using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.DDL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _OLC2_CQL_desktop.DML
{
    class Insert : IInstruccion
    {
        readonly string nombreTabla;
        readonly LinkedList<string> ids;
        readonly LinkedList<IExpresion> valores;

        public Insert(string nombreTabla, LinkedList<string> ids, LinkedList<IExpresion> valores)
        {
            this.nombreTabla = nombreTabla;
            this.ids = ids;
            this.valores = valores;
        }

        public Insert(string nombreTabla, LinkedList<IExpresion> valores)
        {
            this.nombreTabla = nombreTabla;
            this.valores = valores;
        }

        public void Ejecutar(Entorno e)
        {
            Tabla tabla = e.ObtenerTabla(nombreTabla);
            if (tabla == null)
            {
                Console.WriteLine("Error!! No se puede hacer la insercion porque la tabla " + nombreTabla + " no existe");
                return;
            }

            //Si existe la tabla
            IEnumerable<Celda> result = null;
            //Si tenemos la lista de ids
            if (ids != null)
            {
                result = ids.Zip(valores, (a, b) => new Celda(a, b.GetValor(e)));
            }
            //Si no tenemos la lista de ids
            else
            {
                result = tabla.columnas.Zip(valores, (a, b) => new Celda(a.nombre, b.GetValor(e)));
            }
            //Creo la lista de celdas
            LinkedList<Celda> celdas = new LinkedList<Celda>();
            foreach(Celda celda in result)
            {
                celdas.AddLast(celda);
            }
            //Insertamos las celdas que representan nuestra tupla
            tabla.Insert(celdas);
        }
    }
}
