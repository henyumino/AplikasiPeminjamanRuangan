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
    public partial class Form2 : Form
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
       
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);
            conn.Open();

            

            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedIndex == -1 || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("tidak bole kosong");
            }
            else
            {
                string koderuangan = textBox1.Text;
                string namaruangan = textBox2.Text;
                string jenis = comboBox1.Text;
                int kapasitas = int.Parse(textBox3.Text);
                int status = 0;

                if(jenis == "Lab")
                {
                    koderuangan = "L0" + koderuangan;
                }
                else
                {
                    koderuangan = "K0" + koderuangan;
                }

                string ssql = "insert into tb_ruangan values('" + koderuangan + "','"+namaruangan+"',"+kapasitas+",'"+jenis+"','"+status+"')";
                cmd = new SqlCommand(ssql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Berhasil menambah ruangan");
            }


            ManageGrid();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;


            ((Form1)Owner).IsiGridView();
        }


        private void ManageGrid()
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

                    if (Convert.ToInt32(Rdr["status"]) == 0)
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

        private void Form2_Load(object sender, EventArgs e)
        {
            ManageGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            textBox1.Text = selectedRow.Cells[0].Value.ToString();
            textBox2.Text = selectedRow.Cells[1].Value.ToString();
            textBox3.Text = selectedRow.Cells[2].Value.ToString();
            comboBox1.SelectedIndex = comboBox1.FindString(selectedRow.Cells[3].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);

            conn.Open();

            string id = textBox1.Text;

            string ssql = "DELETE FROM tb_ruangan WHERE kode_ruangan='"+id+"'";

            cmd = new SqlCommand(ssql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();

            ManageGrid();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;

            ((Form1)Owner).IsiGridView();

            MessageBox.Show("Berhasil menghapus");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connstring = @"Server=localhost\SQLEXPRESS;Database=dbruangan;Trusted_Connection=True;";
            conn = new SqlConnection(connstring);
            conn.Open();

            


            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedIndex == -1 || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Form tidak boleh kosong");
            }
            else
            {
                string koderuangan = textBox1.Text;
                string namaruangan = textBox2.Text;
                string jenis = comboBox1.Text;
                int kapasitas = int.Parse(textBox3.Text);
                int status = 0;

                string ssql = "update tb_ruangan set nama_ruangan = '"+namaruangan+"', jenis_ruangan='"+jenis+"', kapasitas='"+kapasitas+"' where kode_ruangan='"+koderuangan+"'";
                cmd = new SqlCommand(ssql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                ManageGrid();

                MessageBox.Show("Berhasil memperbarui ruangan");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.SelectedIndex = -1;


                ((Form1)Owner).IsiGridView();
            }
        }
    }
}
