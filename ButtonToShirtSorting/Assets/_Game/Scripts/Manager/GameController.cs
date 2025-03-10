using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public LevelController levelController;

    public override void InitAwake()
    {
        base.InitAwake();
    }

    public void PlayLevel()
    {
        Module.isLose = false;
        Module.isWin = false;
        if (levelController != null)
        {
            levelController.gameObject.SetActive(true);
            levelController.Init(GameMode.Level);
        }
    }

    public void PlayEndless()
    {
        Module.isLose = false;
        Module.isWin = false;
        if (levelController != null)
        {
            levelController.gameObject.SetActive(true);
            levelController.Init(GameMode.Endless);
        }
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
        levelController.ClearLevel();
        Module.isLose = false;
        Module.isWin = false;
        levelController.Init(levelController.gameMode);
    }
}