using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] List<GameObject> listUI;
    [SerializeField] UIHome m_UIHome;
    [SerializeField] UIGamePlay m_UIGamePlay;
    [SerializeField] List<GameObject> listPopUp;
    [SerializeField] PopUpWin popup_Win;
    [SerializeField] PopUpLose popup_Lose;
    [SerializeField] PopUpSetting popup_Setting;
    void Start()
    {
        CloseAllUI();
        ShowUIHome();
    }
    #region Show UI
    public void CloseAllUI()
    {
        foreach (GameObject go in listUI)
        {
            go.SetActive(false);
        }
    }
    public void ShowUIHome()
    {
        CloseAllUI();
        m_UIHome.gameObject.SetActive(true);
        m_UIHome.CallStart();
    }
    public void ShowUISetting()
    {
        popup_Setting.gameObject.SetActive(true);
        popup_Setting.CallStart();
    }
    public void ShowUIGamePlay()
    {
        CloseAllUI();
        m_UIGamePlay.gameObject.SetActive(true);
        m_UIGamePlay.CallStart();
    }
    #endregion
}
