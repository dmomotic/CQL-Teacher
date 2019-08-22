using _OLC2_CQL_desktop.Arbol;
using _OLC2_CQL_desktop.Clases;
using _OLC2_CQL_desktop.DDL;
using _OLC2_CQL_desktop.DML;
using _OLC2_CQL_desktop.Expresiones;
using _OLC2_CQL_desktop.FCL;
using _OLC2_CQL_desktop.Instrucciones;
using _OLC2_CQL_desktop.InstruccionesCollections;
using _OLC2_CQL_desktop.Structs;
using Irony.Parsing;
using System;
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
            if (SoyElNodo("INSTRUCCIONES", actual))
            {
                LinkedList<INodoAST> instrucciones = new LinkedList<INodoAST>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    instrucciones.AddLast((INodoAST)Recorrer(hijo));
                }
                return new AST(instrucciones);
            }

            //Solo tiene un hijo
            if (SoyElNodo("PRINT", actual))
            {
                return new Print((IExpresion)Recorrer(actual.ChildNodes[0]));
            }

            //Solo tiene un hijo
            if (SoyElNodo("LITERAL", actual))
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

                if(SoyElNodo("date", actual.ChildNodes[0]))
                {
                    string aux = GetLexema(actual, 0);
                    aux = aux.Replace("\'", "");
                    DateTime fecha = Convert.ToDateTime(aux);
                    return new Literal(fecha);
                }

                if(SoyElNodo("time", actual.ChildNodes[0]))
                {
                    string aux = GetLexema(actual, 0);
                    aux = aux.Replace("\'", "");
                    DateTime hora = Convert.ToDateTime(aux);
                    return new Literal(hora);
                }

                return null;
            }

            if (SoyElNodo("DECLARACION", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;

                if(numero_hijos == 2)
                {
                    LinkedList<string> identificadores = GetIds(actual.ChildNodes[1]);
                    //Estudiante LISTA_IDS_ARR
                    if (actual.FirstChild.Term.Name.Equals("id"))
                    {
                        string id = GetLexema(actual,0);
                        return new DeclaracionStruct(id,identificadores);
                    }
                    //int LISTA_IDS_ARR
                    Tipos tipo = GetTipo(actual.ChildNodes[0]);
                    return new Declaracion(tipo, identificadores);
                }


                if (numero_hijos == 3)
                {
                    LinkedList<string> identificadores = GetIds(actual.ChildNodes[1]);

                    //Estudiante LISTA_IDS_ARR Estudiante    //Est @e = new Est
                    if(SoyElNodo("id",actual.FirstChild) && SoyElNodo("id", actual.LastChild))
                    {
                        string idStructGenerador = GetLexema(actual.FirstChild);
                        return new DeclaracionStruct(idStructGenerador,identificadores);
                    }
                    
                    //Estudiante LISTA_IDS_ARR LISTA_EXPRESIONES
                    if (actual.LastChild.Term.Name.Equals("LISTA_EXPRESIONES", System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        string idStructGenerador = GetLexema(actual, 0);
                        LinkedList<IExpresion> asignaciones = (LinkedList<IExpresion>)Recorrer(actual.ChildNodes[2]);
                        return new DeclaracionStruct(idStructGenerador, identificadores, asignaciones);
                    }

                    //Estudiante LISTA_IDS_ARR CUALQUIER_EXPRESION
                    if(SoyElNodo("id",actual.FirstChild) && SoyElNodo("LISTA_IDS_ARR", actual.ChildNodes[1]))
                    {
                        string idStructGenerador = GetLexema(actual, 0);
                        LinkedList<IExpresion> asignaciones = new LinkedList<IExpresion>();
                        IExpresion asig = (IExpresion)Recorrer(actual.LastChild);
                        asignaciones.AddLast(asig);
                        return new DeclaracionStruct(idStructGenerador,identificadores,asignaciones);
                    }

                    //int LISTA_IDS_ARR EXP
                    Tipos tipo = GetTipo(actual.ChildNodes[0]);
                    IExpresion asignacion = (IExpresion)Recorrer(actual.ChildNodes[2]);
                    return new Declaracion(tipo, identificadores, asignacion);
                }
            }

            if (SoyElNodo("ASIGNACION", actual)){
                int numero_hijos = actual.ChildNodes.Count;

                
                if (numero_hijos == 2)
                {
                    string id = GetLexema(actual, 0);
                    //id LISTA_EXPRESIONES
                    if(SoyElNodo("LISTA_EXPRESIONES", actual.LastChild))
                    {
                        LinkedList<IExpresion> asignaciones = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                        return new AsignacionStruct(id,asignaciones);
                    }

                    //id ID
                    if (SoyElNodo("id", actual.LastChild))
                    {
                        string idStructGenerador = GetLexema(actual,1);
                        return new AsignacionStruct(id, idStructGenerador);
                    }

                    //id exp
                    IExpresion valor = (IExpresion)Recorrer(actual.ChildNodes[1]);
                    return new Asignacion(id, valor);
                }

                //id ACCESOS_OBJETO EXP
                if(numero_hijos == 3)
                {
                    string id = GetLexema(actual,0);
                    LinkedList<string> atributos = (LinkedList<string>)Recorrer(actual.ChildNodes[1]);
                    IExpresion valor = (IExpresion)Recorrer(actual.ChildNodes[2]);
                    return new AsignacionAtributo(id, atributos, valor);
                }
            }

            if (SoyElNodo("ACCESOS_OBJETO", actual))
            {
                LinkedList<string> atributos = new LinkedList<string>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    string atributo = Recorrer(hijo).ToString();
                    atributos.AddLast(atributo);
                }
                return atributos;
            }

            if (SoyElNodo("ACCESO", actual))
            {
                return GetLexema(actual.FirstChild);
            }

            if (SoyElNodo("EXPRESION_ARITMETICA", actual))
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

            if (SoyElNodo("ACCESO_OBJETO", actual))
            {
                //id ACCESOS_OBJETO
                string id = GetLexema(actual,0);
                LinkedList<string> atributos = (LinkedList<string>)Recorrer(actual.LastChild);
                return new AccesoObjeto(id, atributos);
            }

            if (SoyElNodo("CREACION_TIPO", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;
                //id LISTA_ATRIBUTOS
                if(numero_hijos == 2)
                {
                    string id = GetLexema(actual, 0);
                    LinkedList<IInstruccion> declaraciones = (LinkedList<IInstruccion>)Recorrer(actual.LastChild);
                    return new DefinicionStruct(id,declaraciones);
                }
            }

            if (SoyElNodo("LISTA_ATRIBUTOS", actual))
            {
                LinkedList<IInstruccion> declaraciones = new LinkedList<IInstruccion>();
                foreach(ParseTreeNode atributo in actual.ChildNodes)
                {
                    declaraciones.AddLast((IInstruccion)Recorrer(atributo));
                }
                return declaraciones;
            }

            if (SoyElNodo("ATRIBUTO", actual))
            {
                LinkedList<string> ids = new LinkedList<string>();
                string id = GetLexema(actual, 0);
                ids.AddLast(id);
                //id ID
                if (actual.LastChild.Term.Name.Equals("id", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    string idStructGenerador = GetLexema(actual,1);
                    return new DeclaracionStructComoAtributo(id, idStructGenerador);
                }
                //id int
                Tipos tipo = GetTipo(actual.LastChild);
                return new Declaracion(tipo,ids);
            }

            if (SoyElNodo("LISTA_EXPRESIONES", actual))
            {
                LinkedList<IExpresion> expresiones = new LinkedList<IExpresion>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    IExpresion exp = (IExpresion)Recorrer(hijo);
                    expresiones.AddLast(exp);
                }
                return expresiones;
            }

            if (SoyElNodo("ALTER_TYPE", actual))
            {
                //id LISTA_ATRIBUTOS
                string idStruct = GetLexema(actual,0);
                if(SoyElNodo("LISTA_ATRIBUTOS", actual.LastChild))
                {
                    LinkedList<IInstruccion> declaraciones = (LinkedList<IInstruccion>)Recorrer(actual.LastChild);
                    return new AlterType(idStruct, declaraciones);
                }

                //id LISTA_IDS
                if(SoyElNodo("LISTA_IDS", actual.LastChild))
                {
                    LinkedList<string> atributos = (LinkedList<string>)Recorrer(actual.LastChild);
                    return new AlterType(idStruct, atributos);
                }
            }

            if (SoyElNodo("LISTA_IDS", actual))
            {
                LinkedList<string> ids = new LinkedList<string>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    string id = GetLexema(hijo);
                    ids.AddLast(id);
                }
                return ids;
            }

            if (SoyElNodo("DELETE_TYPE", actual))
            {
                string idStruct = GetLexema(actual, 0);
                return new DeleteType(idStruct);
            }

            if (SoyElNodo("CREATE_TABLE", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;
                // nombreTabla COLUMNAS_TABLA
                if (numero_hijos == 2)
                {
                    string nombre = GetLexema(actual,0);
                    LinkedList<Columna> columnas = (LinkedList<Columna>)Recorrer(actual.LastChild);
                    return new CreateTable(nombre, columnas);
                } 

                // exists nombreTabla COLUMNAS_TABLA
                if(numero_hijos == 3)
                {
                    string nombre = GetLexema(actual, 1);
                    LinkedList<Columna> columnas = (LinkedList<Columna>)Recorrer(actual.LastChild);
                    return new CreateTable(true, nombre, columnas);
                }
            }

            if (SoyElNodo("COLUMNAS_TABLA", actual))
            {
                LinkedList<Columna> columnas = new LinkedList<Columna>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    Columna columna = (Columna)Recorrer(hijo);
                    columnas.AddLast(columna);
                }
                return columnas;
            }

            if (SoyElNodo("COLUMNA_TABLA", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;
                
                //id TIPO_DATO primary
                if (numero_hijos == 3)
                {
                    string nombre = GetLexema(actual, 0);
                    Tipos tipo = GetTipo(actual.ChildNodes[1]);
                    return new Columna(nombre, tipo, true);
                }

                if (numero_hijos == 2)
                {
                    //persona Atributo
                    if (SoyElNodo("id", actual.LastChild))
                    {
                        string nombre = GetLexema(actual, 0);
                        return new Columna(nombre, Tipos.OBJETO);
                    }
                    //primary LISTA_IDS
                    if (SoyElNodo("LISTA_IDS", actual.LastChild))
                    {
                        LinkedList<string> ids_columnas_pk = GetIds(actual.LastChild);
                        return new Columna(ids_columnas_pk);
                    }
                    //id TIPO_DATO
                    else
                    {
                        string nombre = GetLexema(actual, 0);
                        Tipos tipo = GetTipo(actual.ChildNodes[1]);
                        return new Columna(nombre, tipo);
                    }
                }
            }

            if (SoyElNodo("INSERT", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;
                string nombreTabla = GetLexema(actual, 0);
                LinkedList<IExpresion> expresiones = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                //nombreTabla LISTA_EXPRESIONES
                if (numero_hijos == 2)
                {
                    return new Insert(nombreTabla, expresiones);
                }
                //nombreTabla LISTA_IDS LISTA_EXPRESIONES
                if (numero_hijos == 3)
                {
                    LinkedList<string> ids = (LinkedList<string>)Recorrer(actual.ChildNodes[1]);
                    return new Insert(nombreTabla, ids, expresiones);
                }
            }

            if (SoyElNodo("SELECT", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;
                
                if(numero_hijos == 2)
                {
                    string nombreTabla = GetLexema(actual, 1);

                    //COLUMNAS_SELECT NombreTabla
                    if (SoyElNodo("COLUMNAS_SELECT", actual.FirstChild))
                    {
                        LinkedList<AccesoColumna> columnas = (LinkedList<AccesoColumna>)Recorrer(actual.FirstChild);
                        return new Select(nombreTabla, columnas);
                    }

                    // * NombreTabla
                    return new Select(nombreTabla);
                }
            }

            if (SoyElNodo("OBJECT_PAIRS", actual))
            {
                LinkedList<ClaveValor> atributos = new LinkedList<ClaveValor>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    ClaveValor claveValor = (ClaveValor)Recorrer(hijo);
                    atributos.AddLast(claveValor);
                }
                return new InstanciaObjecto(atributos);
            }

            if (SoyElNodo("OBJECT_PAIR", actual))
            {
                //clave valor
                IExpresion clave = (IExpresion)Recorrer(actual.FirstChild);
                IExpresion valor = (IExpresion)Recorrer(actual.LastChild);
                return new ClaveValor(clave, valor);
            }

            if (SoyElNodo("COLUMNAS_SELECT", actual))
            {
                LinkedList<AccesoColumna> accesosColumnas = new LinkedList<AccesoColumna>();
                foreach(ParseTreeNode hijo in actual.ChildNodes)
                {
                    AccesoColumna accesoColumna = (AccesoColumna)Recorrer(hijo);
                    accesosColumnas.AddLast(accesoColumna);
                }
                return accesosColumnas;
            }

            if (SoyElNodo("COLUMNA_SELECT", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;
                //id
                if(numero_hijos == 1)
                {
                    string nombre = GetLexema(actual, 0);
                    return new AccesoColumna(nombre);
                }

                //id ACCESOS_OBJETO
                if(numero_hijos == 2)
                {
                    string nombre = GetLexema(actual, 0);
                    LinkedList<string> atributos = (LinkedList<string>)Recorrer(actual.LastChild);
                    return new AccesoColumna(nombre, atributos);
                }
            }

            if (SoyElNodo("MAP", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;

                //LISTA_IDS_ARR < tipo tipo >
                if (numero_hijos == 5)
                {
                    LinkedList<string> identificadores = (LinkedList<string>)Recorrer(actual.FirstChild);
                    Tipos tipoClave = GetTipo(actual.ChildNodes[2]);
                    Tipos tipoValor = GetTipo(actual.ChildNodes[3]);
                    return new Map(identificadores,tipoClave,tipoValor);
                }

                //LISTA_IDS_ARR OBJECT_PAIRS
                if(numero_hijos == 2)
                {
                    LinkedList<string> identificadores = (LinkedList<string>)Recorrer(actual.FirstChild);
                    LinkedList<ClaveValor> valores = ((InstanciaObjecto)Recorrer(actual.LastChild)).atributos;
                    return new Map(identificadores, valores);
                }
            }

            if (SoyElNodo("LISTA_IDS_ARR", actual))
            {
                LinkedList<string> ids = new LinkedList<string>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    ids.AddLast(hijo.Token.Text);
                }
                return ids;
            }

            if (SoyElNodo("COLLECTION_INSERT", actual))
            {
                //id LISTA_EXPRESIONES
                string id = GetLexema(actual, 0);
                LinkedList<IExpresion> valores = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                return new CollectionInsert(id,valores);
            }

            if (SoyElNodo("COLLECTION_GET", actual))
            {
                //id LISTA_EXPRESIONES
                string id = GetLexema(actual, 0);
                LinkedList<IExpresion> valor = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                return new CollectionGet(id,valor);
            }

            if (SoyElNodo("COLLECTION_SET", actual))
            {
                //id LISTA_EXPRESIONES
                string id = GetLexema(actual, 0);
                LinkedList<IExpresion> valores = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                return new CollectionSet(id, valores
);
            }

            if (SoyElNodo("COLLECTION_REMOVE", actual))
            {
                //id LISTA_EXPRESIONES
                string id = GetLexema(actual, 0);
                LinkedList<IExpresion> valores = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                return new CollectionRemove(id, valores);
            }

            if (SoyElNodo("COLLECTION_SIZE", actual))
            {
                //id
                string id = GetLexema(actual, 0);
                return new CollectionSize(id);
            }

            if (SoyElNodo("COLLECTION_CLEAR", actual))
            {
                //id
                string id = GetLexema(actual, 0);
                return new CollectionClear(id);
            }

            if (SoyElNodo("COLLECTION_CONTAINS", actual))
            {
                //id LISTA_EXPRESIONES
                string id = GetLexema(actual, 0);
                LinkedList<IExpresion> valores = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                return new CollectionContains(id, valores);
            }

            if (SoyElNodo("LIST", actual))
            {
                int numero_hijos = actual.ChildNodes.Count;

                if(numero_hijos == 4)
                {
                    LinkedList<string> ids = GetIds(actual.FirstChild);
                    //LISTA_IDS_ARR < TIPO_DATO >
                    if (SoyElNodo("TIPO_DATO", actual.ChildNodes[2]))
                    {
                        Tipos tipo = GetTipo(actual.ChildNodes[2]);
                        return new List(ids, tipo);
                    }
                    //LISTA_IDS_ARR < ID >
                    if (SoyElNodo("id", actual.ChildNodes[2]))
                    {
                        Tipos tipo = Tipos.OBJETO;
                        return new List(ids, tipo);
                    }
                }

                //LISTA_IDS_ARR LISTA_EXPRESIONES
                if (numero_hijos == 2)
                {
                    LinkedList<string> ids = GetIds(actual.FirstChild);
                    LinkedList<IExpresion> valores = (LinkedList<IExpresion>)Recorrer(actual.LastChild);
                    return new List(ids, valores);
                }
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
            if (nodo.Token.Text.Equals("date", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return Tipos.DATE;
            }
            if (nodo.Token.Text.Equals("time", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return Tipos.TIME;
            }
            if (nodo.Token.Text.Equals("counter", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return Tipos.COUNTER;
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
