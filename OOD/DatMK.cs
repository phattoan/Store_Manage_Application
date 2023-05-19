using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD
{
    public partial class DatMK : Form
    {
        SqlConnection connection;
        int id;
        public DatMK(int id,SqlConnection sql)
        {
            CenterToParent();
            InitializeComponent();this.id = id;
            this.connection = sql;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length<8)
            {
                errorProvider1.SetError(button1, "Mật khẩu phải hơn 6 kí tự");
                return;
            }
            errorProvider1.Clear();
            if(textBox1.Text!=textBox2.Text)
            {
                errorProvider1.SetError(button1, "Mật khẩu không trùng nhau!");
                return;
            }
            errorProvider1.Clear();
            
            if(MessageBox.Show("Một khi đặt mật khẩu sẽ không thể thay đổi lại,\n để thay đổi cần phải báo admin thay đổi, bạn có muốn tiếp tục?","Thông báo",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                new SqlCommand("Update DangNhap set pass='" + textBox2.Text + "' where id=" + id, connection).ExecuteNonQuery();
                MessageBox.Show("Thay đổi thành công");
                this.Close();
            }
        }
    }
}
