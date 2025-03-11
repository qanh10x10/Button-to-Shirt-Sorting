using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLose : MonoBehaviour
{
    [SerializeField] UIButton btnHome;
    public void CallStart()
    {
        SoundManager.Instance.PlayFx(2);
        btnHome.SetUpEvent(Action_btnHome);
    }

    private void Action_btnHome()
    {
        CallEnd();
        GameController.Instance.ResetLevel();
        UIManager.Instance.ShowUIHome();
    }

    public void CallEnd()
    {
        this.gameObject.SetActive(false);
    }
}
