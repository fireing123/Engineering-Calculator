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
    /// 필드에 문자열을 추가하는 함수, 버튼에 사용됨
    /// </summary>
    /// <param name="s"></param>
    public void PushFunc(string _str)
    {
        inputFields[InputFieldType.inputFieldFormula].text += _str;
    }

    /// <summary>
    /// 수식에 값을 반환함
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
    /// 그래프를 수식으로 그려줌
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
    /// 입력된 식을 그래프로 표현함
    /// </summary>
    /// <param name="str"></param>
    /// <exception cref="Exception"></exception>
    public void DrawGraph(string str, GameObject Dot)
    {

        if (str.IndexOf('^') != -1) throw new Exception("지원되지않는 연산자");
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
    /// @object를 x, y 좌표에 생성합니다.
    /// </summary>
    /// <param name="object"></param>
    /// <returns>원한 좌표에 생성된 오브젝트를 반환</returns>
    public GameObject CreateGameObect(GameObject @object) => Instantiate(@object);

    /// <summary>
    /// 트렌스폼에서 위치값을 추출합니다
    /// </summary>
    /// <param name="father"></param>
    /// <returns>프지션</returns>
    public Vector3 GetPosition(Transform father) => father.transform.position;


    /// <summary>
    /// 값 수식 대입값 을 지웁니다
    /// </summary>
    public void DeleteInputText()
    {
        foreach (var inputField in inputFields.Values) inputField.text = string.Empty;
    }

    /// <summary>
    /// 아버지 오브젝트 아래 자식들을 삭제합니다.
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