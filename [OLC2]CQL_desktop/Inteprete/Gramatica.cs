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
                r_print = ToTerm("print")
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
                "print"
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
                arroba = ToTerm("@")
            ;

            /*** EXPRESIONES REGULARES ***/
            NumberLiteral _decimal = new NumberLiteral("decimal",NumberOptions.AllowStartEndDot);
            NumberLiteral entero = new NumberLiteral("entero", NumberOptions.IntOnly);
            IdentifierTerminal id = new IdentifierTerminal("id");
            StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.AllowsAllEscapes);

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
                LITERAL = new NonTerminal("LITERAL")
            
            ;

            #endregion

            #region GRAMATICA

            INICIO.Rule = INSTRUCCIONES; // ya

            INSTRUCCIONES.Rule = MakePlusRule(INSTRUCCIONES, INSTRUCCION); //ya

            INSTRUCCION.Rule = CREACION_TIPO
                | DECLARACION
                | ASIGNACION
                | ALTER_TYPE
                | DELETE_TYPE
                | PRINT //ya
                ;

            PRINT.Rule = r_print + parizq + EXPRESION + parder + ptocoma ; //ya

            CREACION_TIPO.Rule = r_create + r_type + id + parizq + LISTA_ATRIBUTOS + parder + ptocoma //ya
                | r_create + r_type + r_if + r_not + r_exists + id + parizq + LISTA_ATRIBUTOS + parder + ptocoma
                ;

            LISTA_ATRIBUTOS.Rule = MakePlusRule(LISTA_ATRIBUTOS, coma , ATRIBUTO) //ya
                ;

            ATRIBUTO.Rule = id + TIPO_DATO //ya
                ;

            TIPO_DATO.Rule = r_int
                | r_double
                | r_string
                | r_boolean
                | r_date
                | r_time
                ;

            DECLARACION.Rule = id + LISTA_IDS_ARR + ptocoma //Estudiante @est;
                | id + LISTA_IDS_ARR + igual + r_new + id + ptocoma // Estudiante @est = new Estudiante;
                | id + LISTA_IDS_ARR + igual + llavizq + LISTA_EXPRESIONES + llavder + ptocoma //Estudiante @est = {valores};
                | TIPO_DATO + LISTA_IDS_ARR + igual + EXPRESION + ptocoma //int @carnet = cualquiercosa; - primitivos ya
                | TIPO_DATO + LISTA_IDS_ARR + ptocoma //int @carnet; - ya 
                ;

            ASIGNACION.Rule = arroba + id + igual + r_new + id + ptocoma //@est = new Estudiante;
                | arroba + id + igual + llavizq + LISTA_EXPRESIONES + llavder + ptocoma // @est = new {valores};
                | arroba + id + igual + EXPRESION + ptocoma //@var = 5+"hola"; - primitivos ya
                | arroba + id + ACCESOS_OBJETO + igual + EXPRESION + ptocoma //@obj.atr.atr = 5 +3; - ya
               ;


            EXPRESION.Rule = EXPRESION_ARITMETICA
                | LITERAL //ya
                | ACCESO_OBJETO //ya
                ;

            LITERAL.Rule = entero //ya
                | _decimal //ya
                | arroba + id //ya
                | cadena //ya
                | r_true //ya
                | r_false //ya
                ;

            EXPRESION_ARITMETICA.Rule = EXPRESION + mas + EXPRESION //ya
                | EXPRESION + menos + EXPRESION //ya
                | EXPRESION + por + EXPRESION //ya
                | EXPRESION + div + EXPRESION //ya
                ;

            LISTA_EXPRESIONES.Rule = MakePlusRule(LISTA_EXPRESIONES, coma, EXPRESION); //5,"hola",true

            ACCESO_OBJETO.Rule = arroba + id + ACCESOS_OBJETO //@est.accesos - ya
                ;

            ACCESOS_OBJETO.Rule = MakePlusRule(ACCESOS_OBJETO, ACCESO); // .atributo.otroAtributo.otroAtributo - ya

            ACCESO.Rule = punto + id; // .atributo - ya

            ALTER_TYPE.Rule = r_alter + r_type + id + r_add + parizq + LISTA_ATRIBUTOS + parder + ptocoma //add campo
                | r_alter + r_type + id + r_delete + parizq + LISTA_IDS + parder + ptocoma //delete campo
                ;

            DELETE_TYPE.Rule = r_delete + r_type + id + ptocoma; //delete type Estudiante ;

            LISTA_IDS.Rule = MakePlusRule(LISTA_IDS, coma, id); // id, otro_id, otro_id

            LISTA_IDS_ARR.Rule = MakePlusRule(LISTA_IDS_ARR, coma, ID_ARR); //@id, @otro_id, @otro_id

            ID_ARR.Rule = arroba + id; // @id


            /*** PRECEDENCIAS ***/
            RegisterOperators(5, Associativity.Left, mas, menos);
            RegisterOperators(6, Associativity.Left, por, div);

            /*** SIMBOLOS QUE NO ME SON DE UTILIDAD EN EL ARBOL ***/
            MarkPunctuation(
                ptocoma, parizq, parder, arroba, igual, llavizq, llavder, coma, punto,
                r_create, r_type, r_if, r_not, r_new, r_alter, r_delete, r_add,
                r_print
            );

            /*** NODOS QUE NO ME SON DE UTILIDAD EN EL ARBOL ***/
            MarkTransient(
                INICIO, INSTRUCCION, TIPO_DATO, ID_ARR, EXPRESION
            );


            this.Root = INICIO;

            #endregion
        }
    }
}
