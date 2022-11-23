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
using System.Data.SqlClient;

namespace _202503056
{
    public partial class Form2 : Form
    {
        public static string Kullanici = "";
       
        public Form2()
        {
            InitializeComponent();
        }
                  //baglantı fonksiyonu
        private void button1_Click(object sender, EventArgs e)
        {
            if (veritabanı.baglantı(textBox1.Text, textBox2.Text))
            {
                MessageBox.Show("GİRİŞ BAŞARILI");

                this.Hide();
                Kullanici = textBox1.Text;
                Form3 a = new Form3();
                a.Show();

            }
            else
            {
                MessageBox.Show("HATALI GİRİŞ ");
            }
            


        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }

   
}
