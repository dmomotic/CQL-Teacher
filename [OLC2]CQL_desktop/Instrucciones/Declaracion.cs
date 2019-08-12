using _OLC2_CQL_desktop.Arbol;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Instrucciones
{
    class Declaracion : IInstruccion
    {

        readonly Tipos tipo;
        readonly LinkedList<string> identificadores;
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
            if (!asignacion.GetTipo(e).Equals(s.tipo))
            {
               Console.WriteLine("No se puede asigar un tipo difrente a " + s.tipo + " al identificador: " + id);
                return; 
            }

            //Si el tipo de asignacion y del simbolo son iguales
            object valor = asignacion.GetValor(e);
            if (valor == null)
            {
                Console.WriteLine("El valor calculado en la asignacion del id " + id + " es null");
                return;
            }

            //Si el valor calculado es valido
            s.valor = valor;
        }
    }
}
