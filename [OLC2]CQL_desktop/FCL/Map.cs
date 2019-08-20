using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Clases;
using _OLC2_CQL_desktop.Collections;
using System;
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.FCL
{
    class Map : IInstruccion
    {
        readonly LinkedList<string> identificadores;
        readonly Tipos tipoClave;
        readonly Tipos tipoValor;

        //atributo utilizado cuando se le envian valores
        readonly LinkedList<ClaveValor> clavesValores;

        public Map(LinkedList<string> identificadores, Tipos tipoClave, Tipos tipoValor)
        {
            this.identificadores = identificadores;
            this.tipoClave = tipoClave;
            this.tipoValor = tipoValor;
        }

        public Map(LinkedList<string> identificadores, LinkedList<ClaveValor> clavesValores)
        {
            this.identificadores = identificadores;
            this.clavesValores = clavesValores;
        }

        public void Ejecutar(Entorno e)
        {
            if (identificadores == null)
            {
                Console.WriteLine("Error al crear map, porque no se encontro ningun identificador valido para la asignacion");
                return;
            }
            MapCollection map = null;
            //Si tenemos identificadores los declaramos
            foreach (string identificador in identificadores)
            {
                map = new MapCollection(identificador, tipoClave, tipoValor);
                e.InsertarMap(identificador, map);
            }
            if (clavesValores == null) return;
            //Si ya trae los valores los insertamos
            foreach(ClaveValor pair in clavesValores)
            {
                object clave = pair.clave.GetValor(e);
                if (map.TieneLaClave(clave))
                {
                    Console.WriteLine("No se puede insertar al map: " + map.identificador + " con la clave: " + clave + " porque ya tiene un valor ingresado con la misma clave");
                    continue;
                }
                object valor = pair.valor.GetValor(e);
                Tipos tipoValor = pair.valor.GetTipo(e);
                map.Insertar(clave, valor, tipoValor);
            }
        }
    }
}
