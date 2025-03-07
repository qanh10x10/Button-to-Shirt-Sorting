using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] UIHome m_UIHome;
    [SerializeField] UISetting m_UISetting;
    [SerializeField] UIGamePlay m_UIGamePlay;
    void Start()
    {

    }
    #region Show UI
    public void ShowUIHome()
    {
        m_UIHome.gameObject.SetActive(true);
        m_UIHome.CallStart();
    }
    public void ShowUISetting()
    {
        m_UISetting.gameObject.SetActive(true);
        m_UISetting.CallStart();
    }
    public void ShowUIGamePlay()
    {
        m_UIGamePlay.gameObject.SetActive(true);
        m_UIGamePlay.CallStart();
    }
    #endregion
}
