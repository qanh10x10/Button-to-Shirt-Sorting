using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameMode gameMode;
    [SerializeField] ButtonCtrl buttonPrefab;
    [SerializeField] List<ButtonCtrl> buttonList;
    private void Start()
    {
        SimplePool.Preload(buttonPrefab.gameObject, 10);
    }
    public void Init(GameMode mode)
    {
        ClearLevel();
    }
    public void ClearLevel()
    {
        foreach (ButtonCtrl button in buttonList)
        {
            if (button != null)
            {
                SimplePool.Despawn(button.gameObject);
            }
        }
        buttonList.Clear();
        StopAllCoroutines();
    }
}
public enum GameMode
{
    None = 0,
    Level = 1,
    Endless = 2,
}