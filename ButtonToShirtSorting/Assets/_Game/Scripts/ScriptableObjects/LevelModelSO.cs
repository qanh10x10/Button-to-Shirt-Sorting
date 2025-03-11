using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "ButtonToShirt/LevelModelSO", fileName = "LevelModelSO")]
public class LevelModelSO : ScriptableObject
{
    public int level;
    public int timeCountdown = 99;
    public int buttonCount = 5;

    public bool isRandom = false;

    [Header("Fixed")]
    public List<Vector2> buttonsPos;
    public List<Vector2> slotsPos;

    public List<ButtonInfo> buttonInfos;
    public ButtonInfo GetRandomColor()
    {
        return buttonInfos[Random.Range(0, buttonInfos.Count)];
    }
    public LevelModelSO CreateNewLevel(int _level)
    {
        LevelModelSO newLevel = ScriptableObject.CreateInstance<LevelModelSO>();
        newLevel.level = _level;
        newLevel.timeCountdown = 99;
        newLevel.buttonCount = Random.Range(4, 6) + _level / 6;
        newLevel.isRandom = true;

        string path = "Assets/_Game/Resources/Levels/Lv" + _level + ".asset";

#if UNITY_EDITOR
        if (!Directory.Exists("Assets/_Game/Resources/Levels"))
        {
            Directory.CreateDirectory("Assets/_Game/Resources/Levels");
        }

        UnityEditor.AssetDatabase.CreateAsset(newLevel, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif

        return newLevel;
    }
}