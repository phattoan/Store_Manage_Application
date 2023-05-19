using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD
{
    public partial class Login : Form
    {
        SqlConnection connection;
        public Login(SqlConnection sql)
        {
            InitializeComponent();
            this.connection = sql;
            radioButton2.Checked = true;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            comboBox1.Items.Clear();
            SqlDataReader reader = new SqlCommand("Select * from DangNhap", connection).ExecuteReader();
            while(reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();
        }
        public void reInitialize()
        {
            comboBox1.Items.Clear();
            SqlDataReader reader = new SqlCommand("Select * from DangNhap", connection).ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.reInitialize();
            if ((radioButton1.Checked || radioButton2.Checked||radioButton3.Checked)&&id!=-1)
            {
                SqlDataReader reader = new SqlCommand("Select * from DangNhap where id= " + id.ToString(), connection).ExecuteReader();
                if (reader.Read())
                {
                    
                    if (reader[1].ToString()==textBox2.Text)
                    {
                        
                        errorProvider1.Clear();
                       
                        int type = reader.GetInt32(3);
                        int id = int.Parse(reader[0].ToString());
                        reader.Close();
                        if (textBox2.Text == "")
                        {
                            new DatMK(id, connection).ShowDialog();
                            
                            return;
                        }
                        if (radioButton2.Checked == true &&type  == 0)
                        {
                            reader.Close();


                            // Đăng nhập thành công form Nhân Viên
                            MessageBox.Show("Đăng Nhập thành công");

                            Form form = new OOD.FormNV(connection, id); ;
                            this.Hide();
                            form.ShowDialog();
                            this.Show();
                            return;
                        }
                        if (radioButton1.Checked == true && type == 1)
                        {
                            reader.Close();
                            // Đăng nhập thành công form Admin
                            MessageBox.Show("Đăng Nhập thành công");
                            Form form = new OOD.Admin(connection);
                            this.Hide();
                            form.ShowDialog();
                            this.reInitialize();
                            this.Show();
                            
                            return;
                        }
                        if (radioButton3.Checked == true && type == 2)
                        {
                            reader.Close();
                            // Đăng nhập thành công form Admin
                            MessageBox.Show("Đăng Nhập thành công");
                            Form form = new OOD.NhaCungCapForm(connection,id);
                            this.Hide();
                            form.ShowDialog();
                            this.Show();
                            return;
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(button1, "Sai mậu khẩu!");
                    }
                    reader.Close();

                }
                else
                {
                    reader.Close();
                    errorProvider1.SetError(button1, "Sai thông tin đăng nhập!");
                }

            }
            else
            {
                // Thông báo chưa chọn chức vụ
                MessageBox.Show("Vui lòng chọn phương thức đầy đủ để đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

            private Point MouseDownLocation;


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        int id=-1;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = int.Parse( comboBox1.Items[comboBox1.SelectedIndex].ToString());
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
                textBox2.UseSystemPasswordChar = !textBox2.UseSystemPasswordChar;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
