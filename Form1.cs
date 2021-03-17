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


namespace VeritabaniProjesi
{
    public partial class Form1 : Form
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-MCPDMU0\\SQLEXPRESS;Initial Catalog=veritabani;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
       // public static string veri; böyle yazınca veri'ye form2den de ulaşabiliyoruz

        private void Form1_Load(object sender, EventArgs e)
        {
            cb();
        }

        public void cb()
        {   //comboboxta müşteri adlarının listesi
            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = baglanti.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select musteriadi from Tmusteri";
            cmd.ExecuteNonQuery();
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);
            foreach (DataRow dr in tb.Rows)
            {
                comboBox1.Items.Add(dr["musteriadi"].ToString());
            }

            baglanti.Close();

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {   //yeni müşteri girişi
            baglanti.Open();
            SqlCommand cmd = baglanti.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Tmusteri(musteriadi,adres) values('" + textBox1.Text + "', '" + textBox2.Text + "')";   
            cmd.ExecuteNonQuery();
            baglanti.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            cb();
            MessageBox.Show("Müşteri Kaydedildi.");
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {   //formlar arası geçişi sağlar
            Form1 gecis = new Form1();
            gecis.Close();
            Form2 form = new Form2();
            form.Show();
            this.Hide();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {   //seçilen müşterinin bilgilerini texboxta gösteriyor
            baglanti.Open();
            SqlCommand cmd = baglanti.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Tmusteri where musteriadi= '" + comboBox1.SelectedItem.ToString() + "'";
            cmd.ExecuteNonQuery();
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);
            foreach (DataRow dr in tb.Rows)
            {
                textBox3.Text = dr["musteriID"].ToString();
                textBox4.Text = dr["musteriadi"].ToString();
                textBox5.Text = dr["adres"].ToString();
            }

            baglanti.Close();
        }
    }
    }

