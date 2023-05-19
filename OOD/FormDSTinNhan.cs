using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD
{
    public partial class FormDSTinNhan : Form
    {
        SqlConnection connection;
        Admin admin;
        public FormDSTinNhan(Admin ad, SqlConnection connection)
        {
            CenterToParent();
            InitializeComponent();
            this.connection = connection;
            this.admin = ad;
            loadTinNhan();
        }
        private void loadTinNhan()
        {
            SqlDataReader reader = new SqlCommand("Select * from TinNhan where daDuyet=0", connection).ExecuteReader();
            ;List<int> list = new List<int>();
            while(reader.Read())
            {
                list.Add(   reader.GetInt32(1));
            }
            reader.Close();
            foreach(int i in list)
            {
                flowLayoutPanel1.Controls.Add(new TinNhan(connection, i));
            }
        }
        private void FormDSTinNhan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void FormDSTinNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            admin.load();
        }
    }
}
