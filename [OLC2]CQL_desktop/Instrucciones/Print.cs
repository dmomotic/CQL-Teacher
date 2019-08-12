using _OLC2_CQL_desktop.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC2_CQL_desktop.Instrucciones
{
    class Print : IInstruccion
    {
        private readonly IExpresion expresion;

        public Print(IExpresion expresion)
        {
            this.expresion = expresion;
        }

        public void Ejecutar(Entorno e)
        {
            object valor = expresion.GetValor(e);
            if(valor != null)
            {
                Console.WriteLine(valor.ToString());
            }
        }
    }
}
