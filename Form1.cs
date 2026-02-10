using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace interview_simulator
{
    public partial class Form1 : Form
    {
        string connStr = "server=localhost;port=3306;database=interview_simulator;uid=root;pwd=;";
       public int i = 1;
        bool interviewStarted = false;

        void show_question()
        {
           
            MySqlConnection conn = new MySqlConnection(connStr);
            string query = "SELECT question FROM questions WHERE id=@id ";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@id", i);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                label1.Text = reader["question"].ToString();
                i++;
            }
            else
            {
                if (interviewStarted)
                {
                    button1.Enabled = false;
                    textBox1.Enabled = false;
                    button2.Enabled = false;
                    MessageBox.Show("No more questions");
                    button4.Visible = true;
                }
            }


            reader.Close();
            conn.Close();
        }

        void hide()
        {
            textBox1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button4.Visible = false;
            button5.Visible = false;


        }
        void show()
        {
            textBox1.Visible = true;
            label1.Visible = true;
            
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            hide();
            
            button5.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "");

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "enter your answer first");
            }
            else
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        string query = "UPDATE questions SET answer=@answer WHERE id=@id";
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@answer", textBox1.Text);
                        cmd.Parameters.AddWithValue("@id", i - 1);
                        cmd.ExecuteNonQuery();


                    }



                    textBox1.Text = "";
                    textBox1.Focus();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            } }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            interviewStarted = true; // start
            i = 1;
            show();
            show_question();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            button5.Visible = true;
            button5.Enabled = true;
            show_question();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            if (i - 2 > 0)
            {
                button5.Enabled = true;

                string query = "SELECT  question FROM questions WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", i - 2);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label1.Text = reader["question"].ToString();
                }
                i--;
            }
            else
                
            {
                button5.Enabled = false;
                MessageBox.Show("it is the first question");
            }
            conn.Close();
        }
        int score = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string keyword = "";
            string answer = "";
            string all_correct_words = "";

            int d = 1;
            while (d <= 30) {
                string query = "SELECT keywords ,answer FROM questions WHERE id=@id";
                

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", d);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    keyword = reader["keywords"].ToString();
                answer = reader["answer"].ToString();
               
                    char[] del = { ',' };
                    string[] tokens = keyword.Split(del);
                    int count = 0;
                    while (count < tokens.Length)
                    {
                        if (answer.Contains(tokens[count]))
                        {
                            score += 10;
                            all_correct_words = all_correct_words + tokens[count] + " , ";

                        }
                        count++;
                    }
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label4.Text = score.ToString();
                    label5.Text = all_correct_words;

                }

                d++;
                reader.Close();
            }
            label1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
