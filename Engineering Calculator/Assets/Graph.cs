using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;
using TMPro;
using System.Linq;

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
    /// <param name="s"></param>
    public void PushFunc(string _str)
    {
        inputFields[InputFieldType.inputFieldFormula].text += _str;
    }

    /// <summary>
    /// ���Ŀ� ���� ��ȯ��
    /// </summary>
    public void Result()
    {
        try
        {
            var _InputField= inputFields[InputFieldType.inputFieldFormula];
            Expression exp = new Expression(_InputField.text);
            var xValue = inputFields[InputFieldType.inputFieldXValue].text;
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
            var inputField = inputFields[InputFieldType.inputFieldFormula];
            DrawGraph(inputField.text, Dot);
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
    public void DrawGraph(string str, GameObject Dot)
    {

        if (str.IndexOf('^') != -1) throw new Exception("���������ʴ� ������");
        for (double i = -10; i < 10; i += 0.001f)
        {
            Expression exp = new(str);
            exp.Parameters["x"] = i;
            var resultStr = exp.Evaluate().ToString();
            var result = Convert.ToDouble(resultStr);
            var ObjectPosition = new Vector3((float)i, (float)result, 0);
            var DotObject = CreateGameObect(Dot);
            DotObject.transform.position = ObjectPosition; 
 
            
        }
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

    static class InputFieldType
    {
        public static readonly string inputFieldFormula = "inputFieldRormula";
        public static readonly string inputFieldXValue = "inputFieldXValue";
    }

}