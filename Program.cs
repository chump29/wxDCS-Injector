using System;
using System.Windows.Forms;
using wxDCS_Injector.Presentation;
using static wxDCS_Injector.DI;

namespace wxDCS_Injector
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Wire(new Bindings());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(Resolve<frmMain>());
        }
    }
}
