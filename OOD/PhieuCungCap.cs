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
    public partial class PhieuCungCap : UserControl
    {
        int maPN;
        SqlConnection Sql;
        List<string> nhacungcap=new List<string>();
        public PhieuCungCap (SqlConnection sql, int maPN)
        {
            InitializeComponent();
            this.maPN = maPN;
            this.Sql = sql;
            SqlDataReader reader = new SqlCommand("Select * from PhieuNhap Where maPN= " + maPN, sql).ExecuteReader();
            reader.Read();
            label1.Text = "Phiếu số: " + reader[0].ToString();
            label2.Text = "Ngày Tạo: " + reader.GetDateTime(1).ToString();
            checkBox1.Checked = reader[2].ToString() == "1";
            reader.Close();
            List<int> ints = new List<int>();
            reader=new SqlCommand("Select * from CTPN Where maPN= "+maPN,sql).ExecuteReader();
            while(reader.Read())
            {
                ints.Add(reader.GetInt32(1)); 
            }
            reader.Close();
            for (int i = 0; i < ints.Count; i++)
            {
                string temp = new SqlCommand("Select tenNCC from NhaCungCap where maNCC=" + ints[i], Sql).ExecuteScalar().ToString() ;
                bool flag = false;
                
                foreach (var str in nhacungcap)
                {
                    if (str == temp)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag) 
                {
                    nhacungcap.Add(temp);
                    label4.Text += temp + "|";
                }
            }
            
        }
        bool isfromNCC = false;
        NhaCungCapForm nccform;
        QuanLiPhieuAdmin adminForm;
        Button but;
        public void onLoad(NhaCungCapForm form)
        {
            isfromNCC = true;
            this.nccform=form;
        }
        public void onLoad(QuanLiPhieuAdmin form, Button but)
        {
            isfromNCC = false;
            this.adminForm = form;
            this.but = but;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void PhieuCungCap_Enter(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            if(!isfromNCC) this.but.Enabled = !checkBox1.Checked;
            if(isfromNCC)
            {
                nccform.loadData(maPN);
            }
            else
            {
                adminForm.loadData(maPN);
            }
        }

        private void PhieuCungCap_Leave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }

    }
}
