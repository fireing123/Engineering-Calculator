using NCalc;
using System;

namespace GraphMake
{
    public static class Calculator
    {

        public static double Calculate(string str, double Xvalue)
        {
            Expression expression = new(str);
            expression.Parameters["x"] = Xvalue;
            string result = expression.Evaluate().ToString();
            return toDouble(result);
        }

        public static bool HasValue(double value)
        {
            return !(double.IsNaN(value) && double.IsInfinity(value));
        }

        public static double toDouble(string str)
        {
            return Convert.ToDouble(str);
        }
    }

    public static class InputFieldType
    {
        public static string inputFieldFormula = "inputFieldRormula";
        public static string inputFieldXValue = "inputFieldXValue";
    }
}