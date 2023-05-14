using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;
using TMPro;

public class Graph : MonoBehaviour
{
    public GameObject father;
    public GameObject Dot;
    public TMP_InputField inputField;
    public TMP_InputField X;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
    }

    public void PushFunc(string s)
    {
        inputField.text += s;
    }

    public void Result()
    {
        try
        {
            Expression exp = new Expression(inputField.text);
            exp.Parameters["x"] = X.text;
            text.text = exp.Evaluate().ToString();
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void PushButton()
    {
        DeleteChild();
        try
        {
            DrawGraph(inputField.text);
        }   catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void DrawGraph(string str)
    {

        //if (str.IndexOf('^') != -1) throw new Exception("지원되지않는 연산자");
        for (double i = -10; i < 10; i += 0.001f)
        {
            Expression exp = new(str);
            exp.Parameters["x"] = i;
            Dotted(i, Convert.ToDouble(exp.Evaluate().ToString()));
        }
    }

    public void Dotted(double x, double y) => Instantiate(Dot, new Vector3((float)x, (float)y, 0), Quaternion.identity).transform.parent = father.transform;

    public void DeleteText()
    {
        text.text = "";
        inputField.text = "";
        X.text = "";
    }

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
}