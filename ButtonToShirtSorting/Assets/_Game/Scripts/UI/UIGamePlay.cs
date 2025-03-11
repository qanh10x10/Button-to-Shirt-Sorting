using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] TextMeshProUGUI txtSlot;
    [SerializeField] TextMeshProUGUI txtTime;
    [SerializeField] UIButton btnMenu;
    [SerializeField] UIButton btnHint;
    [SerializeField] UIButton btnReset;

    public void CallStart()
    {
        switch (GameController.Instance.gameMode)
        {
            case GameMode.None:
                break;
            case GameMode.Level:
                txtLevel.text = "Level " + Module.cr_Level;
                break;
            case GameMode.Endless:
                txtLevel.text = "Endless";
                break;
            default:
                break;
        }
        btnMenu.SetUpEvent(OnMenuClicked);
        btnHint.SetUpEvent(OnHintClicked);
        btnReset.SetUpEvent(OnResetClicked);
    }
    public void UpdateSlotLeft(int remainingButtons)
    {
        if (txtSlot != null)
            txtSlot.text = $"Slot Left: {remainingButtons}";
    }
    public void UpdateTime(float remainingTime)
    {
        if (txtTime != null)
            txtTime.text = $"Time: {Mathf.CeilToInt(remainingTime)}";
    }

    private void OnMenuClicked()
    {
        UIManager.Instance.Show_PopUpSetting();
    }

    private void OnHintClicked()
    {
        AdsManager.Instance.ShowAd_Reward(() =>
        {
            GameController.Instance.OnClickButtonHint();
        });
    }

    private void OnResetClicked()
    {
        GameController.Instance.ResetLevel();
    }
}