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
    public partial class FormHoaDon : Form
    {
        SqlConnection connection;
        
        public FormHoaDon(SqlConnection sql,bool isAdmin)
        {
            InitializeComponent();
            CenterToParent();
            this.connection = sql;
            dataGridView1.ReadOnly = true;
            this.isAdmin(isAdmin);
            loadData();
            dataGridView1.AllowUserToAddRows = false;
            
        }
        private void isAdmin(bool flag)
        {
            
        }

        private void loadData()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Mã hóa đơn"));
            table.Columns.Add(new DataColumn("Mã nhân viên"));
            table.Columns.Add(new DataColumn("Họ tên khách hàng"));
            table.Columns.Add(new DataColumn("Địa chỉ"));
            table.Columns.Add(new DataColumn("Số điện thoại"));
            table.Columns.Add(new DataColumn("Ngày tạo"));
            table.Columns.Add(new DataColumn("Bảo hành"));
            SqlDataReader reader = new SqlCommand("Select * from HoaDon",connection).ExecuteReader();
            while (reader.Read())
            {
                    table.Rows.Add(new object[] { reader[0].ToString(), reader[1].ToString(),reader[2].ToString(),
                 reader[3].ToString(),reader[4].ToString(),reader[5].ToString(),reader[6].ToString()});
            }
            reader.Close();
            dataGridView1.DataSource = table;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {

        }
    }
}
