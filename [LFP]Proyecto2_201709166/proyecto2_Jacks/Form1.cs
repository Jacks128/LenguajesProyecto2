using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace proyecto2_Jacks
{
    public partial class Form1 : Form
    {
        private static string filefule;
        Boolean abrir = false;
        Analizador analizador;
        Analizador_Sintac analizadorsintax;
        Manejador token; public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            if (guardar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter archivo = File.CreateText(guardar.FileName);
                archivo.Write(rtbTexto.Text);
                archivo.Close();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscar = new OpenFileDialog();
            abrir = true;

            if (buscar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filefule = buscar.FileName.ToString();
                string Textfile = File.ReadAllText(filefule);
                rtbTexto.Text = Textfile;
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jackeline Alexandra Benitez Benitez   Carnet 201709166", "Acerca de",
         MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private void generarTraducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String entry = rtbTexto.Text;
           analizador /*lex*/ = new Analizador();

            analizador.Scanear(entry);
            analizador.TokenHTML();
           // sintax.ErrorSintaxHTML();



        
            //List<Token> tokencitos=  analizador.Scanear(entry);

            //List<Token> lex = analizador.Scanear(entry);
            // analizador.addLast
            Manejador.Obtenerllamado().getsalidaToken().Add(new Token("ULTIMO", Token.Tipo.ULTIMO, 0, 0, 1000));


            Analizador_Sintac sintax = new Analizador_Sintac();
            sintax.parsear(Manejador.Obtenerllamado().getsalidaToken());
            rtbTraduc.Text = sintax.tra();
            MessageBox.Show("Analisis Sintactico concluido Satisfactoriamente");


               // Mostrar();
                //LlenarLista();
            
        }

        private void limpiarEditorDeTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbTexto.Text = "";
            rtbTraduc.Text = "";
            Manejador.Obtenerllamado().getsalidaToken().Clear();
            Manejador.Obtenerllamado().getsalidaError().Clear();

        }

        private void tokensReconocidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            analizador.TokenHTML();
        }

        private void erroresLexicoYSintacticosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            analizador.ErrorHTML();
        }
    }
}
