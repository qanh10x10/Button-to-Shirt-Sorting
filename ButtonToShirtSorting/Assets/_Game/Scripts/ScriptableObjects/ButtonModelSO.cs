using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ButtonToShirt/ButtonModelSO", fileName = "ButtonModelSO")]
public class ButtonModelSO : ScriptableObject
{
    public static ButtonModelSO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ButtonModelSO>("Models/ButtonModelSO");
            }
            return _instance;
        }
    }
    public static ButtonModelSO _instance;
    public List<ButtonInfo> buttons = new List<ButtonInfo>();
}
[Serializable]
public class ButtonInfo
{
    public string name;
    public Color color;
    public Sprite sprite;
}
