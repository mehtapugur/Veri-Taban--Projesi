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
using System.Data.Sql;

namespace VeritabaniProjesi
{
    public partial class Form3 : Form
    {
        public static SqlConnection bagla = new SqlConnection("Data Source=DESKTOP-MCPDMU0\\SQLEXPRESS;Initial Catalog=veritabani;Integrated Security=True");
       
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cc();
            ct();
        }

        public void cc()
        {
            comboBox1.Items.Clear();
            bagla.Open();
            SqlCommand cmd = bagla.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select firmaadi from Tbtedarikci";
            cmd.ExecuteNonQuery();
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);
            foreach (DataRow dr in tb.Rows)
            {
                comboBox1.Items.Add(dr["firmaadi"].ToString());
            }

            bagla.Close();
        }

        public void ct()
        {
            comboBox2.Items.Clear();
            bagla.Open();
            SqlCommand cmd = bagla.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct hammadde from Tbhammadde";
            cmd.ExecuteNonQuery();
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);
            foreach (DataRow dr in tb.Rows)
            {
                comboBox2.Items.Add(dr["hammadde"].ToString());
            }

            bagla.Close();
        }

        public void listele() //tablo yazdirma fonksiyonu
        {     
                bagla.Open(); //kapali ise açıyor
                SqlCommand cmd = new SqlCommand(); //komut yazmamız için
                cmd.Connection = bagla;
                cmd.CommandText = "select * from Tbhammadde where hammadde = '" + comboBox2.SelectedItem.ToString() + "'  ORDER BY fiyat ASC";
                SqlDataAdapter adpr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet(); //dataset oluşturuyoruz
                adpr.Fill(ds, "Tbhammadde"); //adpr yi dolduruyoruz
                dataGridView1.DataSource = ds.Tables["Tbhammadde"]; //liste datagridviewde görüntülensin 
                dataGridView1.Columns[0].Visible = false; //tabloda hangi sütunun listelenmesini istemiyorsak
                bagla.Close(); //işlem bitince baglantiyi kapatiyoruz           
        }

        public static int mes, kon; //public oldukları için her yerden çağırılıp kullanılabilirler
        public float karg;
        public void mesafe()
        {
            bagla.Open();
            SqlCommand al = new SqlCommand("select konumID from Tbtedarikci where firmaID = '" + comboBox3.SelectedItem.ToString() + "' ", bagla);
            SqlDataReader verioku = al.ExecuteReader();
            while (verioku.Read())
            {
                mes = Convert.ToInt32(verioku["konumID"]);
            }

            al.Dispose();
            verioku.Close();

            SqlCommand ab = new SqlCommand("select Kocaeli_km from Tbkonum where konumID = '" + mes.ToString() + "' ", bagla);
            SqlDataReader oku = ab.ExecuteReader();
            while (oku.Read())
            {
                kon = Convert.ToInt32(oku["Kocaeli_km"]);
            }

            ab.Dispose();
            oku.Close();
          
            if (kon <=1000 ){

                karg = (float)kon/(float)2 ;

            } else
            {
                karg = kon * 1;
            }

            bagla.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bagla.Open();
            SqlCommand cmd = bagla.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Tbtedarikci(firmaadi,ulke,sehir) values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "')";
            cmd.ExecuteNonQuery();
            bagla.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            cc();

            MessageBox.Show("Tedarikçi Firma Kaydedildi.");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele();
            bagla.Open();
            SqlCommand cmd = bagla.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select firmaID from Tbhammadde where hammadde = (select distinct hammadde from Tbhammadde where hammadde = '" + comboBox2.SelectedItem.ToString() + "') ";
            cmd.ExecuteNonQuery();
            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);
            foreach (DataRow dr in tb.Rows)
            {
                comboBox3.Items.Add(dr["firmaID"].ToString());
            }

            bagla.Close();
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        public static int mktr;  //miktar
        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                mktr = int.Parse(textBox4.Text);
            }

        }

        public static int id, numb, sum;
        public static float top;
        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            bagla.Open();
            SqlCommand al = new SqlCommand ("select fiyat from Tbhammadde where hammadde = '" + comboBox2.SelectedItem.ToString() + "' AND  firmaID = '" + comboBox3.SelectedItem.ToString() + "'",bagla);
            SqlDataReader verioku = al.ExecuteReader();
            while (verioku.Read())
            {
                numb = Convert.ToInt32(verioku["fiyat"]);

            }
           
             al.Dispose();
            verioku.Close();
            //numb fiyat bilgisi
            sum = numb * mktr;    
            
            bagla.Close();

            mesafe();  

            top = sum + karg;

            textBox6.Text = top.ToString();
        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }
        public static float nb, yeni;

        private void Button2_Click(object sender, EventArgs e)
        {
            Form3 go = new Form3();
            go.Close();
            Form4 form = new Form4();
            form.Show();
            //this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e) // SATIN AL BUTONU
        {
            textBox5.Clear();

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }
        public float money = 0;
        private void Button4_Click(object sender, EventArgs e) // EKLE BUTONU
        {
            bagla.Open();
            SqlDataAdapter sd = new SqlDataAdapter("select hammadde, miktar, fiyat from Tbhammadde where hammadde = '" + comboBox2.SelectedItem.ToString() + "' AND  firmaID = '" + comboBox3.SelectedItem.ToString() + "'", bagla);
            DataTable tt = new DataTable();
            sd.Fill(tt);
            //dataGridView2.Rows.Clear();
            foreach(DataRow item in tt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                 dataGridView2.Rows[n].Cells[0].Value = item["hammadde"].ToString();
                 dataGridView2.Rows[n].Cells[1].Value = mktr;
                 dataGridView2.Rows[n].Cells[2].Value = sum;
                 dataGridView2.Rows[n].Cells[3].Value = karg; 
                
            }
            bagla.Close();

            money = money + top;
            textBox5.Text = money.ToString();

            bagla.Open();
            SqlCommand xy = new SqlCommand("select miktar from Tbhammadde where hammadde = '" + comboBox2.SelectedItem.ToString() + "' AND  firmaID = '" + comboBox3.SelectedItem.ToString() + "'", bagla);
            SqlDataReader verioku = xy.ExecuteReader();
            while (verioku.Read())
            {
                nb = Convert.ToInt32(verioku["miktar"]); //nb stoklardaki miktar

            }

            xy.Dispose();
            verioku.Close();


            //stokları güncelle
            SqlCommand cmd = new SqlCommand(); //komut yazmamız için
            cmd.Connection = bagla;
            //mktr = kullanıcı kaç tane almak istiyor
            yeni = nb - mktr;
            //yeni = 1;
            if (mktr < nb) //tedarikcinin hammadde tablosunu günceller
            {
                cmd.CommandText = "Update Tbhammadde Set miktar = '" + yeni.ToString() + "' where hammadde = '" + comboBox2.SelectedItem.ToString() + "' AND  firmaID = '" + comboBox3.SelectedItem.ToString() + "'";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            else if (mktr == nb)
            {
                cmd.CommandText = "delete from Tbhammadde where hammadde = '" + comboBox2.SelectedItem.ToString() + "' AND  firmaID = '" + comboBox3.SelectedItem.ToString() + "'"; // nerenin silineceği
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            // üreticinin hammadde tablosuna ekleme yapar
            //cmd.CommandText = "INSERT INTO Tbhammaddeler(hammadde,stokdurumu) VALUES('" + comboBox2.SelectedItem.ToString() + "', '" + int.Parse (textBox4.Text) + "')";
            cmd.CommandText = "INSERT INTO Tbhammaddeler(hammadde,stokdurumu) VALUES('" + comboBox2.SelectedItem.ToString() + "', '" + textBox4.Text + "')";
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            bagla.Close();

            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            textBox4.Clear();
            textBox6.Clear();

        }

    }
}
