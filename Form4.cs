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
    public partial class Form4 : Form
    {
        public static SqlConnection bag = new SqlConnection("Data Source=DESKTOP-MCPDMU0\\SQLEXPRESS;Initial Catalog=veritabani;Integrated Security=True");
        public Form4()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {   //datagrid verilerini tablolara ekler
            bag.Open();
            SqlCommand cmd = bag.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Tbkimyasal(kimyasal,stok) values('" + dataGridView1.Rows[0].Cells[0].Value + "', '" + dataGridView1.Rows[0].Cells[1].Value + "')";
            cmd.ExecuteNonQuery();

            SqlCommand cm = bag.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "insert into Tbilesenler(hammadde,miktar) values('" + dataGridView2.Rows[0].Cells[0].Value + "', '" + dataGridView2.Rows[0].Cells[1].Value + "')";
            cm.ExecuteNonQuery();
            cm.CommandText = "insert into Tbilesenler(hammadde,miktar) values('" + dataGridView2.Rows[1].Cells[0].Value + "', '" + dataGridView2.Rows[1].Cells[1].Value + "')";
            cm.ExecuteNonQuery();

            SqlCommand cmn = bag.CreateCommand();
            cmn.CommandType = CommandType.Text;
            cmn.CommandText = "insert into Tiscilik(isim,iscilik) values('" + dataGridView3.Rows[0].Cells[0].Value + "', '" + dataGridView3.Rows[0].Cells[1].Value + "')";
            cmn.ExecuteNonQuery();

            bag.Close();

        }
    }
}
