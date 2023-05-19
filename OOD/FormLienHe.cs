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
    public partial class FormLienHe : Form
    {
        SqlConnection sql;
        int id;
        public FormLienHe(SqlConnection sql,int id)
        {
            CenterToParent();
            InitializeComponent();this.sql = sql;
            this.id = id;
            this.textBox2.Text = id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length<10)
            {
                errorProvider1.SetError(textBox2, "Chủ đề cần phải trên 10 kí tự"); return;
            }
            errorProvider1.Clear();
            string str = DateTime.Now.Year.ToString() + ((DateTime.Now.Month < 10) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) +
              ((DateTime.Now.Day < 10) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString());
            int count = int.Parse(new SqlCommand("Select Count(*) from TinNhan", sql).ExecuteScalar().ToString())+1;
            new SqlCommand("Insert into TinNhan values("+id+"," + count + ",N'" + textBox1.Text + "',CONVERT(date,'" + str + "'),0)",sql).ExecuteNonQuery();
            MessageBox.Show("Đã gửi tin nhắn!");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy", "Lưu ý", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void FormLienHe_Load(object sender, EventArgs e)
        {

        }
    }
}
