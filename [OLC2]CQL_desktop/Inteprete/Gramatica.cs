using Irony.Parsing;

namespace _OLC2_CQL_desktop.Inteprete
{
    class Gramatica : Irony.Parsing.Grammar
    {
        public Gramatica() : base(false)
        {
            #region TERMINALES

            /*** PALABRAS RESERVADAS ***/
            KeyTerm 
                r_int = ToTerm("int"),
                r_double = ToTerm("double"),
                r_string = ToTerm("string"),
                r_boolean = ToTerm("boolean"),
                r_true = ToTerm("true"),
                r_false = ToTerm("false"),
                r_date = ToTerm("date"),
                r_time = ToTerm("time"),
                r_create = ToTerm("create"),
                r_type = ToTerm("type"),
                r_if = ToTerm("if"),
                r_not = ToTerm("not"),
                r_exists = ToTerm("exists"),
                r_new = ToTerm("new"),
                r_null = ToTerm("null"), 
                r_alter = ToTerm("alter"),
                r_add = ToTerm("add"),
                r_delete = ToTerm("delete"),
                r_print = ToTerm("print"),
                r_table = ToTerm("table"),
                r_primary = ToTerm("primary"),
                r_key = ToTerm("key"),
                r_insert = ToTerm("insert"),
                r_into = ToTerm("into"),
                r_values = ToTerm("values"),
                r_select = ToTerm("select"),
                r_from = ToTerm("from"),
                r_where = ToTerm("where"),
                r_counter = ToTerm("counter"),
                r_map = ToTerm("map"),
                r_get = ToTerm("get"),
                r_set = ToTerm("set"),
                r_remove = ToTerm("remove"),
                r_size = ToTerm("size"),
                r_clear = ToTerm("clear"),
                r_contains = ToTerm("contains"),
                r_list = ToTerm("list")
            ;

            //Le indicamos al parser las palabras reservadas, para evitar conflictos al reconocer ids
            MarkReservedWords(
                "int", 
                "double", 
                "boolean",
                "true",
                "false",
                "string", 
                "date", 
                "time", 
                "create",
                "type",
                "if",
                "not",
                "exists",
                "new",
                "null",
                "alter",
                "add",
                "delete",
                "print",
                "table",
                "primary",
                "key",
                "insert",
                "into",
                "values",
                "select",
                "from",
                "where",
                "counter",
                "map",
                "get",
                "set",
                "remove",
                "size",
                "clear",
                "contains",
                "list"
            );

            /*** SIMBOLOS DEL LENGUAJE ***/
            Terminal 
                ptocoma = ToTerm(";"),
                mas = ToTerm("+"),
                menos = ToTerm("-"),
                por = ToTerm("*"),
                div = ToTerm("/"),
                coma = ToTerm(","),
                punto = ToTerm("."),
                parizq = ToTerm("("),
                parder = ToTerm(")"),
                igual = ToTerm("="),
                llavizq = ToTerm("{"),
                llavder = ToTerm("}"),
                arroba = ToTerm("@"),
                dosptos = ToTerm(":"),
                mayque = ToTerm(">"),
                menque = ToTerm("<"),
                corizq = ToTerm("["),
                corder = ToTerm("]")
            ;

            /*** EXPRESIONES REGULARES ***/
            NumberLiteral _decimal = new NumberLiteral("decimal",NumberOptions.AllowStartEndDot);
            NumberLiteral entero = new NumberLiteral("entero", NumberOptions.IntOnly);
            IdentifierTerminal id = new IdentifierTerminal("id");
            StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.AllowsAllEscapes);
            RegexBasedTerminal date = new RegexBasedTerminal("date", "'[0-9]{4}-[0-9]{2}-[0-9]{2}'");
            RegexBasedTerminal time = new RegexBasedTerminal("time", "'[0-9]{2}:[0-9]{2}:[0-9]{2}'");

            /*** COMENTARIOS DE UNA Y VARIAS LINEAS ***/
            CommentTerminal comentarioMultilinea = new CommentTerminal("comentarioMultiLinea", "/*", "*/");
            base.NonGrammarTerminals.Add(comentarioMultilinea);

            CommentTerminal comentarioUnilinea = new CommentTerminal("comentarioUniLinea", "//", "\n", "\r\n");
            base.NonGrammarTerminals.Add(comentarioUnilinea);

            #endregion

            #region NO_TERMINALES

