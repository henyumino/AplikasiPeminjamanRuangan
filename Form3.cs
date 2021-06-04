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
    public partial class Form3 : Form
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        public Form3()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            PinjamGridView();

            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);

            conn.Open();

            string ssql = "select * from tb_ruangan where status=0";
            cmd = new SqlCommand(ssql, conn);
            SqlDataReader data = cmd.ExecuteReader();

            while (data.Read())
            {
                comboBox2.Items.Add(data["kode_ruangan"].ToString());
            }

            conn.Close();
           
        }

        public void PinjamGridView()
        {

            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

         
            conn.Open();
            string ssql = "select * from tb_ruangan where status=0";
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

                    if (Convert.ToInt32(Rdr["status"]) == 0)
                    {
                        dataGridView1.Rows[i].Cells[4].Value = "Tidak Digunakan";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[4].Value = "Digunakan";
                    }

                }

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }





            



            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);


            if (string.IsNullOrEmpty(textBox1.Text) || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("tidak boleh kosong");
            }
            else
            {
                string tanggal = dateTimePicker1.Value.ToString();
                int durasi = Convert.ToInt32(comboBox1.SelectedItem);
                string an = textBox1.Text;
                string kr = comboBox2.Text;

                string ssql = "insert into tb_pinjam values('" + kr + "','" + an + "','" + tanggal + "'," + durasi + ")";
                conn.Open();
                cmd = new SqlCommand(ssql, conn);
                cmd.ExecuteNonQuery();

                string ssql2 = "update tb_ruangan set status=1 where kode_ruangan='" + kr + "'";

                cmd = new SqlCommand(ssql2, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("berhasil");

                conn.Close();

                textBox1.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;

                PinjamGridView();

                ((Form1)Owner).IsiGridView();
            }

            

        }

       
    }
}
