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

        //if (str.IndexOf('^') != -1) throw new Exception("���������ʴ� ������");
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
        /// <param name="expression">x�� ���� ���׽�</param>
        /// <param name="x">������ ��</param>
        /// <returns>���� ���� ��ȯ��</returns>
        public static string Calculater(string expression, double x)
        {
            string replacedExpression = ReplaceVariable(expression, x.ToString());
            string evaluatedExpression = EvaluateSubExpressions(replacedExpression);
            return evaluatedExpression;
        }

        /// <summary>
        /// x�� ������ 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="x"></param>
        /// <returns>���Ե� ����</returns>
        private static string ReplaceVariable(string expression, string x) => expression.Replace("x", x);


        /// <summary>
        /// ���������� ��ȣ�� ������ ��Ģ������ ������ ��ȯ��.
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
        /// ���Ŀ� �ִ� ��ȣ�� ���� ���� ��ȯ�մϴ�.
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
        /// ��ȣ �ȿ��ִ� ���� �и��մϴ�
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>()�ȿ��ִ� ��</returns>
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
        /// DataTable�� ����Ͽ� ������ ��Ģ������ string �ȿ��� �����մϴ�.
        /// </summary>
        /// <param name="subExpression"></param>
        /// <returns>���� ��</returns>
        private static string EvaluateSubExpression(string subExpression)
        {
            DataTable dataTable = new DataTable();
            return dataTable.Compute(subExpression, null).ToString();
        }

        public static string RepeatString(string input)
        {
            // '^'�� �������� �տ� �ִ� ���ڿ��� �ڿ� �ִ� ���ڿ��� �����մϴ�.
            string[] parts = input.Split('^');
            string first = parts[0];
            string second = parts[1];
            Debug.Log(first);
            Debug.Log(second);
            // �� ���ڿ����� /*-+�� ���� �������� ���ڿ��� �����ɴϴ�.
            first = GetSubstring(Reverse(first));
            first = Reverse(first);
            second = GetSubstring(second);
            Debug.Log(first);
            Debug.Log(second);
            // ù ��° ���ڿ��� �� ��° ���ڿ��� ���̸�ŭ �ݺ��մϴ�.
            string result = "";
            for (int i = 0; i < double.Parse(second.Replace(" ", "")); i++)
            {
                result += first + "*";
            }
            // ���������� ���ڿ��� ���� �� ���ʿ��� '*'�� �����մϴ�.
            result = result.TrimEnd('*');

            return new string(input.Replace(first + "^" + second, result));
        }

        // �־��� ���ڿ����� /*-+�� ���� �������� �κ� ���ڿ��� ��ȯ�ϴ� �Լ��Դϴ�.

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