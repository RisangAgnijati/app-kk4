using System.Drawing;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;


namespace app_kk3
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = Connection.getConnection();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (login(textBox1.Text, textBox2.Text))
            {
                Form3 fm = new Form3();
                fm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tidak berhasil");
            }
        }

        private Boolean login(String sUsername, String sPassword)
        {
            string SQL = "SELECT username, password FROM db_login";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(SQL, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if ((sUsername == reader.GetString(0) && (sPassword == reader.GetString(1))))
                {
                    conn.Close();
                    return true;
                }
            }
            conn.Close();
            return false;

        }
    }
}