using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHome : MonoBehaviour
{
    [SerializeField] UIButton btnPlayLevel;
    [SerializeField] UIButton btnPlayEndless;
    [SerializeField] UIButton btnSetting;
    public void CallStart()
    {
        btnPlayEndless.SetUpEvent(Action_btnPlayEndless);
        btnPlayLevel.SetUpEvent(Action_btnPlayLevel);
        btnSetting.SetUpEvent(Action_btnSetting);
    }

    private void Action_btnSetting()
    {

    }

    private void Action_btnPlayLevel()
    {

    }

    private void Action_btnPlayEndless()
    {

    }
}
