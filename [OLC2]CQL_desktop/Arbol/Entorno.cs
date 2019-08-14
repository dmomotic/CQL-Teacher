using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections;

namespace _OLC2_CQL_desktop.Arbol
{
    class Entorno : Hashtable
    {
        private readonly Entorno padre;

        public Entorno() : base ()
        {
            this.padre = null;
        }

        public Entorno(Entorno padre) : base ()
        {
            this.padre = padre;
        }

        public void Insertar(Simbolo simbolo)
        {
            for(Entorno e=this; e!=null; e = e.padre)
            {
                Simbolo s = (Simbolo)this[simbolo.identificador];
                if (s != null)
                {
                    Console.WriteLine("Error!!, ya existe declarada una variable con id: " + simbolo.identificador);
                    return;
                }
            }
            this.Add(simbolo.identificador,simbolo);
        }

        public void InsertarObjeto(string identificador, Objeto objeto)
        {
            this.Add(identificador,objeto);
        }

        public void ActualizarObjeto(string identificador, Objeto objeto)
        {
            for(Entorno e = this; e!=null; e = e.padre)
            {
                if (e.ContainsKey(identificador))
                {
                    e[identificador] = objeto;
                    return;
                }
            }
        }

        public bool Existe(string identificador)
        {
            for(Entorno e = this; e!=null; e = e.padre)
            {
                if (e.Contains(identificador))
                {
                    return true;
                }
            }
            return false;
        }

        public Simbolo Obtener(string id)
        {
            for(Entorno e = this; e!=null; e = e.padre)
            {
                Simbolo encontrado = (Simbolo)e[id];
                if (encontrado!=null)
                {
                    return encontrado;
                }
            }
            Console.WriteLine("No se encontro la variable " + id + " en este entorno :(");
            return null;
        }

        public Entorno GetGlobal()
        {
            Entorno e = this;
            while(e.padre != null)
            {
                e = e.padre;
            }
            return e;
        }

        public void InsertarStruct(Struct _struct)
        {
            this.GetGlobal().Add(_struct.identificador,_struct);
        }

        public bool TieneAlStruct(string id)
        {
            return this.GetGlobal().Contains(id) && ((Simbolo)this.GetGlobal()[id]).IsStruct();
        }

        public Struct ObtenerStruct(string id)
        {
            Entorno global = this.GetGlobal();
            if(global.Contains(id) && ((Simbolo)global[id]).IsStruct())
            {
                return (Struct)global[id];
            }
            Console.WriteLine("No se encontro al struct " + id + " en este entorno :( ");
            return null;
        }
    }
}
