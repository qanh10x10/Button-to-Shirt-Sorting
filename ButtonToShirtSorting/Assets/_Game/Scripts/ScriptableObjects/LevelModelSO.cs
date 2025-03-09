using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ButtonToShirt/LevelModelSO", fileName = "LevelModelSO")]
public class LevelModelSO : ScriptableObject
{
    public static LevelModelSO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<LevelModelSO>("Models/LevelModelSO");
            }
            return _instance;
        }
    }
    public static LevelModelSO _instance;
    public List<LevelInfo> levelInfos = new List<LevelInfo>();
}
[Serializable]
public class LevelInfo
{
    public int level;
}
