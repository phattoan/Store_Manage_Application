using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                // Đăng nhập thành công form Nhân Viên
                MessageBox.Show("Đăng Nhập thành công");
                Form form = new OOD.FormNV();
                form.Show();
            }
            else if (radioButton1.Checked == true)
            {
                // Đăng nhập thành công form Admin
                MessageBox.Show("Đăng Nhập thành công");
                Form form = new OOD.Admin();
                form.Show();
            }
            else {
                // Thông báo chưa chọn chức vụ
                MessageBox.Show("Chưa chọn chức vụ để đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
