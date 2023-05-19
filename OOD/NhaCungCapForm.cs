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
    public partial class NhaCungCapForm : Form
    {
        SqlConnection connection;
        int idNCC;
        public NhaCungCapForm(SqlConnection connection, int idNCC)
        {
            InitializeComponent();
            this.connection = connection;
            dataGridView1.ReadOnly = true;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            this.idNCC = idNCC;
            loadData(idNCC);
            SqlDataReader reader = new SqlCommand("Select * from PhieuNhap", connection).ExecuteReader();
            List<int> list = new List<int>();

            while (reader.Read())
            {
                if (reader[2].ToString()=="0")
                list.Add(reader.GetInt32(0));
            }
            reader.Close();
            foreach (int n in list)
            {
                PhieuCungCap phieu = new PhieuCungCap(connection, n);
                phieu.onLoad(this);
                flowLayoutPanel1.Controls.Add(phieu);
            }
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
        }
        int currentID;
        public void loadData(int id)
        {
            currentID = id;
            SqlDataReader reader = new SqlCommand("Select * from CTPN where maPN= " + id, connection).ExecuteReader();
            DataColumn dataColumn = new DataColumn("Mã cung cấp");
            DataColumn dataColumn1 = new DataColumn("Mã sản phẩm");
            DataColumn dataColumn2 = new DataColumn("Tên sản phẩm");
            DataColumn dataColumn3 = new DataColumn("Số lượng");
            DataColumn dataColumn5 = new DataColumn("Hãng");
            DataColumn dataColumn4 = new DataColumn("Giá sản phẩm");
            DataTable table = new DataTable();
            DataTable table3 = new DataTable();
            DataTable table2 = new DataTable();
            //
            table.Columns.Add(dataColumn);
            table.Columns.Add(dataColumn1);
            table.Columns.Add(dataColumn2);
            table.Columns.Add(dataColumn3);
            table.Columns.Add(dataColumn4);
            table.Columns.Add(dataColumn5);
            ///
            table2.Columns.Add(new DataColumn("Mã cung cấp"));
            table2.Columns.Add(new DataColumn("Mã sản phẩm"));
            table2.Columns.Add(new DataColumn("Tên sản phẩm"));
            table2.Columns.Add(new DataColumn("Số lượng"));
            table2.Columns.Add(new DataColumn("Giá sản phẩm"));
            table2.Columns.Add(new DataColumn("Hãng"));
            //
            table3.Columns.Add(new DataColumn("Mã sản phẩm"));
            table3.Columns.Add(new DataColumn("Tên sản phẩm"));

            List<int> ids = new List<int>();
            while (reader.Read())
            {
                int idd = reader.GetInt32(2);
                object[] objects = new object[] { reader[1], ((idd == -1) ? "" : idd.ToString()), reader[3], reader[5], reader[6], reader[4] };

                table.Rows.Add(objects);
                if (objects[0].ToString() == idNCC.ToString())
                {
                    table2.Rows.Add(objects);
                    ids.Add(idd);
                }
            }

            reader.Close();

            reader = new SqlCommand("Select * from SanPham where maNCC= " + idNCC, connection).ExecuteReader();
            while (reader.Read())
            {

                bool flag = false;
                foreach (int n in ids)
                {
                    if (n == reader.GetInt32(0))
                    {
                        flag = true;
                        break;
                    }

                }
                if (!flag)
                {
                    table3.Rows.Add(new object[] { reader[0], reader[2] });
                }
            }
            reader.Close();
            dataGridView3.DataSource = table3;
            dataGridView1.DataSource = table;
            dataGridView2.DataSource = table2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Lưu thay đổi?", "Thông báo", MessageBoxButtons.YesNo))
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Themsanphamform themsanphamform = new Themsanphamform();
            themsanphamform.ShowDialog();
            if (themsanphamform.isCreated)
            {
                new SqlCommand("Insert into CTPN values(" + (currentID).ToString() + "," + idNCC + "," + -1 + ",'"
                    + themsanphamform.name + "','" + themsanphamform.hang + "'," + 1 + ",'" + themsanphamform.gia + "')", connection).ExecuteNonQuery();
                loadData(currentID);
                MessageBox.Show("Thêm thành công sản phẩm");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.SelectedCells.Count; i++)
            {
                SqlDataReader reader = new SqlCommand("Select * from SanPham where maSP= " + dataGridView3[0, dataGridView3.SelectedCells[i].RowIndex].Value, connection).ExecuteReader();
                reader.Read();
                string name = reader[2].ToString();
                int idSP = reader.GetInt32(0);
                int idCC = reader.GetInt32(1);
                string hang = reader[4].ToString();
                string gia = reader[6].ToString();
                reader.Close();
                new SqlCommand("Insert into CTPN values(" + (currentID).ToString() + "," + idNCC + "," + idSP + ",'"
                    + name + "','" + hang + "'," + 1 + ",'" + gia + "')", connection).ExecuteNonQuery();

            }
            loadData(currentID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.SelectedCells.Count; i++)
            {
                if (dataGridView2[1, dataGridView2.SelectedCells[i].RowIndex].Value.ToString() == "")
                {
                    string item = dataGridView2[2, dataGridView2.SelectedCells[i].RowIndex].Value.ToString();
                    new SqlCommand("DELETE CTPN where tenSP= '" + item + "' and maPN= " + currentID, connection).ExecuteNonQuery();
                }
                else
                {
                    new SqlCommand("DELETE CTPN where maSP= " + dataGridView2[1, dataGridView2.SelectedCells[i].RowIndex].Value + " and maPN= " + currentID, connection).ExecuteNonQuery();
                }
            }
            

            loadData(currentID);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dataGridView2.SelectedCells.Count;i++)
            {
                if (dataGridView2[1, dataGridView2.SelectedCells[i].RowIndex].Value.ToString() == "")
                {
                    string item = dataGridView2[2, dataGridView2.SelectedCells[i].RowIndex].Value.ToString();
                    new SqlCommand("Update CTPN set soLuong= " +(int.Parse(dataGridView2[3, dataGridView2.SelectedCells[i].RowIndex].Value.ToString()) + 1) + " where tenSP= '" + item + "' and maPN= " + currentID, connection).ExecuteNonQuery();
                }
                else
                {
                    new SqlCommand("Update CTPN set soLuong= " + (int.Parse(dataGridView2[3, dataGridView2.SelectedCells[i].RowIndex].Value.ToString()) + 1 )
                        + " Where maPN= "+currentID+" and maSP= " + dataGridView2[1, dataGridView2.SelectedCells[i].RowIndex].Value, connection).ExecuteNonQuery();
                }
                
            }
            loadData(currentID);
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.SelectedCells.Count; i++)
            {
                if (dataGridView2[1, dataGridView2.SelectedCells[i].RowIndex].Value.ToString() == "")
                {
                    string item = dataGridView2[2, dataGridView2.SelectedCells[i].RowIndex].Value.ToString();
                    new SqlCommand("Update CTPN set soLuong= " + (int.Parse(dataGridView2[3, dataGridView2.SelectedCells[i].RowIndex].Value.ToString()) - 1) + " where tenSP= '" + item + "' and maPN= " + currentID, connection).ExecuteNonQuery();
                }
                else
                {
                    new SqlCommand("Update CTPN set soLuong= " + (int.Parse(dataGridView2[3, dataGridView2.SelectedCells[i].RowIndex].Value.ToString()) - 1)
                        + " Where maPN= " + currentID + " and maSP= " + dataGridView2[1, dataGridView2.SelectedCells[i].RowIndex].Value, connection).ExecuteNonQuery();
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes==MessageBox.Show("Bạn có chắc muốn đăng xuất!"))
            {
                this.Close();
            }
        }
    }
}
