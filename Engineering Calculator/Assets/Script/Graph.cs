using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;
using TMPro;
using static GraphMake.Calculator;
using static GraphMake.InputFieldType;

public class Graph : MonoBehaviour
{
    public GameObject father;
    public GameObject Dot;
    public SerializableDict<TMP_InputField> serializableDict;
    Dictionary<string, TMP_InputField> inputFields;
    void Awake()
    {
        inputFields = serializableDict.getDict();
    }

    void Update()
    {
        
    }


    /// <summary>
    /// �ʵ忡 ���ڿ��� �߰��ϴ� �Լ�, ��ư�� ����
    /// </summary>
    /// <param name="_str"></param>
    public void PushFunc(string _str)
    {
        inputFields[inputFieldFormula].text += _str;
    }

    /// <summary>
    /// ���Ŀ� ���� ��ȯ��
    /// </summary>
    public void Result()
    {
        try
        {
            var _InputField= inputFields[inputFieldFormula];
            Expression exp = new Expression(_InputField.text);
            var xValue = inputFields[inputFieldXValue].text;
            exp.Parameters["x"] = xValue;
            _InputField.text = exp.Evaluate().ToString();
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    /// <summary>
    /// �׷����� �������� �׷���
    /// </summary>
    public void PushButton()
    {
        DeleteChild(father.transform);
        try
        {
            var inputField = inputFields[inputFieldFormula];
            var condition = inputFields[inputFieldCondition];
            Debug.Log(condition.text);
            DrawGraph(inputField.text, Dot, condition.text);
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    /// <summary>
    /// �Էµ� ���� �׷����� ǥ����
    /// </summary>
    /// <param name="str"></param>
    /// <exception cref="Exception"></exception>
    public void DrawGraph(string str, GameObject Dot, string cond)
    {

        if (str.IndexOf('^') != -1) throw new Exception("���������ʴ� ������");
        for (double i = -10; CheckCondition(cond ,i); i += 0.001f)
        {
            double result = Calculate(str, i);
            if (HasValue(result))
            {
                GameObject DotObject = CreateGameObect(Dot);
                Vector3 DotPosition = setPositon(i, result);
                DotObject.transform.position = DotPosition;
            }
        }
    }
    
    /// <summary>
    /// x , y �� ���Ͱ�����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 setPositon(double x, double y)
    {
        return  new Vector3((float)x, (float)y, 0);
    }


    /// <summary>
    /// @object�� x, y ��ǥ�� �����մϴ�.
    /// </summary>
    /// <param name="object"></param>
    /// <returns>���� ��ǥ�� ������ ������Ʈ�� ��ȯ</returns>
    public GameObject CreateGameObect(GameObject @object) => Instantiate(@object);

    /// <summary>
    /// Ʈ���������� ��ġ���� �����մϴ�
    /// </summary>
    /// <param name="father"></param>
    /// <returns>������</returns>
    public Vector3 GetPosition(Transform father) => father.transform.position;


    /// <summary>
    /// �� ���� ���԰� �� ����ϴ�
    /// </summary>
    public void DeleteInputText()
    {
        foreach (var inputField in inputFields.Values) inputField.text = string.Empty;
    }

    /// <summary>
    /// �ƹ��� ������Ʈ �Ʒ� �ڽĵ��� �����մϴ�.
    /// </summary>
    /// <param name="parent"></param>
    public void DeleteChild(Transform parent)
    {
        Transform[] childList = parent.GetComponentsInChildren<Transform>();

        if (childList == null) return;

        foreach (var child in childList)
        {
            if (child == parent) continue;

            Destroy(child.gameObject);
        }

    }

    [Serializable]
    public class SerializableDict<T>
    {
        public List<SerializeData<T>> data;
        private Dictionary<string, T> dict = new();

        public Dictionary<string, T> getDict()
        {
            for (int i = 0; i < data.Count; i++)
            {
                dict.Add(data[i].key, data[i].value);
            }
            return dict;
        }
    }

    [Serializable]
    public class SerializeData<T>
    {
        public string key;
        public T value;
    }

}