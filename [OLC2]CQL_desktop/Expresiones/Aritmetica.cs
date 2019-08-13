using _OLC2_CQL_desktop.Arbol;
using System;

namespace _OLC2_CQL_desktop.Expresiones
{


    class Aritmetica : IExpresion
    {
        readonly IExpresion opIzq;
        readonly Operaciones operacion;
        readonly IExpresion opDer;

        public Aritmetica(IExpresion opIzq, Operaciones operacion, IExpresion opDer)
        {
            this.opIzq = opIzq;
            this.operacion = operacion;
            this.opDer = opDer;
        }

        public Aritmetica(IExpresion opIzq, Operaciones operacion)
        {
            this.opIzq = opIzq;
            this.operacion = operacion;
        }


        public Tipos GetTipo(Entorno e)
        {
            Tipos tipo1 = opIzq.GetTipo(e);
            //Operaciones binarias
            if (opDer != null)
            {
                Tipos tipo2 = opDer.GetTipo(e);
                if(tipo1.Equals(Tipos.STRING) || tipo2.Equals(Tipos.STRING))
                {
                    return Tipos.STRING;
                }
                if(tipo1.Equals(Tipos.BOOLEAN) || tipo2.Equals(Tipos.BOOLEAN))
                {
                    return Tipos.NULL;
                }
                if (tipo1.Equals(Tipos.DOUBLE) || tipo2.Equals(Tipos.DOUBLE))
                {
                    return Tipos.DOUBLE;
                }
                if (tipo1.Equals(Tipos.INT) || tipo2.Equals(Tipos.INT))
                {
                    return Tipos.INT;
                }
            }
            //Operaciones unarias
            if (tipo1.Equals(Tipos.DOUBLE))
            {
                return Tipos.DOUBLE;
            }
            if (tipo1.Equals(Tipos.INT))
            {
                return Tipos.INT;
            }
            //Operaciones invalidas
            return Tipos.NULL;
        }

        public object GetValor(Entorno e)
        {
            object val1 = opIzq?.GetValor(e);
            object val2 = opDer?.GetValor(e);

            if(val1 != null && val2 != null)
            {
                Tipos superTipo = GetTipo(e);
                Tipos t1 = opIzq.GetTipo(e);
                Tipos t2 = opDer.GetTipo(e);

                switch (operacion)
                {
                    case Operaciones.SUMA:
                        if (superTipo.Equals(Tipos.STRING))
                        {
                            return val1.ToString() + val2.ToString();
                        }
                        if (superTipo.Equals(Tipos.DOUBLE))
                        {
                            return Convert.ToDouble(val1) + Convert.ToDouble(val2);
                        }
                        if (superTipo.Equals(Tipos.INT))
                        {
                            return Convert.ToInt32(val1) + Convert.ToInt32(val2);
                        }
                        Console.WriteLine("Error al sumar los tipos " + t1 + " y " + t2);
                        break;
                    case Operaciones.RESTA:
                        if (superTipo.Equals(Tipos.DOUBLE))
                        {
                            return Convert.ToDouble(val1) - Convert.ToDouble(val2);
                        }
                        if (superTipo.Equals(Tipos.INT))
                        {
                            return Convert.ToInt32(val1) - Convert.ToInt32(val2);
                        }
                        Console.WriteLine("Error al restar los tipos " + t1 + " y " + t2);
                        break;
                    case Operaciones.MULTIPLICACION:
                        if (superTipo.Equals(Tipos.DOUBLE))
                        {
                            return Convert.ToDouble(val1) * Convert.ToDouble(val2);
                        }
                        if (superTipo.Equals(Tipos.INT))
                        {
                            return Convert.ToInt32(val1) * Convert.ToInt32(val2);
                        }
                        Console.WriteLine("Error al multiplicar los tipos " + t1 + " y " + t2);
                        break;
                    case Operaciones.DIVISION:
                        if (superTipo.Equals(Tipos.DOUBLE))
                        {
                            double divisor = Convert.ToDouble(val2);
                            if(divisor == 0.0)
                            {
                                Console.WriteLine("No se permite la division entre 0");
                                break;
                            }
                            return Convert.ToDouble(val1) / divisor;
                        }
                        if (superTipo.Equals(Tipos.INT))
                        {
                            int divisor = Convert.ToInt32(val2);
                            if(divisor == 0)
                            {
                                Console.Write("No se permite la division entre 0");
                                break;
                            }
                            return Convert.ToInt32(val1) / divisor;
                        }
                        Console.WriteLine("Error al multiplicar los tipos " + t1 + " y " + t2);
                        break;
                }
            }
            return null;
        }
    }
}
