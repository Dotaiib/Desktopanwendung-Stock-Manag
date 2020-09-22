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
    public partial class Form4 : Form
    {
        SqlConnection cn = new SqlConnection(@"data source = localhost; initial catalog = STManag; integrated security = true;");

        public Form4()
        {
            InitializeComponent();
        }

        private void Afficher()
        {

            dataGridView1.Rows.Clear();
            string req = "select * from DB_ST_M order by Date_Time desc";
            SqlCommand cmd = new SqlCommand(req, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5));
            }
            cn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || comboBox1.SelectedIndex != -1)
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                SqlCommand cmd = new SqlCommand("select * from DB_ST_M where Location like '" + comboBox1.Text + "' and N_Palette like '" + textBox1.Text + "' ", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5));
                }
                cn.Close();
            }
            else { MessageBox.Show("Entrée une Valeur"); }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("insert into DB_ST_M_SRT select Id, PName, Lot, N_Palette, Location, Date_Time, Date_Time_Update, getdate(), Symbl='Out' from DB_ST_M where Location = '" + comboBox1.Text + "' and N_Palette = '" + textBox1.Text + "'", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            dataGridView1.Rows.Clear();
            textBox1.Clear(); comboBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void textBox1Changed(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("select N_Palette from DB_ST_M WHERE LOCATION = '" + comboBox1.Text + "'", cn);
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
        }
    }
}
