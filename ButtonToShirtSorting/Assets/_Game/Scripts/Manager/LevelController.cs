using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameMode gameMode;
    [SerializeField] ShirtButton buttonPrefabs;
    [SerializeField] List<ShirtButton> buttonList;
    [SerializeField] GridLayoutGroup buttonsField;
    public void Init(GameMode mode)
    {

    }
}
public enum GameMode
{
    None = 0,
    Level = 1,
    Endless = 2,
}