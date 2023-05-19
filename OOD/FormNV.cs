using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD
{
    public partial class FormNV : Form
    {
        SqlConnection conn;
        
        int maNV;
        Timer timer;
        int hour;
        int minute;
        int seccond;
        int day = 0;
        DateTime timeLogin=DateTime.Now;
        int hourworked;
        public FormNV(SqlConnection connection,int maNV)
        {
            CenterToParent();
            InitializeComponent();
            conn = connection;
            this.maNV = maNV;
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
            timer.Start();
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            seccond = DateTime.Now.Second;
            if(new SqlCommand("SELECT COUNT(*) FROM NhanVien where maNV= "+maNV,connection).ExecuteScalar().ToString()=="0")
            {
                new SqlCommand("Insert into NhanVien values(" + maNV + ",'','','',NULL,NULL,0,NULL)", connection).ExecuteNonQuery();
            }
        }
        private void updateHourWork()
        {
            int s = seccond - timeLogin.Second;
            int m = minute - timeLogin.Minute;
            int h = hour - timeLogin.Hour;
            int d = day - timeLogin.Day;
            if (s < 0) m--;
            if (m < 0) h--;
            if (h < 0) d--;
            while(d>0)
            {
                h += 24;
                d--;
            }
            sogio.Text = "Số giờ làm việc: " + (h+hourworked);
           
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            seccond++;
            if(seccond>60)
            {
                seccond -= 60;
                minute++;
            }
            if(minute>60)
            {
                minute -= 60;
                hour++;
            }
            if(hour>24)
            {
                hour -= 24;
            }
            timelabel.Text = "Đồng hồ: "+hour+":"+minute+":"+seccond;
            updateHourWork();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất", "Lưu ý", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new OOD.update_if_NV(new SqlCommand("",conn),maNV);
            form.ShowDialog();
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new OOD.FormSanPham(conn,maNV);
            form.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new OOD.FormHoaDon(conn,false);
            form.Show();
        }
        public  Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File ảnh|*.jpg;*.png";
            openFileDialog.ShowDialog();
            if(openFileDialog.CheckPathExists)
            {
                try
                {
                    Image im = Image.FromFile(openFileDialog.FileName);
                    Byte[] imagedata = ImageToByteArray(im);
                    SqlCommand command = new SqlCommand("Update NhanVien set ImageByte= @image where maNV= "+maNV, conn);
                    command.Parameters.AddWithValue("@image", imagedata);
                    command.ExecuteNonQuery();
                    pictureBox1.Image = resizeImage(Image.FromFile(openFileDialog.FileName), pictureBox1.Size);
                    MessageBox.Show("Thay đổi thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }catch(Exception)
                {
                    
                }




            }
        }
        private void loadData()
        {
            id.Text =maNV.ToString();
            SqlDataReader sqlreader = new SqlCommand("Select * from NhanVien Where maNV=" + maNV, conn).ExecuteReader();
            if (sqlreader.Read())
            {
                    name.Text = sqlreader[1].ToString();
                    identity.Text = sqlreader[2].ToString();
                    phone.Text = sqlreader[3].ToString();
                    address.Text = sqlreader[4].ToString();
                    birth.Text = sqlreader[5].ToString();
                    hourworked = sqlreader.GetInt32(6);
                    try
                {
                    byte[] byteArrayIn = (byte[])sqlreader.GetValue(7);
                    MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                    ms.Position = 0;
                    pictureBox1.Image = Image.FromStream(ms, true);
                }
                    catch(Exception)
                {

                }
                    
                }
            
            else
            {
                
            }
            sqlreader.Close();
        }
        private void FormNV_Load(object sender, EventArgs e)
        {

            loadData();
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
