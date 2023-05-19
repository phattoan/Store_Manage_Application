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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOD
{
    public partial class QuanLiPhieuAdmin : Form
    {
        SqlConnection connection;
        public QuanLiPhieuAdmin(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
            loadPhieu();dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
        }
        int currentId;
        public void loadPhieu()
        {
            flowLayoutPanel1.Controls.Clear();
            SqlDataReader reader = new SqlCommand("Select * From PhieuNhap", connection).ExecuteReader();
            List<int> ints = new List<int>();
            while(reader.Read())
            {
                ints.Add(reader.GetInt32(0));
            }
            reader.Close();
            foreach(int n in ints)
            {
                PhieuCungCap phieu = new PhieuCungCap(connection, n);
                phieu.onLoad(this,button1);
                flowLayoutPanel1.Controls.Add(phieu);
            }
            reader.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes==MessageBox.Show("Bạn có muốn tạo phiếu!","Thông baos",MessageBoxButtons.YesNo))
            {
                int count = int.Parse(new SqlCommand("Select COUNT(*) from PhieuNhap", connection).ExecuteScalar().ToString()) + 1;
                new SqlCommand("Insert into PhieuNhap values(" + count + ",GETDATE()," + 0 + ")", connection).ExecuteNonQuery();
                loadPhieu();
            }
            
        }
        public void loadData(int id)
        {
            currentId = id;
            SqlDataReader reader = new SqlCommand("Select * from CTPN where maPN= " + id, connection).ExecuteReader();
            DataColumn dataColumn = new DataColumn("Mã cung cấp");
            DataColumn dataColumn1 = new DataColumn("Mã sản phẩm");
            DataColumn dataColumn2 = new DataColumn("Tên sản phẩm");
            DataColumn dataColumn3 = new DataColumn("Số lượng");
            DataColumn dataColumn4 = new DataColumn("Hãng");
            DataColumn dataColumn5 = new DataColumn("Giá sản phẩm");
            DataTable table = new DataTable();
            table.Columns.Add(dataColumn);
            table.Columns.Add(dataColumn1);
            table.Columns.Add(dataColumn2);
            table.Columns.Add(dataColumn3);
            table.Columns.Add(dataColumn4);
            table.Columns.Add(dataColumn5);
            while (reader.Read())
            {
                int idd = reader.GetInt32(2);
                object[] objects = new object[] { reader[1], ((idd == -1) ? "" : idd.ToString()), reader[3], reader[5], reader[4], reader[6] };
                table.Rows.Add(objects);
            }
            reader.Close();
            dataGridView1.DataSource = table;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            loadData(currentId);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (dataGridView1[1,i].Value.ToString()=="")
                {
                    int count =int.Parse( new SqlCommand("Select COUNT(*) from SanPham", connection).ExecuteScalar().ToString());
                    new SqlCommand("Insert into SanPham values (" + (count+1)+", "+
                        dataGridView1[0,i].Value+", N'" + dataGridView1[2,i].Value+"', " + dataGridView1[3,i].Value+",N'" +
                        dataGridView1[4, i].Value +"',1,N'"+ dataGridView1[5,i].Value+"')", connection).ExecuteNonQuery();
                }
                else
                {
                    int count =int.Parse( new SqlCommand("Select soLuong from SanPham where maSP= " + dataGridView1[1, i].Value, connection).ExecuteScalar().ToString())+1;
                    new SqlCommand("Update SanPham set soLuong=" + count + " where maSP = " + dataGridView1[1, i].Value, connection).ExecuteNonQuery();
                }
            }
            new SqlCommand("Update PhieuNhap set daDuyet=1 where maPN= "+currentId,connection).ExecuteNonQuery();
            MessageBox.Show("Duyệt thành công");
            loadPhieu();
        }
    }
}
