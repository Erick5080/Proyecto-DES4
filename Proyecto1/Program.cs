using System;
using System.Windows.Forms;

namespace Proyecto1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Lanza tu formulario principal
            Application.Run(new Form1());
        }
    }
}