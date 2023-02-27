using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace app_kk3
{
    public partial class Form3 : Form
    {
        MySqlConnection conn = Connection.getConnection();
        DataTable dataTable = new DataTable();

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            fillDataTable();

            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            button2.Enabled = false;
        }

        public DataTable getDataBarang()
        {
            dataTable.Reset();
            dataTable = new DataTable();
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM db_barang", conn))
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
            }
            return dataTable;
        }

        private void dgv_barang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int id = Convert.ToInt32(dgv_barang.CurrentCell.RowIndex.ToString());
                textBox4.Text = dgv_barang.Rows[id].Cells[1].Value.ToString();
                textBox5.Text = dgv_barang.Rows[id].Cells[2].Value.ToString();
                textBox6.Text = dgv_barang.Rows[id].Cells[3].Value.ToString();

                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                button2.Enabled = true;

            }
        }

        public void fillDataTable()
        {
            dgv_barang.DataSource = getDataBarang();

            DataGridViewButtonColumn colEdit = new DataGridViewButtonColumn();
            colEdit.UseColumnTextForButtonValue = true;
            colEdit.Text = "Edit";
            colEdit.Name = "";
            dgv_barang.Columns.Add(colEdit);

            DataGridViewButtonColumn colDelete = new DataGridViewButtonColumn();
            colDelete.UseColumnTextForButtonValue = true;
            colDelete.Text = "Delete";
            colDelete.Name = "";
            dgv_barang.Columns.Add(colDelete);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;
            //conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO db_barang(nm_barang, price, stoc) VALUES(@nm_barang, @price, @stoc)";
                cmd.Parameters.AddWithValue("@nm_barang", textBox1.Text);
                cmd.Parameters.AddWithValue("@price", textBox2.Text);
                cmd.Parameters.AddWithValue("@stoc", textBox3.Text);
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                conn.Close();

                dgv_barang.Columns.Clear();
                dataTable.Clear();
                fillDataTable();

                MessageBox.Show("Anda berhasil menambahkan data");
            }


            catch (Exception ex)
            {
                // ex.Message.EndsWith("");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            MySqlCommand cmd;
            //conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE db_barang SET nm_barang = @nm_barang, price = @price, stoc = @stoc WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", textBox7.Text);
                cmd.Parameters.AddWithValue("@nm_barang", textBox4.Text);
                cmd.Parameters.AddWithValue("@price", textBox5.Text);
                cmd.Parameters.AddWithValue("@stoc", textBox6.Text);
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;

                resetId();

                conn.Close();

                dgv_barang.Columns.Clear();
                dataTable.Clear();
                fillDataTable();

                MessageBox.Show("Anda berhasil mengupdate data");
            }


            catch (Exception ex)
            {
                // ex.Message.EndsWith("");
            }

        }

        private void dgv_barang_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int id = Convert.ToInt32(dgv_barang.CurrentCell.RowIndex.ToString());
                textBox7.Text = dgv_barang.Rows[id].Cells[0].Value.ToString();
                textBox4.Text = dgv_barang.Rows[id].Cells[1].Value.ToString();
                textBox5.Text = dgv_barang.Rows[id].Cells[2].Value.ToString();
                textBox6.Text = dgv_barang.Rows[id].Cells[3].Value.ToString();

                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                button2.Enabled = true;

            }

            if (e.ColumnIndex == 5)
            {
                int id = Convert.ToInt32(dgv_barang.CurrentCell.RowIndex.ToString());

                MySqlCommand cmd;
                try
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM db_barang WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", dgv_barang.Rows[id].Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();

                    resetId();

                    conn.Close();

                    dgv_barang.Columns.Clear();
                    dataTable.Clear();
                    fillDataTable();

                    MessageBox.Show("Anda berhasil menghapus data");
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void searchData(string ValueToFind)
        {
            string searchQuery = "SELECT * FROM db_barang WHERE CONCAT(nm_barang, price, stoc) LIKE '%" + ValueToFind + "%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgv_barang.DataSource = table;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            searchData(textBox8.Text);
        }

        public void resetId()
        {
            MySqlScript script = new MySqlScript(conn, "SET @num := 0;" +
                "UPDATE db_barang SET id = @num := (@num +1);" +
                "ALTER TABLE db_barang AUTO_INCREMENT = 1;");
            script.Execute();

            conn.Clone();
        }
    }
}
