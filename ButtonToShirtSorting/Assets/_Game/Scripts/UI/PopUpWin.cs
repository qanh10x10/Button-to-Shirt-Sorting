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
        btnHome.SetUpEvent(Action_btnHome);
        btnNextLevel.SetUpEvent(Action_btnNextLevel);
    }

    private void Action_btnNextLevel()
    {
        OnClose();
        UIManager.Instance.ShowUIGamePlay();
    }

    private void Action_btnHome()
    {
        OnClose();
        GameController.Instance.BackToHome();
        UIManager.Instance.ShowUIHome();
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
}
