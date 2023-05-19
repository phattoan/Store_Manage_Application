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
    public partial class FormGiaoDich : Form
    {
        int nv;
        SqlConnection SqlConnection;
        List<int> list;
        public FormGiaoDich(SqlConnection sqlConnection,int nv,List<int> ints)
        {
            CenterToParent();
            InitializeComponent();
            this.nv = nv;
            this.SqlConnection = sqlConnection;list = ints;
            dataGridView1.AllowUserToAddRows = false;
            loadData();
            
        }
        double total=0;
        int highest=0;
        int idhd;
        private void loadData()
        {
            textBox3.Text = nv.ToString();
            idhd = (int.Parse(new SqlCommand("Select COUNT(maHD) from HoaDon", SqlConnection).ExecuteScalar().ToString()) + 1);
            label2.Text = "Mã: " + idhd;
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Tên sản phẩm"));
            table.Columns.Add(new DataColumn("Giá"));
            SqlDataReader reader;
            for (int i=0;i<list.Count;i++)
            {
                 reader = new SqlCommand("Select * from SanPham where maSP= "+list[i], SqlConnection).ExecuteReader();reader.Read();
                table.Rows.Add(new object[] { reader[2].ToString(), reader[6].ToString() });
                total += int.Parse(reader[6].ToString().Replace(".", String.Empty).Replace("đ", String.Empty));
                int bh=int.Parse( reader[5].ToString());
                highest = (bh > highest) ? bh : highest;
                reader.Close();
            }
            dataGridView1.DataSource = table;
            textBox4.Text = total.ToString() + "đ";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn hủy","Lưu ý",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length<10)
            {
                errorProvider1.SetError(textBox1, "Vui lòng nhập đầy đủ họ và tên");return;
            }
            errorProvider1.Clear();
            if(textBox2.Text.Length<15)
                {
                errorProvider1.SetError(textBox2, "Địa chỉ phải 15 kí tự trở lên");return;
            }
            errorProvider1.Clear();
            if(dataGridView1.Rows.Count==0)
            {
                errorProvider1.SetError(button1, "Không có sản phẩm nào để mua");
                return;
            }
            errorProvider1.Clear();
            if(maskedTextBox1.Text.Length!=12||!int.TryParse(maskedTextBox1.Text.Replace("-",String.Empty),out _))
            {
                errorProvider1.SetError(maskedTextBox1, "Số điện thoại ko hợp lệ");
                return;
            }
            errorProvider1.Clear();
            string str = DateTime.Now.Year.ToString()+ ((DateTime.Now.Month < 10) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString())+
              ((DateTime.Now.Day < 10) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString());
            new SqlCommand("Insert into HoaDon values (" + idhd + "," + nv + ",'" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox1.Text.Replace("-",String.Empty) + "',Convert(date,'"
            + str + "'),"+highest+")",SqlConnection).ExecuteNonQuery();
            foreach (int id in list)
            {
                new SqlCommand("Insert into CTHD values (" + idhd + "," + id + "," + 1 + ")", SqlConnection).ExecuteNonQuery();
            }
            MessageBox.Show("Giao dịch thành công", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            foreach(int id in list)
            {
                int count =int.Parse (new SqlCommand("Select soLuong from SanPham where maSP= "+id,SqlConnection).ExecuteScalar().ToString());
                new SqlCommand("Update SanPham set soLuong= " + (count - 1) + " where maSP= " + id, SqlConnection).ExecuteNonQuery();
            }
            this.Close();
        }
        string datetime = ((DateTime.Now.Day < 10) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString())
                + "-" + ((DateTime.Now.Month < 10) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString())
                + "-" + DateTime.Now.Year.ToString();
        
        private void FormGiaoDich_Load(object sender, EventArgs e)
        {
            maskedTextBox2.Text = datetime;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
