using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Expresiones;
using System;

namespace _OLC2_CQL_desktop.Instrucciones
{
    class Asignacion : IInstruccion
    {

        readonly string id;
        readonly IExpresion valor;

        public Asignacion(string id, IExpresion valor)
        {
            this.id = id;
            this.valor = valor;
        }

        public void Ejecutar(Entorno e)
        {
            Simbolo simb = e.Obtener(id);
            if(simb == null)
            {
                Console.WriteLine("No se puede asigar un valor al identificador " + id + " ya que no se encuentra en este entorno");
                return;
            }
            //Si encontramos el simbolo validamos tipos
            Tipos tipo_asignacion = simb.tipo;
            Tipos tipo_a_asignar = valor.GetTipo(e);
            object val = valor.GetValor(e);
            if (!tipo_asignacion.Equals(tipo_a_asignar))
            {
                //Intento realizar un casteo 
                Casteo cast = new Casteo(tipo_asignacion, tipo_a_asignar, val);
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
                    simb.valor = nuevoValor;
                }
                //Si no se puede realizar el casteo
                else
                {
                    Console.WriteLine("No se puede asigar un tipo " + tipo_a_asignar + " al identificador " + id + " de tipo " + tipo_asignacion);
                }
                return;
            } 
            //Si los tipos son iguales
            if(val == null)
            {
                Console.WriteLine("No se realizo la asignacion al identificador " + id + " ya que al calcular el valor de su expresion retorno null");
                return;
            }
            //Si se capturo un valor valido
            simb.valor = val;
        }
    }
}
