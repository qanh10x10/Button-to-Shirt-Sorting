using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSetting : MonoBehaviour
{
    [SerializeField] UIButton btnClose;
    public void CallStart()
    {
        btnClose.SetUpEvent(Action_btnClose);
    }

    private void Action_btnClose()
    {
        OnClose();
    }

    private void OnClose()
    {
        this.gameObject.SetActive(false);
    }
}
