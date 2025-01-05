using System;
using System.Windows.Forms;

namespace MapUnpacker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Global.form = new Form1();
            Application.Run(Global.form);
        }
    }
}
