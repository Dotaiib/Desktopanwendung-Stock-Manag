﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace St_Manag
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"data source = localhost; initial catalog = STManag; integrated security = true;");
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            SqlCommand cmd = new SqlCommand("select * from Enter where Name like '" + textBox1.Text + "' and PWD like '" + textBox2.Text + "'", cn);
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("User or Password Incorrecte");
            }
            textBox1.Clear();
            textBox2.Clear();
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txtchanged_box(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "")
            { button1.Enabled = true; }
            else { button1.Enabled = false; }
        }
    }
}
