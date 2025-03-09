using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] LevelController level;
    public void PlayLevel()
    {
        level.gameObject.SetActive(true);
        level.Init(GameMode.Level);
    }
    public void PlayEndless()
    {
        level.gameObject.SetActive(true);
        level.Init(GameMode.Endless);
    }
    public void DoWin()
    {
        level.gameObject.SetActive(false);
    }
    public void DoLose()
    {
        level.gameObject.SetActive(false);
    }
    public void BackToHome()
    {
        level.gameObject.SetActive(false);
    }
}
