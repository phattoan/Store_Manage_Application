using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new OOD.FormSanPham();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new OOD.FormHoaDon();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new OOD.AD_QLNV();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new OOD.update_if_AD();
            form.Show();
        }
    }
}
