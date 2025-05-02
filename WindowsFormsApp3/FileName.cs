using System;
using System.Windows.Forms;
using WindowsFormsApp3;

namespace GoldenSectionMinimization
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Method()); // Здесь MainForm - ваша главная форма
        }
    }
}