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
    public partial class Form2 : Form
    {

        SqlConnection cn = new SqlConnection(@"data source = localhost; initial catalog = STManag; integrated security = true;");

        public Form2()
        {
            InitializeComponent(); Afficher();
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
                dataGridView1.Rows.Add( dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4));
            }
            cn.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("insert into DB_ST_M values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "', getdate(), '')", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                Afficher();
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); comboBox1.Text = "";
            }
            else { MessageBox.Show("L'emplacent est vide !!"); }
        }

        private void Txtchanged_box(object sender, EventArgs e)
        {
            //Textbox
            if(textBox1.Text != "") { textBox2.Enabled = true; }
            else { textBox2.Enabled = false; }
            if (textBox2.Text != "") { textBox3.Enabled = true; }
            else { textBox3.Enabled = false; }

            //Combobox
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text!="")
            { comboBox1.Enabled = true; }
            else { comboBox1.Enabled = false; }

            //Button
            if (comboBox1.Enabled == true)
            { button1.Enabled = true; }
            else { button1.Enabled = false; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
            Form1 f1 = new Form1();
            f1.Show();     
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void t1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void t2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private void t3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }
    }
}
