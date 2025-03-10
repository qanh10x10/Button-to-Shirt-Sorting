using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtSlot;
    [SerializeField] TextMeshProUGUI txtTime;
    [SerializeField] UIButton btnMenu;
    [SerializeField] UIButton btnHint;
    [SerializeField] UIButton btnReset;

    public void CallStart()
    {
        btnMenu.SetUpEvent(OnMenuClicked);
        btnHint.SetUpEvent(OnHintClicked);
        btnReset.SetUpEvent(OnResetClicked);
    }

    public void UpdateUI(int remainingButtons, float remainingTime)
    {
        if (txtSlot != null)
            txtSlot.text = $"Remaining: {remainingButtons}";
        if (txtTime != null)
            txtTime.text = $"Time: {Mathf.CeilToInt(remainingTime)}";
    }

    private void OnMenuClicked()
    {
        UIManager.Instance.Show_PopUpSetting();
    }

    private void OnHintClicked()
    {

    }

    private void OnResetClicked()
    {
        if (GameController.Instance.levelController != null)
        {
            GameController.Instance.ResetLevel();
        }
    }
}