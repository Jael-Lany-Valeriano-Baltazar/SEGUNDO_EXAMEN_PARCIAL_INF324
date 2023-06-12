using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        DataTable dato = null;
        private SqlConnection conexion = new SqlConnection("Data Source=localhost;Initial Catalog=DB_TEXTURA;Integrated Security=True");


        public Form2()
        {
            InitializeComponent();
        }

        
        private void button9_Click(object sender, EventArgs e)
        {
            dato = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT ID, NOM_COLOR, R1,G1,B1 FROM RGB_COLOR ORDER BY ID DESC";
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            dat.Fill(dato);
            dataGridView1.DataSource = dato;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            dato = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT ID, NOM_COLOR, R1,G1,B1 FROM RGB_COLOR ORDER BY ID DESC"; 
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            dat.Fill(dato);
            dataGridView1.DataSource = dato;
        }
    }
}
