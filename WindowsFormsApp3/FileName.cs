using System;
using System.Windows.Forms;

namespace GoldenSectionMinimization
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); // Здесь MainForm - ваша главная форма
        }
    }
}