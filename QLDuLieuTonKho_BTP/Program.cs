using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDuLieuTonKho_BTP
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
            //Application.Run(new Boc("D:\\Database\\QLSX_DG_New.db"));
            //Application.Run(new Ben("D:\\Database\\QLSX_DG_New.db"));
            //Application.Run(new CapNhatDatabase("D:\\Database\\QLSX_DG_New.db"));
            Application.Run(new Main());
        }
    }
}
