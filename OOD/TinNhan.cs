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
    public partial class TinNhan : UserControl
    {
        SqlConnection sql;
        string noidung;
        int maNV;
        int idTN;
        public TinNhan( SqlConnection sql,int idtinnhan)
        {
            InitializeComponent();
            this.sql = sql;
            this.idTN = idtinnhan;
            SqlDataReader reader = new SqlCommand("Select * from TinNhan where idTN= "+idtinnhan, sql).ExecuteReader();
            reader.Read();
            this.maNV = reader.GetInt32(0);
            this.noidung = reader[2].ToString(); textBox1.Text ="số: " +maNV.ToString();
            richTextBox1.Text = noidung;
            reader.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            new SqlCommand("Update TinNhan set daDuyet= " + (checkBox1.Checked?1:0)+" Where idTN= "+idTN, sql).ExecuteNonQuery();
        }
    }
}
