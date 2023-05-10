using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    public GameObject father;
    public GameObject Dot;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
    }

    public void PushButton()
    {
        DeleteChild();
        DrawGraph(inputField.text);
    }

    public void DrawGraph(string str)
    {

        //if (str.IndexOf('^') != -1) throw new Exception("지원되지않는 연산자");
        for (double i = -10.00f; i < 10.00f; i += 0.01f)
        {
            Dotted(i, double.Parse(Calculate.Calculater(str, i)));
        }
    }

    public void Dotted(double x, double y) => Instantiate(Dot, new Vector3((float)x, (float)y, 0), Quaternion.identity).transform.parent = father.transform;

    public void DeleteChild()
    {
        Transform[] childList = father.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }

    public static class Calculate
    {
        /// <summary>
        /// Function for Calculate to string
        /// </summary>
        /// <param name="expression">x에 대한 다항식</param>
        /// <param name="x">대입할 값</param>
        /// <returns>계산된 값을 반환함</returns>
        public static string Calculater(string expression, double x)
        {
            string replacedExpression = ReplaceVariable(expression, x.ToString());
            string evaluatedExpression = EvaluateSubExpressions(replacedExpression);
            return evaluatedExpression;
        }

        /// <summary>
        /// x를 대입함 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="x"></param>
        /// <returns>대입된 수식</returns>
        private static string ReplaceVariable(string expression, string x) => expression.Replace("x", x);


        /// <summary>
        /// 마지막으로 괄호를 제외한 사칙연산을 연산해 반환함.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static string EvaluateSubExpressions(string expression)
        {
            string result = expression;

            for (int i=0; i < HasSubExpressions(result); i++)
            { 
                string subExpression = GetSubExpression(result);;
                result = result.Replace($"({subExpression})", EvaluateSubExpression(subExpression));
            }
            Debug.Log(result);
            if (result.IndexOf('^') != -1) result = RepeatString(result);
            Debug.Log(result);
            return EvaluateSubExpression(result);
        }
        /// <summary>
        /// 수식에 있는 괄호의 수를 세어 반환합니다.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>()acount</returns>
        /// <exception cref="ArgumentException"></exception>
        private static int HasSubExpressions(string expression)
        {
            int openParenthesesCount = 0;
            int open = 0;
            foreach (char c in expression)
            {
                if (c == '(')
                {
                    openParenthesesCount++;
                    open++;
                }
                else if (c == ')') openParenthesesCount--;

                if (openParenthesesCount < 0) throw new ArgumentException("The expression has mismatched parentheses.");
            }
            Debug.Log(open);
            return open;
        }

        /// <summary>
        /// 괄호 안에있는 식을 분리합니다
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>()안에있는 식</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string GetSubExpression(string expression)
        {
            int openParenthesisIndex = expression.LastIndexOf('(');

            if (openParenthesisIndex == -1) throw new ArgumentException("The expression has no sub-expression.");

            int closeParenthesisIndex = expression.IndexOf(')', openParenthesisIndex);

            if (closeParenthesisIndex == -1) throw new ArgumentException("The expression has no closing parenthesis.");

            return expression.Substring(openParenthesisIndex + 1, closeParenthesisIndex - openParenthesisIndex - 1);
        }
        /// <summary>
        /// DataTable을 사용하여 간단한 사칙연산을 string 안에서 진행합니다.
        /// </summary>
        /// <param name="subExpression"></param>
        /// <returns>계산된 값</returns>
        private static string EvaluateSubExpression(string subExpression)
        {
            DataTable dataTable = new DataTable();
            return dataTable.Compute(subExpression, null).ToString();
        }

        public static string RepeatString(string input)
        {
            // '^'를 기준으로 앞에 있는 문자열과 뒤에 있는 문자열을 구분합니다.
            string[] parts = input.Split('^');
            string first = parts[0];
            string second = parts[1];
            Debug.Log(first);
            Debug.Log(second);
            // 각 문자열에서 /*-+를 만날 때까지의 문자열을 가져옵니다.
            first = GetSubstring(Reverse(first));
            first = Reverse(first);
            second = GetSubstring(second);
            Debug.Log(first);
            Debug.Log(second);
            // 첫 번째 문자열을 두 번째 문자열의 길이만큼 반복합니다.
            string result = "";
            for (int i = 0; i < double.Parse(second.Replace(" ", "")); i++)
            {
                result += first + "*";
            }
            // 마지막에는 문자열을 붙일 때 불필요한 '*'를 제거합니다.
            result = result.TrimEnd('*');

            return new string(input.Replace(first + "^" + second, result));
        }

        // 주어진 문자열에서 /*-+를 만날 때까지의 부분 문자열을 반환하는 함수입니다.

        private static string GetSubstring(string input)
        {
            string result = "";
            foreach (char c in input)
            {
                if (c == '*' || c == '/' || c == '+' || c == '-')
                {
                    break;
                }
                result += c;
            }
            return result;
        }

        private static string Reverse(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
    }
}