            NonTerminal
                INICIO = new NonTerminal("INICIO"),
                INSTRUCCIONES = new NonTerminal("INSTRUCCIONES"),
                INSTRUCCION = new NonTerminal("INSTRUCCION"),
                CREACION_TIPO = new NonTerminal("CREACION_TIPO"),
                LISTA_ATRIBUTOS = new NonTerminal("LISTA_ATRIBUTOS"),
                ATRIBUTO = new NonTerminal("ATRIBUTO"),
                TIPO_DATO = new NonTerminal("TIPO_DATO"),
                DECLARACION = new NonTerminal("DECLARACION"),
                ASIGNACION = new NonTerminal("ASIGNACION"),
                EXPRESION = new NonTerminal("EXPRESION"),
                LISTA_EXPRESIONES = new NonTerminal("LISTA_EXPRESIONES"),
                ACCESO_OBJETO = new NonTerminal("ACCESO_OBJETO"),
                ACCESOS_OBJETO = new NonTerminal("ACCESOS_OBJETO"),
                ACCESO = new NonTerminal("ACCESO"),
                ALTER_TYPE = new NonTerminal("ALTER_TYPE"),
                DELETE_TYPE = new NonTerminal("DELETE_TYPE"),
                LISTA_IDS = new NonTerminal("LISTA_IDS"),
                LISTA_IDS_ARR = new NonTerminal("LISTA_IDS_ARR"),
                ID_ARR = new NonTerminal("ID_ARR"),
                PRINT = new NonTerminal("PRINT"),
                EXPRESION_ARITMETICA = new NonTerminal("EXPRESION_ARITMETICA"),
                LITERAL = new NonTerminal("LITERAL"),
                INSTRUCCION_DDL = new NonTerminal("INSTRUCCION_DDL"),
                CREATE_TABLE = new NonTerminal("CREATE_TABLE"),
                COLUMNAS_TABLA = new NonTerminal("COLUMNAS_TABLA"),
                COLUMNA_TABLA = new NonTerminal("COLUMNA_TABLA"),
                INSTRUCCION_DML = new NonTerminal("INSTRUCCION_DML"),
                INSERT = new NonTerminal("INSERT"),
                SELECT = new NonTerminal("SELECT"),
                OBJECT_NOTATION = new NonTerminal("OBJECT_NOTATION"),
                OBJECT_PAIRS = new NonTerminal("OBJECT_PAIRS"),
                OBJECT_PAIR = new NonTerminal("OBJECT_PAIR"),
                COLUMNAS_SELECT = new NonTerminal("COLUMNAS_SELECT"),
                COLUMNA_SELECT = new NonTerminal("COLUMNA_SELECT"),
                INSTRUCCION_FCL = new NonTerminal("INSTRUCCION_FCL"),
                MAP = new NonTerminal("MAP"),
                LLAMADA_METODO_FUNCION = new NonTerminal("LLAMADA_METODO_FUNCION"),
                COLLECTION_INSERT = new NonTerminal("COLLECTION_INSERT"),
                COLLECTION_GET = new NonTerminal("COLLECTION_GET"),
                COLLECTION_SET = new NonTerminal("COLLECTION_SET"),
                COLLECTION_REMOVE = new NonTerminal("COLLECTION_REMOVE"),
                COLLECTION_SIZE = new NonTerminal("COLLECTION_SIZE"),
                COLLECTION_CLEAR = new NonTerminal("COLLECTION_CLEAR"),
                COLLECTION_CONTAINS = new NonTerminal("COLLECTION_CONTAINS"),
                LIST = new NonTerminal("LIST"),
                SET = new NonTerminal("SET")
            ;

            #endregion

            #region GRAMATICA

            INICIO.Rule = INSTRUCCIONES; // ya

            INSTRUCCIONES.Rule = MakePlusRule(INSTRUCCIONES, INSTRUCCION); //ya

            INSTRUCCION.Rule = CREACION_TIPO
                | DECLARACION
                | ASIGNACION
                | ALTER_TYPE //ya
                | DELETE_TYPE //ya
                | PRINT //ya
                | INSTRUCCION_DDL
                | INSTRUCCION_DML
                | INSTRUCCION_FCL
                | LLAMADA_METODO_FUNCION
                ;

            PRINT.Rule = r_print + parizq + EXPRESION + parder + ptocoma ; //ya

            CREACION_TIPO.Rule = r_create + r_type + id + parizq + LISTA_ATRIBUTOS + parder + ptocoma //ya
                | r_create + r_type + r_if + r_not + r_exists + id + parizq + LISTA_ATRIBUTOS + parder + ptocoma
                ;

            LISTA_ATRIBUTOS.Rule = MakePlusRule(LISTA_ATRIBUTOS, coma , ATRIBUTO) //ya
                ;

            ATRIBUTO.Rule = id + TIPO_DATO //ya
                | id + id
                ;

