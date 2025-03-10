using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Level Info")]
    public GameMode gameMode;
    public LevelModelSO level;
    public GameObject trGamelevel;
    public int timeRemaining = 100;
    public int buttonRemain = 10;

    [Header("Prefabs Ref")]
    [SerializeField] ButtonCtrl buttonPrefab;
    [SerializeField] List<ButtonCtrl> buttonList;
    [SerializeField] ShirtSlot shirtSlotPrefab;
    [SerializeField] List<ShirtSlot> shirtSlotList;

    private void OnEnable()
    {
        switch (gameMode)
        {
            case GameMode.Level:
                LoadLevel();
                break;
            case GameMode.Endless:
                LoadEndless();
                break;
            default:
                break;
        }
        if (ctTimeRemain != null)
            StopCoroutine(ctTimeRemain);
        ctTimeRemain = StartCoroutine(IeTimerCountdown());
    }
    public void LoadLevel()
    {
        Module.isLose = false;
        Module.isWin = false;
        level = Resources.Load<LevelModelSO>(string.Format("Levels/Lv{0}", Module.cr_Level));
        buttonRemain = level.buttonCount;
        UIManager.Instance.m_UIGamePlay.UpdateSlotLeft(buttonRemain);
        List<ButtonInfo> selectColors = ButtonModelSO.Instance.buttons.OrderBy(b => Random.Range(0, ButtonModelSO.Instance.buttons.Count)).Take(buttonRemain).ToList();

    }
    public void LoadEndless()
    {
        Module.isLose = false;
    }
    public void DoWin()
    {
        if (Module.isWin) return;
        Module.isWin = true;
        UIManager.Instance.Show_PopUpWin();
    }

    public void DoLose()
    {
        if (Module.isLose) return;
        Module.isLose = true;
        UIManager.Instance.Show_PopUpLose();
    }

    public void BackToHome()
    {
        UIManager.Instance.ShowUIHome();
    }
    public void ResetLevel()
    {
        Module.isLose = false;
        Module.isWin = false;
    }
    Coroutine ctTimeRemain;
    IEnumerator IeTimerCountdown()
    {
        UIManager.Instance.m_UIGamePlay.UpdateTime(timeRemaining);
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            UIManager.Instance.m_UIGamePlay.UpdateTime(timeRemaining);
        }

        if (timeRemaining <= 0)
        {
            UIManager.Instance.Show_PopUpLose();
        }
    }
}
public enum GameMode
{
    None = 0,
    Level = 1,
    Endless = 2,
}