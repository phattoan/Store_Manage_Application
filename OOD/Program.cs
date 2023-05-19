using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login(new System.Data.SqlClient.SqlConnection("Data Source=PHATTOAN;Initial Catalog=CuaHangNoiBo;Integrated Security=True;Connect Timeout=30;Encrypt=False;")
                ));
        }
    }
}
