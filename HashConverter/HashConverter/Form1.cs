using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace HashConverter
{
    public partial class Form1 : Form
    {
        public static string convert = "";
        public static String pfad1 = @"C:\HashConverter\";
        public static String pfad2 = ".ini";

        public Form1()
        {
            InitializeComponent();
        }

        public static string GetMD5Hash(string TextToHash)
        {
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return string.Empty;
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            return System.BitConverter.ToString(result);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbtext.Text != "")
            {
                convert = GetMD5Hash(tbtext.Text);
                tbhash.Text = convert;
            }
            else
            MessageBox.Show("Sie haben keinen Text eingegeben!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbhash.Text == String.Empty)
            {
                MessageBox.Show("Es wurde kein Hashcode genneriert", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            if (tbsave.Text == String.Empty)
            {
                MessageBox.Show("Sie haben keinen Dateinamen angegeben", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            if (!Directory.Exists(pfad1))
            {
                System.IO.Directory.CreateDirectory(pfad1);
            }
            else
            if (File.Exists(pfad1 + tbsave.Text + pfad2))
            {
                File.Delete(pfad1 + tbsave.Text + pfad2);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(pfad1 + tbsave.Text + pfad2, true))
            {
                file.WriteLine(tbhash.Text);
            }
            if (tbsave.Text != String.Empty)
            {
                MessageBox.Show("Sie haben den Hashcode erfoldreich gespeichert", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] eingabe = null;
            try
            {
                // einlesen der Datei in ein String-Array
                eingabe = File.ReadAllLines(pfad1 + tbload.Text + pfad2, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // Datei nicht vorhanden ...
                MessageBox.Show("Fehler beim Einlesen:\r\n", ex.Message);
                return;
            }
            tbhash.Text = eingabe[0];
            tbtext.Text = "Ein Md5 Hashcode kann nicht entschlüsselt werden deshalb ist nur der Code geladen worden!";
            MessageBox.Show("Sie haben den Hashcode erfolgreich geladen", "Laden erfolgreich", MessageBoxButtons.OK);
        }
    }
}
