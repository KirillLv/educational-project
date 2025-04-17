using System;
using WindowsFormsApp3;

namespace GoldenSectionMinimization
{
    public static class GoldenSection
    {
        public static double GoldenSectionSearch(Func<double, double> f, double a, double b, double tolerance, IterationsForm iterForm)
        {
            double goldenRatio = (1 + Math.Sqrt(5)) / 2;
            double c = a + (b - a) / goldenRatio;
            double d = b - (b - a) / goldenRatio;

            var dataGrid = iterForm.DataGrid;
            dataGrid.Rows.Clear();

            int iteration = 0;

            while (Math.Abs(b - a) > tolerance)
            {
                iteration++;
                double intervalLength = Math.Abs(b - a);

                dataGrid.Rows.Add(
                    iteration,
                    $"[{a:F4}, {b:F4}]",
                    $"{intervalLength:F6}",
                    $"{c:F6}",
                    $"{f(c):F6}",
                    $"{d:F6}",
                    $"{f(d):F6}"
                );

                if (f(c) < f(d))
                    b = d;
                else
                    a = c;

                c = a + (b - a) / goldenRatio;
                d = b - (b - a) / goldenRatio;
            }

            return (a + b) / 2;
        }
    }
}