using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;



public class Calculator : MonoBehaviour
{

    Graph graph = new Graph();

    void Start()
    {
        Debug.Log(graph.Calculate("x+2 + x + 3", 1));
    }

    void Update()
    {
        
    }


    class Graph
    {
        public void DrawGraph()
        {

        }

        public string Calculate(string expression, double x)
        {
            var result = RemoveSpace(expression);
            result = ReplacedString(result, x.ToString());
            return EvaluateExpression();
        }

        public string RemoveSpace(string str) => str.Replace(" ", "");

        public string ReplacedString(string str, string x) => str.Replace("x", x);

 /*
  * public static string EvaluateExpression(string expression)
  *      {
  *         //괄호 안에 값 계산해 반환함 (3 + 1) + 2 => 4 + 2
  *      }
  */
        public static string EvaluateSubExpression(string subExpression)
        {
            return new DataTable().Compute(subExpression, null).ToString();
        }



    }
}
