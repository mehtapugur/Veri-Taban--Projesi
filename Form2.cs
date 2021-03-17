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
    public partial class Form2 : Form
    {
        public static SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-MCPDMU0\\SQLEXPRESS;Initial Catalog=veritabani;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cd();
            listele();
        }

        public void listele()
        {
            try
            {
                baglan.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter adapt = new SqlDataAdapter("select * from Tbkimyasal", baglan);
                adapt.Fill(ds); //adapt nesnesinin içini doldurur
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch (SqlException)
            {
                MessageBox.Show("Hata Oluştu..");
            }

            finally
            {
                baglan.Close();
            }

        }
        public void cd()
        {
            comboBox1.Items.Clear();
            baglan.Open();
            SqlCommand com = baglan.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "select kimyasal from Tbkimyasal";
            com.ExecuteNonQuery();
            DataTable td = new DataTable();
            SqlDataAdapter dc = new SqlDataAdapter(com);
            dc.Fill(td);
            foreach (DataRow dy in td.Rows)
            {
                comboBox1.Items.Add(dy["kimyasal"].ToString());
            }

            baglan.Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 gec = new Form2();
            gec.Close();
            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox1.Items.Clear();
            baglan.Open();
            SqlCommand com = baglan.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "select stok from Tbkimyasal where kimyasal = '" + comboBox1.SelectedItem.ToString() + "'";
            com.ExecuteNonQuery();
            DataTable td = new DataTable();
            SqlDataAdapter dc = new SqlDataAdapter(com);
            dc.Fill(td);
            foreach (DataRow dy in td.Rows)
            {
                textBox2.Text = dy["stok"].ToString();
            }
            
            baglan.Close();
        }
        public static int sayi;
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            sayi = int.Parse(textBox1.Text); //müşterinin istediği miktar
        }

        public static int stk;
        private void Button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand compare = new SqlCommand("select stok from Tbkimyasal where kimyasal = '" + comboBox1.SelectedItem.ToString() + "'", baglan);
            SqlDataReader verioku = compare.ExecuteReader();
            while (verioku.Read())
            {
                stk = Convert.ToInt32(verioku["stok"]); //üretici stoklarındaki bileşik durumu

            }

            if(sayi <= stk)
            {
                MessageBox.Show("Satın alma işlemi gerçekleştirilebilir.");
            }
            else
            {
                MessageBox.Show("Ürün stoklarımızda bulunmamaktadır lütfen üretim için bekleyiniz..");
            }

            compare.Dispose();
            verioku.Close();
            baglan.Close();
            
           // textBox4.Text = 
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form2 ge = new Form2();
            ge.Close();
            Form5 fr = new Form5();
            fr.Show();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
