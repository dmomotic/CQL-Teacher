namespace _OLC2_CQL_desktop.Arbol
{
    interface IExpresion : INodoAST
    {
        object GetValor(Entorno e);
        Tipos GetTipo(Entorno e);

    }
}
