using NCalc;
using System;
using UnityEngine;

namespace GraphMake
{
    public static class Calculator
    {

        /// <summary>
        /// Nclcu �� ����� str ���� ����� double ������ ��ȯ��
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Xvalue"></param>
        /// <returns></returns>
        public static double Calculate(string str, double Xvalue)
        {
            object Eval = NCalcu(str, Xvalue);
            string result = Eval.ToString();
            return ToDouble(result);
        }

        /// <summary>
        /// Nclac ���̺귯���� ����� str ������ ������Ʈ�� ��ȯ��
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Xvalue"></param>
        /// <returns>���� ������Ʈ</returns>
        public static object NCalcu(string str, double Xvalue)
        {
            Expression expression = new Expression(str);
            expression.Parameters["x"] = Xvalue;
            return expression.Evaluate();
        }

        /// <summary>
        /// ���� �����̰ų� ���ڰ� �ƴϸ� false �� ��ȯ��
        /// </summary>
        /// <param name="value"></param>
        /// <returns>value �� �����ΰ�?</returns>
        public static bool HasValue(double value)
        {
            return !(double.IsNaN(value) || double.IsInfinity(value));
        }

        /// <summary>
        /// string �� double ������ ��ȯ��
        /// </summary>
        /// <param name="str"></param>
        /// <returns>double</returns>
        public static double ToDouble(string str)
        {
            return Convert.ToDouble(str);
        }



    }

    /// <summary>
    /// �Է�â Ÿ����
    /// formula �� ����â 
    /// value�� x �� â
    /// </summary>
    public static class InputFieldType
    {
        public static string inputFieldFormula = "inputFieldFormula";
        public static string inputFieldXValue = "inputFieldXValue";
    }
}