using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Expresiones;
using _OLC2_CQL_desktop.Instrucciones;
using _OLC2_CQL_desktop.Structs;
using Irony.Parsing;
using System.Collections.Generic;
using System.Globalization;

namespace _OLC2_CQL_desktop.Inteprete
{
    class ConstructorAST
    {
        public AST Analizar(ParseTreeNode raiz)
        {
            return (AST)Recorrer(raiz);
        }

        private object Recorrer(ParseTreeNode actual)
        {
            //Tiene varios hijos
            if(SoyElNodo("INSTRUCCIONES", actual))
            {
                LinkedList<INodoAST> instrucciones = new LinkedList<INodoAST>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    instrucciones.AddLast((INodoAST)Recorrer(hijo));
                }
                return new AST(instrucciones);
            }

            //Solo tiene un hijo
            if(SoyElNodo("PRINT", actual))
            {
                return new Print((IExpresion)Recorrer(actual.ChildNodes[0]));
            }

            //Solo tiene un hijo
            if(SoyElNodo("LITERAL", actual))
            {
                if (SoyElNodo("entero",actual.ChildNodes[0]))
                {
                    int n = int.Parse(GetLexema(actual,0));
                    return new Literal(n);
                }

                if (SoyElNodo("decimal", actual.ChildNodes[0]))
                {
                    double d = double.Parse(GetLexema(actual, 0), CultureInfo.InvariantCulture);
                    return new Literal(d);
                }

                if (SoyElNodo("cadena", actual.ChildNodes[0]))
                {
                    string aux = GetLexema(actual, 0);
                    aux = aux.Replace("\\n", "\n");
                    aux = aux.Replace("\\t", "\t");
                    aux = aux.Replace("\\r", "\r");
                    aux = aux.Substring(1, aux.Length - 2);
                    return new Literal(aux);
                }

                if(GetLexema(actual,0).Equals("true", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return new Literal(true);
                }

                if(GetLexema(actual, 0).Equals("false", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return new Literal(false);
                }

                if(SoyElNodo("id", actual.ChildNodes[0]))
                {
                    string aux = GetLexema(actual, 0);
                    return new Identificador(aux);
                }

                return null;
            }

            if(SoyElNodo("DECLARACION", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;

                //int LISTA_IDS_ARR
                if(numero_hijos == 2)
                {
                    Tipos tipo = GetTipo(actual.ChildNodes[0]);
                    LinkedList<string> identificadores = GetIds(actual.ChildNodes[1]);
                    return new Declaracion(tipo, identificadores);
                }

                //int LISTA_IDS_ARR EXP
                if(numero_hijos == 3)
                {
                    Tipos tipo = GetTipo(actual.ChildNodes[0]);
                    LinkedList<string> identificadores = GetIds(actual.ChildNodes[1]);
                    IExpresion asignacion = (IExpresion)Recorrer(actual.ChildNodes[2]);
                    return new Declaracion(tipo, identificadores, asignacion);
                }
            }

            if(SoyElNodo("ASIGNACION", actual)){
                int numero_hijos = actual.ChildNodes.Count;

                //id exp
                if (numero_hijos == 2)
                {
                    string id = GetLexema(actual,0);
                    IExpresion valor = (IExpresion)Recorrer(actual.ChildNodes[1]);
                    return new Asignacion(id, valor);
                }
            }

            if(SoyElNodo("EXPRESION_ARITMETICA", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;

                //exp op exp
                if (numero_hijos == 3)
                {
                    IExpresion opIzq = (IExpresion)Recorrer(actual.FirstChild);
                    Operaciones operacion = GetOperacion(actual.ChildNodes[1]);
                    IExpresion opDer = (IExpresion)Recorrer(actual.LastChild);
                    return new Aritmetica(opIzq,operacion,opDer);
                }
            }

            if(SoyElNodo("CREACION_TIPO", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;
                //id LISTA_ATRIBUTOS
                if(numero_hijos == 2)
                {
                    string id = GetLexema(actual, 0);
                    LinkedList<Declaracion> declaraciones = (LinkedList<Declaracion>)Recorrer(actual.LastChild);
                    return new DefinicionStruct(id,declaraciones);
                }
            }

            if (SoyElNodo("LISTA_ATRIBUTOS", actual))
            {
                LinkedList<Declaracion> declaraciones = new LinkedList<Declaracion>();
                foreach(ParseTreeNode atributo in actual.ChildNodes)
                {
                    declaraciones.AddLast((Declaracion)Recorrer(atributo));
                }
                return declaraciones;
            }

            if(SoyElNodo("ATRIBUTO", actual))
            {
                //id int
                LinkedList<string> ids = new LinkedList<string>();
                string id = GetLexema(actual, 0);
                ids.AddLast(id);
                Tipos tipo = GetTipo(actual.LastChild);
                return new Declaracion(tipo,ids);
            }

            return null;
        }

        private bool SoyElNodo(string nombre, ParseTreeNode nodo)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        /**
         * @fn  private string GetLexema(ParseTreeNode padre, int posicion)
         * 
         * @brief Retorna un string de un lexema calculado con un nodo padre
         * 
         * @param padre Nodo padre 
         * 
         * @param posicion es la posicion del hijo del que se desea obtener el lexema
         * 
         * @return string
         * 
         */
        private string GetLexema(ParseTreeNode padre, int posicion)
        {
            return padre.ChildNodes[posicion].Token.Text;
        }

        /*Devuelve el lexema de un nodo enviado como parametro*/
        private string GetLexema(ParseTreeNode nodo)
        {
            return nodo.Token.Text;
        }

        /**
         * @fn  private Tipos GetTipo(ParseTreeNode nodo)
         * 
         * @brief Retorna el tipo en base a un nodo recibido
         * 
         * @param nodo es el nodo que contiene el tipo, ejemplo: int (Keyword)
         * 
         * @return Tipo
         * 
         */
        private Tipos GetTipo(ParseTreeNode nodo)
        {
            if(nodo.Token.Text.Equals("int", System.StringComparison.InvariantCultureIgnoreCase)){
                return Tipos.INT;
            }
            if (nodo.Token.Text.Equals("double", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return Tipos.DOUBLE;
            }
            if (nodo.Token.Text.Equals("boolean", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return Tipos.BOOLEAN;
            }
            if (nodo.Token.Text.Equals("string", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return Tipos.STRING;
            }

            return Tipos.STRUCT;
        }

        /**
         * @fn  private LinkedList<string> getIds(ParseTreeNode nodo)
         * 
         * @brief Recibe el nodo padre que cotiene la lista de identificadores como hijos y retorna la lista de ids
         * 
         * @param nodo Nodo padre que contiene a los hijos
         * 
         * @return LinkedList<string>
         * 
         */
        private LinkedList<string> GetIds(ParseTreeNode nodo)
        {
            LinkedList<string> ids = new LinkedList<string>();
            foreach(ParseTreeNode hijo in nodo.ChildNodes)
            {
                ids.AddLast(hijo.Token.Text);
            }
            return ids;
        }

        private Operaciones GetOperacion(ParseTreeNode nodo)
        {
            switch (GetLexema(nodo))
            {
                case "+":
                    return Operaciones.SUMA;
                case "-":
                    return Operaciones.RESTA;
                case "*":
                    return Operaciones.MULTIPLICACION;
                case "/":
                    return Operaciones.DIVISION;
            }
            return Operaciones.SUMA;
        }

    }
}
