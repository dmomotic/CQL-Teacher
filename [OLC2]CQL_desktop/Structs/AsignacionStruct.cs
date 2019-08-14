

using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Instrucciones;
using System;
using System.Collections;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class AsignacionStruct : IInstruccion
    {

        readonly string id;
        public string idStructGenerador;
        //Solo para cuando sea una asignacion
        readonly LinkedList<IExpresion> asignaciones;

        public AsignacionStruct(string id, string idStructGenerador)
        {
            this.id = id;
            this.idStructGenerador = idStructGenerador;
        }

        public AsignacionStruct(string id, LinkedList<IExpresion> asignaciones)
        {
            this.id = id;
            this.asignaciones = asignaciones;
            this.idStructGenerador = "";
        }

        public void Ejecutar(Entorno e)
        {
            Simbolo s = e.Obtener(id);
            if (s == null)
            {
                Console.WriteLine("No se encontro el id " + id + " en este entorno");
                return;
            }
            //Si se encontro el simbolo
            if (!s.IsObject())
            {
                Console.WriteLine("El id " + id + " no es un objeto");
                return;
            }
            //Si es un objeto
            Objeto o = (Objeto)s;
            idStructGenerador = idStructGenerador.Equals("") ? o.idStructGenerador : idStructGenerador;
            if (!o.idStructGenerador.Equals(idStructGenerador,System.StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("No se puede asigar a un objeto tipo " + o.idStructGenerador + " una instancia de tipo " + idStructGenerador);
                return;
            }
            //Si los tipos de objeto coinciden
            Struct st = e.ObtenerStruct(idStructGenerador);
            //Lista auxiliar para la asignacion de valores
            ArrayList lista_atributos = new ArrayList();
            Entorno atributos = new Entorno();

            //Realizo la creacion de los atributos 
            foreach (IInstruccion declaracion in st.declaraciones)
            {
                if (declaracion is DeclaracionStructComoAtributo)
                {
                    DeclaracionStructComoAtributo dec = (DeclaracionStructComoAtributo)declaracion;
                    dec.auxiliar = e;
                    dec.Ejecutar(atributos);
                    lista_atributos.Add(dec.id);
                }
                else
                {
                    declaracion.Ejecutar(atributos);
                    lista_atributos.Add(((Declaracion)declaracion).identificadores.First.Value);
                }
            }
            e.ActualizarObjeto(id, new Objeto(idStructGenerador, atributos));
            

            //Realizo la asignacion de valores si se tiene una inicializacion
            if (asignaciones != null)
            {
                string identificador = id;
                Objeto objeto = (Objeto)e.Obtener(identificador);
                if (asignaciones.Count != objeto.GetNumeroDeAtributos())
                {
                    Console.WriteLine("El numero de parametros no coindice con la cantidad de atributos del objeto " + identificador);
                    return;
                }
                //Si la cantidad de atributos es la misma
                int pos = 0;
                foreach (IExpresion asig in asignaciones)
                {
                    /**********************************************
                      NO ESTOY EFECTUANDO COMPROBACION DE TIPOS
                     ************************************************/
                    objeto.SetAtributo(lista_atributos[pos].ToString(), asig.GetValor(e));
                    pos++;
                }
            }
        }
    }
}
