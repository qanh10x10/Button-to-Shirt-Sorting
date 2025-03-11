using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWin : MonoBehaviour
{
    [SerializeField] UIButton btnHome;
    [SerializeField] UIButton btnNextLevel;
    public void CallStart()
    {
        SoundManager.Instance.PlayFx(1);
        btnHome.SetUpEvent(Action_btnHome);
        btnNextLevel.SetUpEvent(Action_btnNextLevel);
    }

    private void Action_btnNextLevel()
    {
        OnClose();
        GameController.Instance.LoadLevel();
        UIManager.Instance.ShowUIGamePlay();
    }

    private void Action_btnHome()
    {
        OnClose();
        GameController.Instance.ResetLevel();
        UIManager.Instance.ShowUIHome();
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
}
