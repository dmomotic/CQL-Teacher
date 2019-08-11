using Irony.Parsing;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace _OLC2_CQL_desktop.Inteprete
{
    class Grafica
    {
        private int index;
        private string rutaExeDot = "C:\\Program Files (x86)\\Graphviz2.38\\bin\\dot.exe";

        public void graficar(ParseTreeNode nodo)
        {
            StreamWriter archivo = new StreamWriter("ArbolSintactico.dot");
            string contenido = "graph G {";
            contenido += "node [shape = egg];";
            index = 0;
            definirNodos(nodo, ref contenido);
            index = 0;
            enlazarNodos(nodo, 0, ref contenido);
            contenido += "}";
            archivo.Write(contenido);
            archivo.Close();
            DialogResult verImagen = MessageBox.Show("¿Desea visualizar el AST de la cadena ingresada?", "Grafica AST", MessageBoxButtons.YesNo);
            if (verImagen == DialogResult.Yes)
            {

                ProcessStartInfo startInfo = new ProcessStartInfo(rutaExeDot);
                startInfo.Arguments = "-Tpng ArbolSintactico.dot -o ArbolSintactico.png";
                Process.Start(startInfo);
                Thread.Sleep(4000);
                startInfo.FileName = "ArbolSintactico.png";
                Process.Start(startInfo);
            }
        }

        public void definirNodos(ParseTreeNode nodo, ref string contenido)
        {
            if (nodo != null)
            {
                contenido += "node" + index.ToString() + "[label = \"" + nodo.ToString() + "\", style = filled, color = lightblue];";
                index++;

                foreach (ParseTreeNode hijo in nodo.ChildNodes)
                {
                    definirNodos(hijo, ref contenido);
                }
            }
        }

        public void enlazarNodos(ParseTreeNode nodo, int actual, ref string contenido)
        {
            if (nodo != null)
            {
                foreach (ParseTreeNode hijo in nodo.ChildNodes)
                {
                    index++;
                    contenido += "\"node" + actual.ToString() + "\"--" + "\"node" + index.ToString() + "\"";
                    enlazarNodos(hijo, index, ref contenido);
                }
            }
        }
    }
}
