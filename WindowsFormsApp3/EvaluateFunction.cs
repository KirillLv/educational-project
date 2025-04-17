using System;
using System.Data;
using System.Globalization;

namespace WindowsFormsApp3
{
    public static class FunctionEvaluator
    {
        public static double EvaluateFunction(string functionStr, double x)
        {
            try
            {
                string expression = functionStr.Replace("x", x.ToString(CultureInfo.InvariantCulture));
                expression = PowerProcessor.ProcessPowerOperators(expression);

                using (DataTable dt = new DataTable())
                {
                    return Convert.ToDouble(dt.Compute(expression, null));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Ошибка в функции '{functionStr}' при x={x}: {ex.Message}");
            }
        }
    }
}