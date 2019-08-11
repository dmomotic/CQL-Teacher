using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC2_CQL_desktop.Arbol
{
    interface IInstruccion : INodoAST
    {
        void Ejecutar(Entorno e);
    }
}
