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
    public partial class Form3 : Form
    {

        SqlConnection cn = new SqlConnection(@"data source = localhost; initial catalog = STManag; integrated security = true;");

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

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                SqlCommand cmd = new SqlCommand("select * from DB_ST_M where Lot like '" + textBox1.Text + "' or Location like '" + textBox1.Text + "' or N_Palette like '" + textBox1.Text + "' or PName like '" + textBox1.Text + "'", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5));
                }
                cn.Close();
            }
            else { Afficher(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
