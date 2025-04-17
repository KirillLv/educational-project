using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp3
{
    public static class FunctionPlotter
    {
        public static void PlotFunction(Chart chart, Func<double, double> f, double a, double b)
        {
            chart.Series.Clear();
            Series series = new Series
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                Name = "Function"
            };

            int points = 100;
            double step = (b - a) / points;

            for (double x = a; x <= b; x += step)
            {
                series.Points.AddXY(x, f(x));
            }

            chart.Series.Add(series);
            chart.ChartAreas[0].AxisX.Title = "x";
            chart.ChartAreas[0].AxisY.Title = "f(x)";
            chart.ChartAreas[0].RecalculateAxesScale();
        }
    }
}