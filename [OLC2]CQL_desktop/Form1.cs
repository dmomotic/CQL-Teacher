using _OLC2_CQL_desktop.Inteprete;
using Irony.Parsing;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace _OLC2_CQL_desktop
{
    public partial class Form1 : Form
    {

        public static string salida = "";

        public static void AgregarSalida(String salida)
        {
            salida += salida + "\n";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            openFileDialog1.Title = "Seleccionar archivo de entrada";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtEntrada.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void btnLimpair_Click(object sender, EventArgs e)
        {
            txtEntrada.Clear();
            txtSalida.Clear();
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            if (!txtEntrada.Text.Equals(string.Empty))
            {
                Gramatica grammar = new Gramatica();
                LanguageData lenguaje = new LanguageData(grammar);
                Parser parser = new Parser(lenguaje);
                ParseTree arbol = parser.Parse(txtEntrada.Text);
                if (arbol.ParserMessages.Count == 0)
                {
                    MessageBox.Show("Cadena valida");
                    Grafica j = new Grafica();
                    j.graficar(arbol.Root);
                }
                else
                {
                    MessageBox.Show("Existen errores en la entrada");
                }
            }
        }
    }
}
