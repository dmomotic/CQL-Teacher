using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Collections;
using _OLC2_CQL_desktop.Expresiones;
using _OLC2_CQL_desktop.Structs;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Instrucciones
{
    class Declaracion : IInstruccion
    {

        readonly Tipos tipo;
        public readonly LinkedList<string> identificadores;
        readonly IExpresion asignacion;

        public Declaracion(Tipos tipo, LinkedList<string> identificadores)
        {
            this.tipo = tipo;
            this.identificadores = identificadores;
        }

        public Declaracion(Tipos tipo, LinkedList<string> identificadores, IExpresion asignacion)
        {
            this.tipo = tipo;
            this.identificadores = identificadores;
            this.asignacion = asignacion;
        }

        public void Ejecutar(Entorno e)
        {
            //Adicion de identificadores al entorno
            foreach(string identificador in identificadores)
            {
                e.Insertar(new Simbolo(identificador, tipo));
            }
            if (asignacion == null)
            {
                return;
            }

            //Si es una asignacion
            string id = identificadores.Last.Value;
            Simbolo s = e.Obtener(id);
            if (s == null)
            {
                Console.WriteLine("No se encontro el simbolo " + id + " en este entorno");
                return;
            }

            //Si se tiene un simbolo valido
            Tipos tipoaAsignar = asignacion.GetTipo(e);
            object valor = asignacion.GetValor(e);

            if (!tipoaAsignar.Equals(s.tipo))
            {
                //Intento realizar un casteo 
                Casteo cast = new Casteo(s.tipo,tipoaAsignar,valor);
                Tipos tipoNuevo = cast.GetTipo(e);
                if (!tipoNuevo.Equals(Tipos.NULL))
                {
                    object nuevoValor = cast.GetValor(e);
                    if (nuevoValor == null)
                    {
                        Console.WriteLine("Ocurrio un error al ejecutar el cast");
                        return;
                    }
                    //Si se obtuvo un valor valido del cast
                    s.valor = nuevoValor;
                }
                //Si no se puede realizar el casteo
                else
                {
                    Console.WriteLine("No se puede asigar un tipo difrente a " + s.tipo + " al identificador: " + id);
                }
                return;
            }

            //Si el tipo de asignacion y del simbolo son iguales
            if (valor == null)
            {
                Console.WriteLine("El valor calculado en la asignacion del id " + id + " es null");
                return;
            }

            //Si es un objeto
            if(s is Objeto objeto && valor is Entorno atributos)
            {
                objeto.atributos = atributos;
                return;
            }
            //Si es una List
            if(s is ListCollection list && valor is List<object> valores)
            {
                list.valores = valores;
                return;
            }

            //Si el valor calculado es valido
            s.valor = valor;
        }
    }
}
