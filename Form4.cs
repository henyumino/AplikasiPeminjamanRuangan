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

namespace AplikasiPeminjamanRuangan
{
    public partial class Form4 : Form
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        public Form4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            JadwalGridView();
        }

        private void JadwalGridView()
        {
            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            conn.Open();
            string ssql = "select * from tb_pinjam";
            cmd = new SqlCommand(ssql, conn);
            SqlDataReader Rdr = cmd.ExecuteReader();


            if (Rdr.HasRows)
            {
                dataGridView1.Columns.Add("Kode Ruangan", "Kode Ruangan");
                dataGridView1.Columns.Add("Nama Peminjam", "Nama Peminjam");
                dataGridView1.Columns.Add("Mulai", "Mulai");
                dataGridView1.Columns.Add("Selesai", "Selesai");
                dataGridView1.Columns.Add("Lama Meminjam", "Lama Meminjam");


                while (Rdr.Read())
                {
                    int i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Rdr["kode_ruangan"].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = Rdr["atas_nama"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = Rdr["mulai"].ToString();
                    

                    int durasi = Convert.ToInt32(Rdr["durasi"]);

                    DateTime mulai = DateTime.Parse(Rdr["mulai"].ToString());
                    DateTime incjam;
                    if (durasi == 2)
                    {

                        incjam = mulai.AddHours(2);
                    }
                    else
                    {
                        incjam = mulai.AddHours(4);
                    }


                    dataGridView1.Rows[i].Cells[3].Value = incjam.ToString();
                    dataGridView1.Rows[i].Cells[4].Value = Rdr["durasi"].ToString() + " Jam";


                }

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
               
            }





            conn.Close();
        }
    }
}
