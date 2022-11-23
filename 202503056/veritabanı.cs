using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace _202503056
{

    //değişken tanımları
    internal class veritabanı
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader dr;
        static SqlDataAdapter da;
        static DataSet ds;

        public static string sqlbaglantı()
        {
            
            
          string  SqlCon = @"Data Source=localhost\SQLEXPRESS;Initial Catalog =pizza_otomasyon;Integrated Security=True "; 
          
            return SqlCon;
        
        } 
            
           
            

           //doldurma fonksiyonu
        public static DataGridView doldur(DataGridView gridim, string sqlSelectsorgu)
        {
            con = new SqlConnection(sqlbaglantı());
            da = new SqlDataAdapter(sqlSelectsorgu, con);
            ds = new System.Data.DataSet();
            con.Open();

            da.Fill(ds, sqlSelectsorgu);

            gridim.DataSource = ds.Tables[sqlSelectsorgu];
            return gridim;
        }
        // şifreleme fonksiyonu
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



        public static void tercih(string sql)
        {
            con = new SqlConnection(sqlbaglantı());
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }


        


        // bağlantı kurma fonksiyou

        public static bool baglantı(string kullanici, string sifre)
        {
            string sor = "select * from tbl_login where kullanici=@kul and sifre=@sif";
            con = new SqlConnection(sqlbaglantı());
            cmd = new SqlCommand(sor, con);
            cmd.Parameters.AddWithValue("@kul", kullanici);
            cmd.Parameters.AddWithValue("@sif", veritabanı.MD5Sifrele(sifre));

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
            

         }
    }
}