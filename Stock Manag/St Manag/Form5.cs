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

namespace St_Manag
{
    public partial class Form5 : Form
    {
        SqlConnection cn = new SqlConnection(@"data source = localhost; initial catalog = STManag; integrated security = true;");

        public Form5()
        {
            InitializeComponent(); AllAfficher();
        }

        private void AllAfficher()
        {
            dataGridView1.Rows.Clear();
            string req = "select * from DB_ST_M where Date_Time_Update>'1900' order by Date_Time_Update desc";
            SqlCommand cmd = new SqlCommand(req, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5),dr.GetValue(6));
            }
            cn.Close();
        }

        private void Afficher()
        {
            dataGridView1.Rows.Clear();
            string req = "select * from DB_ST_M where N_Palette='" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(req, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5), dr.GetValue(6));
            }
            cn.Close();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("update DB_ST_M set Location='" + comboBox1.Text + "', Date_Time_Update=getdate() where  ID='" + textBox2.Text + "'", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            Afficher();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void TextChanged2(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("select N_Palette from DB_ST_M", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            AutoCompleteStringCollection autodata = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                autodata.Add(dr.GetString(0));
            }
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = autodata;
            cn.Close();
            Afficher();
        }
    }
}
