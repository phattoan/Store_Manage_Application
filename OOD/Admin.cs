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
    public partial class Admin : Form
    {
        SqlConnection connection;
        Timer timer = new Timer() { Interval=1000};
        public Admin(SqlConnection sql)
        {
            CenterToParent();
            InitializeComponent();this.connection = sql;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
            MaximizeBox = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new OOD.QuanLiPhieuAdmin(connection);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new OOD.FormHoaDon(connection,true);
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new OOD.AD_QLNV(connection);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormSanPham formSanPham=new FormSanPham(connection,-1);
            formSanPham.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new FormDSTinNhan(this,connection).ShowDialog();
            this.load();
        }
        public void load()
        {
            if ((int)new SqlCommand("Select COUNT(*) from TinNhan where daDuyet= 0", connection).ExecuteScalar() > 0)
            {
                pictureBox1.Visible = true;
            }
            else 
                pictureBox1.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đăng xuất!") == DialogResult.Yes)
                this.Close();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
    }
}
