using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OOD
{
    public partial class FormSanPham : Form
    {
        SqlConnection connection;
        int maNV;
        public FormSanPham(SqlConnection connect,int manv)
        {
            CenterToParent();
            InitializeComponent();
            this.maNV = manv;
            connection = connect;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            if(connection.State==ConnectionState.Closed)
            {
                connection.Open();
            }
            dataGridView1.ReadOnly = true;
            load_Data();
            if(manv==-1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                label7.Visible = true;
                button6.Visible = true;
                dataGridView1.ReadOnly = false;
                    dataGridView1.Columns[1].ReadOnly = true;

            }
            dataGridView1.AllowUserToAddRows = false;
        }
        private void load_Data()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Tên Sản Phẩm"));
                table.Columns.Add(new DataColumn("Số lượng"));
                table.Columns.Add(new DataColumn("Giá"));
                table.Columns.Add(new DataColumn("Hãng"));
                table.Columns.Add(new DataColumn("Hạn bảo hành"));
            SqlDataReader reader = new SqlCommand("Select * from SanPham", connection).ExecuteReader();
            if (maNV == -1)
            {
                
                
                
                while (reader.Read())
                {
                    if (reader[2].ToString().Contains(textBox1.Text))
                        table.Rows.Add(new object[] { reader[2].ToString(), reader[3].ToString(),reader[6].ToString()
               , reader[4].ToString(),reader[5].ToString()});
                }
            }

            else
            {
                table.Columns.Add(new DataColumn("Lưu danh sách", typeof(bool)));
                while (reader.Read())
                {
                    if (reader[2].ToString().Contains(textBox1.Text) && reader.GetInt32(3)>0)
                    {
                        idsp.Add(int.Parse(reader[0].ToString()));
                        table.Rows.Add(new object[] { reader[2].ToString(), reader[3].ToString(),reader[6].ToString()
               , reader[4].ToString(),reader[5].ToString(),false });
                    }
                        
                }
            }
            
            reader.Close();
            dataGridView1.DataSource = table;
           
        }

        List<int> idsp = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            List<int >ids= new List<int>();
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (bool.Parse(dataGridView1[5,i].Value.ToString()))
                {
                    ids.Add(i+1);
                }
            }
            new FormGiaoDich(connection,maNV,ids).ShowDialog();
            this.load_Data();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormSanPham_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            load_Data();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy", "Lưu ý", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            for(int i=0;i<dataGridView1.SelectedCells.Count;i++)
            {
               
                dataGridView1[5, dataGridView1.SelectedCells[i].RowIndex].Value = true;
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            new FormLienHe(connection,maNV).ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                new SqlCommand("Update SanPham set tenSP= N'" + dataGridView1[0, i].Value + "', " +
                    "giaSP= N'" + dataGridView1[2, i].Value + "', hangSP=N'" + dataGridView1[3, i].Value
                    + "', hanBaoHanh= " + dataGridView1[4, i].Value + " where maSP= "+i, connection).ExecuteNonQuery();
            }
            MessageBox.Show("Lưu thành công");
        }

    }
}
