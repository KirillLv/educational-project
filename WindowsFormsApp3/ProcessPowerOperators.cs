using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WindowsFormsApp3
{
    public static class PowerProcessor
    {
        public static string ProcessPowerOperators(string expr)
        {
            var regex = new Regex(@"([\d\.]+)\^([\d\.]+)");

            while (regex.IsMatch(expr))
            {
                expr = regex.Replace(expr, m =>
                {
                    double baseVal = double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
                    double exponent = double.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture);
                    return Math.Pow(baseVal, exponent).ToString(CultureInfo.InvariantCulture);
                });
            }

            return expr;
        }
    }
}