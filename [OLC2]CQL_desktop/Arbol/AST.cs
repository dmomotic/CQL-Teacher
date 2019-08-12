
using System.Collections.Generic;

namespace _OLC2_CQL_desktop.Arbol
{
    class AST
    {
        public LinkedList<INodoAST> instrucciones;

        public AST(LinkedList<INodoAST> instrucciones)
        {
            this.instrucciones = instrucciones;
        }
    }
}