            TIPO_DATO.Rule = r_int
                | r_double
                | r_string
                | r_boolean
                | r_date
                | r_time
                | r_counter //solo para los atributos de la tabla
                ;

            DECLARACION.Rule = id + LISTA_IDS_ARR + ptocoma //Estudiante @est; - ya
                | id + LISTA_IDS_ARR + igual + r_new + id + ptocoma // Estudiante @est = new Estudiante; - ya
                | id + LISTA_IDS_ARR + igual + llavizq + LISTA_EXPRESIONES + llavder + ptocoma //Estudiante @est = {valores}; //primitivos ya
                | id + LISTA_IDS_ARR + igual + EXPRESION + ptocoma // Est @est = lista.get(0);
                | TIPO_DATO + LISTA_IDS_ARR + igual + EXPRESION + ptocoma //int @carnet = cualquiercosa; - primitivos ya
                | TIPO_DATO + LISTA_IDS_ARR + ptocoma //int @carnet; - ya 
                ;

            ASIGNACION.Rule = arroba + id + igual + r_new + id + ptocoma //@est = new Estudiante; - ya
                | arroba + id + igual + llavizq + LISTA_EXPRESIONES + llavder + ptocoma // @est = new {valores}; - primitivos ya8/
                | arroba + id + igual + EXPRESION + ptocoma //@var = 5+"hola"; - primitivos ya
                | arroba + id + ACCESOS_OBJETO + igual + EXPRESION + ptocoma //@obj.atr.atr = 5 +3; - ya
               ;


            EXPRESION.Rule = EXPRESION_ARITMETICA
                | LITERAL //ya
                | ACCESO_OBJETO //ya
                | llavizq + LISTA_EXPRESIONES + llavder //aux para asignacion de objetos
                | OBJECT_NOTATION //para la creacion de objectos tipo { "llave":valor }
                | COLLECTION_GET
                | COLLECTION_SIZE
                | COLLECTION_CONTAINS
                ;

            LITERAL.Rule = entero //ya
                | _decimal //ya
                | arroba + id //ya
                | cadena //ya
                | r_true //ya
                | r_false //ya
                | date //ya
                | time //ya
                ;

            EXPRESION_ARITMETICA.Rule = EXPRESION + mas + EXPRESION //ya
                | EXPRESION + menos + EXPRESION //ya
                | EXPRESION + por + EXPRESION //ya
                | EXPRESION + div + EXPRESION //ya
                ;

            LISTA_EXPRESIONES.Rule = MakePlusRule(LISTA_EXPRESIONES, coma, EXPRESION)  //5,"hola",true
                ;

            ACCESO_OBJETO.Rule = arroba + id + ACCESOS_OBJETO //@est.accesos - ya
                ;

            ACCESOS_OBJETO.Rule = MakePlusRule(ACCESOS_OBJETO, ACCESO); // .atributo.otroAtributo.otroAtributo - ya

            ACCESO.Rule = punto + id; // .atributo - ya

            ALTER_TYPE.Rule = r_alter + r_type + id + r_add + parizq + LISTA_ATRIBUTOS + parder + ptocoma //add campo - ya
                | r_alter + r_type + id + r_delete + parizq + LISTA_IDS + parder + ptocoma //delete campo - ya
                ;

            DELETE_TYPE.Rule = r_delete + r_type + id + ptocoma; //delete type Estudiante ; - ya

            LISTA_IDS.Rule = MakePlusRule(LISTA_IDS, coma, id); // id, otro_id, otro_id

            LISTA_IDS_ARR.Rule = MakePlusRule(LISTA_IDS_ARR, coma, ID_ARR); //@id, @otro_id, @otro_id

            ID_ARR.Rule = arroba + id; // @id

            INSTRUCCION_DDL.Rule = CREATE_TABLE
                ;

            CREATE_TABLE.Rule = r_create + r_table + id + parizq + COLUMNAS_TABLA + parder + ptocoma // ya
                | r_create + r_table + r_if + r_not + r_exists + id + parizq + COLUMNAS_TABLA + parder + ptocoma //ya
                ;

            COLUMNAS_TABLA.Rule = MakePlusRule(COLUMNAS_TABLA, coma, COLUMNA_TABLA); //ya

            COLUMNA_TABLA.Rule = id + TIPO_DATO + r_primary + r_key //ya
                | id + TIPO_DATO //ya
                | id + id
                | r_primary + r_key + parizq + LISTA_IDS + parder
                ;

            INSTRUCCION_DML.Rule = INSERT //ya
                | SELECT
                ;

            INSERT.Rule = r_insert + r_into + id + r_values + parizq + LISTA_EXPRESIONES + parder + ptocoma //ya
                | r_insert + r_into + id + parizq + LISTA_IDS + parder + r_values + parizq + LISTA_EXPRESIONES + parder + ptocoma //ya
                ;

