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
    public partial class update_if_NV : Form
    {
        SqlCommand comm;
        int manhanvien;
        public update_if_NV(SqlCommand c,int manhanvien)
        {
            CenterToParent();
            InitializeComponent();
            this.comm = c;
            this.manhanvien = manhanvien;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            comm.CommandText = "Update NhanVien Set " +
                 "hoTen= N'" + hoten.Text + "'" +
                 ",SDT = '" + sdt.Text.Replace("-",String.Empty).Replace(" ",String.Empty) + "'" +
                 ",diaChi= N'" + address.Text + "'" +
                 ",ngaySinh=Convert(date,'" + dateTimePicker1.Value.Year.ToString() +
                 ((dateTimePicker1.Value.Month < 10) ? "0" + dateTimePicker1.Value.Month.ToString() : dateTimePicker1.Value.Month.ToString()) +
                 ((dateTimePicker1.Value.Day < 10) ? "0" + dateTimePicker1.Value.Day.ToString() : dateTimePicker1.Value.Day.ToString()) + "') Where maNV = " + manhanvien;
            comm.ExecuteNonQuery();
           
            MessageBox.Show("Cập nhật thành công", "", MessageBoxButtons.OK);
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy", "Lưu ý", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
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

    }
}
