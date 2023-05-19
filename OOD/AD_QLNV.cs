using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OOD
{
    public partial class AD_QLNV : Form
    {
        SqlConnection connection;
        public AD_QLNV(SqlConnection sqlConnection)
        {
            InitializeComponent();
            this.connection = sqlConnection;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.MultiSelect = false;
            if(sqlConnection.State==ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            dataLoad();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = !dataGridView1.ReadOnly; dataGridView2.ReadOnly = !dataGridView2.ReadOnly;
        }


        private void dataLoad()
        {
            SqlDataReader reader = new SqlCommand("SELECT * FROM DangNhap inner join NhanVien on id=maNV", connection).ExecuteReader();
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("ID User");
            DataColumn column1 = new DataColumn("Mật khẩu");
            DataColumn column2 = new DataColumn("Ngày tạo");
            DataColumn column4 = new DataColumn("Mã NV");
            DataColumn column5 = new DataColumn("Họ tên");
            DataColumn column6 = new DataColumn("CMND");
            DataColumn column7 = new DataColumn("Số DT");
            DataColumn column8 = new DataColumn("Địa Chỉ");
            DataColumn column9 = new DataColumn("Ngày Sinh");
            DataColumn column10 = new DataColumn("Số giờ làm");
            table.Columns.Add(column);
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            table.Columns.Add(column7);
            table.Columns.Add(column8);
            table.Columns.Add(column9);
            table.Columns.Add(column10);
            column10.ReadOnly = true;
            column.ReadOnly = true;
            column2.ReadOnly = true;
            column4.ReadOnly = true;
            column9.ReadOnly = true;

            while (reader.Read())
            {
                datetime = reader.GetDateTime(2);
                object[] objs = new object[] { reader[0], reader[1], reader.GetDateTime(2), reader[4], reader[5], reader[6], reader[7],
                reader[8],reader[9],reader[10]};
                table.Rows.Add(objs);
            }
            dataGridView1.DataSource = table;
            reader.Close();

            ///Nha cung cấp load
            ///

            reader=new SqlCommand("SELECT * FROM DangNhap  inner join NhaCungCap on id=maNCC", connection).ExecuteReader();
            DataTable table1 = new DataTable();
            DataColumn ccolumn = new DataColumn("ID NCC");
            DataColumn ccolumn1 = new DataColumn("Mật khẩu");
            DataColumn ccolumn2 = new DataColumn("Ngày tạo");
            DataColumn ccolumn3 = new DataColumn("Tên nhà cung cấp");
            DataColumn ccolumn4 = new DataColumn("Địa chỉ");
            DataColumn ccolumn5 = new DataColumn("Số điện thoại");
            table1.Columns.Add(ccolumn);
            table1.Columns.Add(ccolumn1);
            table1.Columns.Add(ccolumn2);
            table1.Columns.Add(ccolumn3);
            table1.Columns.Add(ccolumn4);
            table1.Columns.Add(ccolumn5);
            ccolumn.ReadOnly = true;
            ccolumn2.ReadOnly = true;
           

            while (reader.Read())
            {
                datetime = reader.GetDateTime(2);
                object[] objs = new object[] { reader[0], reader[1], datetime, reader[5], reader[6], reader[8]
                };
                table1.Rows.Add(objs);
            }
            dataGridView2.DataSource = table1;
            reader.Close();

        }
        DateTime datetime;
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tải lại trang sẽ hủy tất cả tác vụ hiện có? Bạn có chắc không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataLoad();
                listBox1.Items.Clear();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (oldValue != dataGridView1[e.ColumnIndex,e.RowIndex].Value.ToString())
            listBox1.Items.Add("Dòng: " + e.RowIndex.ToString() + " Cột: " + e.ColumnIndex.ToString()
                + "| Tại table 1 Đã thay đổi giá trị");

        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (oldValue != dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString())
                listBox1.Items.Add("Dòng: " + e.RowIndex.ToString() + " Cột: " + e.ColumnIndex.ToString()
                    + "| tại table 2 Đã thay đổi giá trị");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                if (MessageBox.Show("Dữ liệu trên hệ thống sẽ loại bỏ và được thay thế bằng dữ liệu hiện tại?\nBạn có chắc là muốn tiếp tục?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Xác nhận muốn thay đổi?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for(int i=0;i<dataGridView1.Rows.Count;i++)
                        {
                            new SqlCommand("Update DangNhap set "+
                                " pass= '" + dataGridView1[1, i].Value +"' where id= " + dataGridView1[0,i].Value,connection).ExecuteNonQuery();
                            new SqlCommand("Update NhanVien set hoTen= N'" + dataGridView1[4, i].Value + "', cmnd='" +
                                dataGridView1[5, i].Value + "', sdt= '" + dataGridView1[6, i].Value + "', diaChi=N'" + dataGridView1[7, i].Value + "' where " +
                                "maNV= " + dataGridView1[0,i].Value, connection).ExecuteNonQuery();
                            
                        }
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            new SqlCommand("Update DangNhap set " +
                                " pass= '" + dataGridView2[1, i].Value + "' where id= " + dataGridView2[0, i].Value, connection).ExecuteNonQuery();
                            new SqlCommand("Update NhaCungCap set tenNCC= N'" + dataGridView2[3, i].Value + "', diaChi=N'" +
                                dataGridView2[4, i].Value + "', SDT= '" + dataGridView2[5, i].Value + "'  where " +
                                "maNCC= " + dataGridView2[0, i].Value, connection).ExecuteNonQuery();

                        }



                        MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK);
                        dataLoad();
                        listBox1.Items.Clear();
                    }
                }
                else MessageBox.Show("Không có tác vụ nào thay đổi", "Thông báo", MessageBoxButtons.OK);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Thoát sẽ hủy hết thay đổi hiện tại?","Thông báo",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        string oldValue;
        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Xác định tạo tài khoản nhân viên?","Thông báo",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                
                listBox1.Items.Add("Đã Lưu dòng mới tại dòng: " + (dataGridView1.RowCount - 1).ToString());
                int count = int.Parse(new SqlCommand("Select COUNT(*) from DangNhap", connection).ExecuteScalar().ToString()) + 1;
                new SqlCommand("Insert into DangNhap values(" + count + ",'"+textBox2.Text+"',GETDATE(),0)",connection).ExecuteNonQuery();
                new SqlCommand("Insert into NhanVien values(" + count + ",'','','',NULL,NULL,0,NULL)", connection).ExecuteNonQuery();
                MessageBox.Show("Thêm thành công");
                dataLoad();
            }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void AD_QLNV_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
        }
         
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác định tạo tài khoản nhà cung cấp?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                listBox1.Items.Add("Đã Lưu dòng mới tại dòng: " + (dataGridView1.RowCount - 1).ToString()+" table nhà cung cấp");
                //Find id missing between
                int count = -1;
                int i = 1;
                while(count==-1)
                {
                    Object ob = new SqlCommand("SelecT COUNT(*) from DangNhap where id= " +i.ToString(), connection).ExecuteScalar();
                    if(int.Parse(ob.ToString())==0)
                    {
                        count = i;
                        continue;
                    }
                    i++;
                }
               
                
                new SqlCommand("Insert into DangNhap values(" + count + ",'',GETDATE(),2)", connection).ExecuteNonQuery();
                new SqlCommand("Insert into NhaCungCap values(" + count + ",'','',GETDATE(),'')", connection).ExecuteNonQuery();
                MessageBox.Show("Thêm thành công");
                dataLoad();
            }
        }
    }
}
