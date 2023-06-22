using NCalc;
using System;
using UnityEngine;

namespace GraphMake
{
    public static class Calculator
    {

        /// <summary>
        /// Nclcu 를 사용해 str 값을 계산해 double 값으로 반환함
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
        /// Nclac 라이브러리를 사용해 str 수식을 오브젝트로 반환함
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Xvalue"></param>
        /// <returns>계산돤 오브젝트</returns>
        public static object NCalcu(string str, double Xvalue)
        {
            Expression expression = new Expression(str);
            expression.Parameters["x"] = Xvalue;
            return expression.Evaluate();
        }

        /// <summary>
        /// 값이 무한이거나 숫자가 아니면 false 를 반환함
        /// </summary>
        /// <param name="value"></param>
        /// <returns>value 가 숫자인가?</returns>
        public static bool HasValue(double value)
        {
            return !(double.IsNaN(value) || double.IsInfinity(value));
        }

        /// <summary>
        /// string 을 double 형으로 반환함
        /// </summary>
        /// <param name="str"></param>
        /// <returns>double</returns>
        public static double ToDouble(string str)
        {
            return Convert.ToDouble(str);
        }



    }

    /// <summary>
    /// 입력창 타입임
    /// formula 는 수식창 
    /// value는 x 값 창
    /// </summary>
    public static class InputFieldType
    {
        public static string inputFieldFormula = "inputFieldFormula";
        public static string inputFieldXValue = "inputFieldXValue";
    }
}