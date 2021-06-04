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
using Microsoft.VisualBasic;


namespace AplikasiPeminjamanRuangan
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }


        public void IsiGridView()
        {

            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            conn.Open();
            string ssql = "select * from tb_ruangan";
            cmd = new SqlCommand(ssql, conn);
            SqlDataReader Rdr = cmd.ExecuteReader();

            


            if (Rdr.HasRows)
            {
                dataGridView1.Columns.Add("Kode Ruangan", "Kode Ruangan");
                dataGridView1.Columns.Add("Nama Ruangan", "Nama Ruangan");
                dataGridView1.Columns.Add("Kapasitas", "Kapasitas");
                dataGridView1.Columns.Add("Jenis Ruangan", "Jenis Ruangan");
                dataGridView1.Columns.Add("Status", "Status");

                while (Rdr.Read())
                {
                    int i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Rdr["kode_ruangan"].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = Rdr["nama_ruangan"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = Rdr["kapasitas"].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = Rdr["jenis_ruangan"].ToString();

                    if(Convert.ToInt32(Rdr["status"]) == 0)
                    {
                        dataGridView1.Rows[i].Cells[4].Value = "Tidak Digunakan";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[4].Value = "Digunakan";
                    }
                    
                }

            }


            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);
            conn.Open();

            IsiGridView();
        }


        public void refreshGrid()
        {
            IsiGridView();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Owner = this;
            form3.ShowDialog();
        }
    }
}
