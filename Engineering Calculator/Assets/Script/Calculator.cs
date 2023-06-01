using NCalc;
using System;
using UnityEngine;

namespace GraphMake
{
    public static class Calculator
    {

        public static double Calculate(string str, double Xvalue)
        {
            object Eval = NCalcu(str, Xvalue);
            string result = Eval.ToString();
            return ToDouble(result);
        }

        public static object NCalcu(string str, double Xvalue)
        {
            Expression expression = new Expression(str);
            expression.Parameters["x"] = Xvalue;
            return expression.Evaluate();
        }

        public static bool HasValue(double value)
        {
            return !(double.IsNaN(value) || double.IsInfinity(value));
        }

        public static double ToDouble(string str)
        {
            return Convert.ToDouble(str);
        }



    }

    public static class InputFieldType
    {
        public static string inputFieldFormula = "inputFieldFormula";
        public static string inputFieldXValue = "inputFieldXValue";
    }
}