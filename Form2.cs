using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace interview_simulator
{
    public partial class Form2 : Form
    {
        string connStr = "server=localhost;port=3306;database=interview_simulator;uid=root;pwd=;";

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "");
            errorProvider1.SetError(textBox2, "");

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                string query = "SELECT * FROm user where id=1";
                MySqlCommand cmd= new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader r = cmd.ExecuteReader();
                if (r.Read())

                {
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        errorProvider1.SetError(textBox1, "this field is require");
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(textBox2.Text))
                        {
                            errorProvider1.SetError(textBox2, "this field is require");
                        }
                        else
                        {

                            if (textBox1.Text == r["username"].ToString())
                            {
                                if (textBox2.Text == r["pass"].ToString())
                                {
                                    Form1 f1 = new Form1();
                                    f1.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("wrong in  password");


                                }
                                con.Close();

                            }
                            else
                            {
                                MessageBox.Show("wrong in username ");
                            }
                        }
                    }

                }


            }
             
        }
    }
}
