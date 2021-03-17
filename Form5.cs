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
    public partial class Form5 : Form
    {
        public static SqlConnection ba = new SqlConnection("Data Source=DESKTOP-MCPDMU0\\SQLEXPRESS;Initial Catalog=veritabani;Integrated Security=True");
        public Form5()
        {
            InitializeComponent();
        }

        public void ccc()
        {
            comboBox1.Items.Clear();
            ba.Open();
            SqlCommand com = ba.CreateCommand();
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

            ba.Close();

        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            ccc();
        }
        public static int oran;
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                oran = int.Parse(textBox1.Text);
            }
        }

        public static float maliyet, karlisatis;
        private void Button3_Click(object sender, EventArgs e)
        {
            ba.Open();
            SqlCommand al = new SqlCommand("select toplammaliyet from Tkimyasal where urunadi = '" + comboBox1.SelectedItem.ToString() + "' ", ba);
            SqlDataReader verioku = al.ExecuteReader();
            while (verioku.Read())
            {
                maliyet = Convert.ToInt32(verioku["toplammaliyet"]);
            }

            al.Dispose();
            verioku.Close();
            ba.Close();
            oran = 1 / 10;
            karlisatis = maliyet + (maliyet*oran);
        }
    }
}