            SELECT.Rule = r_select + por + r_from + id + ptocoma //ya
                | r_select + COLUMNAS_SELECT + r_from + id + ptocoma
                ;

            COLUMNAS_SELECT.Rule = MakePlusRule(COLUMNAS_SELECT, coma, COLUMNA_SELECT);

            COLUMNA_SELECT.Rule = id
                | id + ACCESOS_OBJETO
                ;

            OBJECT_NOTATION.Rule = llavizq + OBJECT_PAIRS + llavder;

            OBJECT_PAIRS.Rule = MakePlusRule(OBJECT_PAIRS, coma, OBJECT_PAIR);

            OBJECT_PAIR.Rule = EXPRESION + dosptos + EXPRESION;

            INSTRUCCION_FCL.Rule = MAP
                | LIST
                | SET
                ;

            MAP.Rule = r_map + LISTA_IDS_ARR + igual + r_new + r_map + menque + TIPO_DATO + coma + TIPO_DATO + mayque + ptocoma
                | r_map + LISTA_IDS_ARR + igual + corizq + OBJECT_PAIRS + corder + ptocoma
                ;

            LIST.Rule = r_list + LISTA_IDS_ARR + igual + r_new + r_list + menque + TIPO_DATO + mayque + ptocoma
                | r_list + LISTA_IDS_ARR + igual + r_new + r_list + menque + id + mayque + ptocoma
                | r_list + LISTA_IDS_ARR + igual + corizq + LISTA_EXPRESIONES + corder + ptocoma
                ;

            SET.Rule = r_set + LISTA_IDS_ARR + igual + r_new + r_set + menque + TIPO_DATO + mayque + ptocoma
                | r_set + LISTA_IDS_ARR + igual + r_new + r_set + menque + id + mayque + ptocoma
                | r_set + LISTA_IDS_ARR + igual + corizq + LISTA_EXPRESIONES + corder + ptocoma
                ;

            LLAMADA_METODO_FUNCION.Rule = arroba + id + punto + id + parizq + parder + ptocoma //@var.metodo()
                | COLLECTION_INSERT
                | COLLECTION_SET
                | COLLECTION_REMOVE
                | COLLECTION_CLEAR
                ;

            COLLECTION_INSERT.Rule = arroba + id + punto + r_insert + parizq + LISTA_EXPRESIONES + parder + ptocoma //@var.Insert(par1, par2, par3)
                ;

            COLLECTION_GET.Rule = arroba + id + punto + r_get + parizq + LISTA_EXPRESIONES + parder //@var.Get(valor)
                ;

            COLLECTION_SET.Rule = arroba + id + punto + r_set + parizq + LISTA_EXPRESIONES + parder + ptocoma //@var.Set(par1,par2);
                ;

            COLLECTION_REMOVE.Rule = arroba + id + punto + r_remove + parizq + LISTA_EXPRESIONES + parder + ptocoma //@var.Remove(par1,par2);
                ;

            COLLECTION_SIZE.Rule = arroba + id + punto + r_size + parizq + parder  //@var.Size()
                ;

            COLLECTION_CLEAR.Rule = arroba + id + punto + r_clear + parizq + parder + ptocoma //@var.Clear();
                ;

            COLLECTION_CONTAINS.Rule = arroba + id + punto + r_contains + parizq + LISTA_EXPRESIONES + parder //@var.Contains(vals)
                ;

            /*** PRECEDENCIAS ***/
            RegisterOperators(5, Associativity.Left, mas, menos);
            RegisterOperators(6, Associativity.Left, por, div);

            /*** SIMBOLOS QUE NO ME SON DE UTILIDAD EN EL ARBOL ***/
            MarkPunctuation(
                ptocoma, parizq, parder, arroba, igual, llavizq, llavder, coma, punto,
                r_create, r_type, r_if, r_not, r_new, r_alter, r_delete, r_add,
                r_print, r_table, r_key, r_insert, r_into, r_values, r_select, r_from, r_where, dosptos,
                r_map, corizq, corder, r_get, r_set, r_remove, r_size, r_clear, r_contains, r_list
            );

            /*** NODOS QUE NO ME SON DE UTILIDAD EN EL ARBOL ***/
            MarkTransient(
                INICIO, INSTRUCCION, TIPO_DATO, ID_ARR, EXPRESION, INSTRUCCION_DDL, INSTRUCCION_DML, OBJECT_NOTATION,
                INSTRUCCION_FCL, LLAMADA_METODO_FUNCION
            );


            this.Root = INICIO;

            #endregion
        }
    }
}
