using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Instrucciones;
using System;
using System.Collections;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Structs
{
    class DeclaracionStruct : IInstruccion
    {

        readonly string idStructGenerador;
        readonly LinkedList<string> identificadores;
        //Solo para cuando sea una asignacion
        readonly LinkedList<IExpresion> asignaciones;

        public DeclaracionStruct(string idStructGenerador, LinkedList<string> identificadores)
        {
            this.idStructGenerador = idStructGenerador;
            this.identificadores = identificadores;
        }

        public DeclaracionStruct(string idStructGenerador, LinkedList<string> identificadores, LinkedList<IExpresion> asignaciones)
        {
            this.idStructGenerador = idStructGenerador;
            this.identificadores = identificadores;
            this.asignaciones = asignaciones;
        }

        public void Ejecutar(Entorno e)
        {
            Struct s = e.ObtenerStruct(idStructGenerador);
            if (s == null)
            {
                Console.WriteLine("No existe el struct " + idStructGenerador);
                return;
            }
            //Lista auxiliar para la asignacion de valores
            ArrayList lista_atributos = new ArrayList();

            //Si se encontro el struct
            foreach (string identificador in identificadores)
            {
                //Si ya existe un identificador con el mismo nombre
                if (e.Existe(identificador))
                {
                    Console.WriteLine("Error ya existe un identificador con el nombre: " + identificador);
                    continue;
                }
                //Si no existe el identificador
                Entorno atributos = new Entorno();

                //Realizo la creacion de los atributos 
                foreach(IInstruccion declaracion in s.declaraciones)
                {
                    if(declaracion is DeclaracionStructComoAtributo)
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
                e.InsertarObjeto(identificador, new Objeto(idStructGenerador, atributos));
            }
            //Realizo la asignacion de valores si se tiene una inicializacion
            if (asignaciones != null)
            {
                string identificador = identificadores.Last.Value;
                Objeto objeto = (Objeto)e.Obtener(identificador);
                if (asignaciones.Count != objeto.GetNumeroDeAtributos())
                {
                    Console.WriteLine("El numero de parametros no coindice con la cantidad de atributos del objeto "  + identificador);
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
