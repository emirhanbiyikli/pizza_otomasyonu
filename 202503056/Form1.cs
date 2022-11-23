using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace _202503056
{
    public partial class Form1 : Form
    {
        // DEĞİŞKEN TANIMLARI
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        SqlDataAdapter da;
        DataSet ds;
       // public static string SqlCon =@"Data Source=localhost\SQLEXPRESS;Initial Catalog =pizza_otomasyon;Integrated Security=True ";

       
        
        
             // DATAGRİDİ DOLDURMA FONKSİYONU
        void doldur()
        {
            con = new SqlConnection(veritabanı.sqlbaglantı());
            da = new SqlDataAdapter("Select * from tbl_login", con);
            ds = new DataSet();
            con.Open();

            da.Fill(ds, "tbl_login");

            dataGridView1.DataSource = ds.Tables ["tbl_login"];
            con.Close();
        }
            // temizleme fonsiyonu
           void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
             

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            veritabanı.doldur(dataGridView1, "Select * from tbl_login");
            this.Hide();
            Form2 a = new Form2();
            a.Show();
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
                  //VERİ TABANINDAKİ SUTUNLARI SEÇME İŞLEMİ.
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString() ;
            dateTimePicker1.Text= dataGridView1.CurrentRow.Cells[3].Value.ToString();   
        }
                // ŞİFRELEME FONSİYONU
        public static string MD5Sifrele(string SifrelenecekMetin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(SifrelenecekMetin);
            dizi = md5.ComputeHash(dizi);
            StringBuilder sb = new StringBuilder();
            foreach (byte item in dizi)
                sb.Append(item.ToString("x2").ToLower());
            return sb.ToString();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void kAYITToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
                // EKLEME FONKSİYONU
        private void button3_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(veritabanı.sqlbaglantı());
            string sql = "insert into tbl_login(kullanici, sifre, tarih) values (@kul, @sif, @tar)";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@kul", textBox2.Text);
            cmd.Parameters.AddWithValue("@sif", veritabanı.MD5Sifrele(textBox3.Text));
            cmd.Parameters.AddWithValue("@tar", DateTime.Now);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
            doldur();
            clear();

        }
      
              //silme fonsiyonu
        private void button2_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(veritabanı.sqlbaglantı());
            string sql = "delete from tbl_login where kullanici=@kul and sifre=@sif and kID=@ıd";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@kul", textBox2.Text);
            cmd.Parameters.AddWithValue("@sif", textBox3.Text);
            cmd.Parameters.AddWithValue("@ıd", Convert.ToInt32(textBox1.Text));
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
            doldur();
            clear();    
        }
             // güncelleme fonsiiyonu
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(veritabanı.sqlbaglantı());
            string sql = "update tbl_login set sifre=@sif where kullanici=@kul and kID=@ıd";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@kul", textBox2.Text);
            cmd.Parameters.AddWithValue("@sif", veritabanı.MD5Sifrele(textBox3.Text));
            cmd.Parameters.AddWithValue("@ıd", Convert.ToInt32(textBox1.Text));
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
            doldur();
            clear();
        }
    }
 }

