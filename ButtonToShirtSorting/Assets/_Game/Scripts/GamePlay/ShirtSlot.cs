using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ShirtSlot : MonoBehaviour
{
    public ButtonInfo slotInfo;
    public SpriteRenderer imgSlot;
    public ParticleSystem hintFX;
    public void SetSlotInfo(ButtonInfo _info)
    {
        slotInfo = _info;
        imgSlot.color = slotInfo.color;
    }
    public bool IsMatchingColor(ButtonInfo _info)
    {
        return slotInfo == _info;
    }
    public void ShowHint()
    {
        hintFX.gameObject.SetActive(true);
        hintFX.Play();
    }
    public void HideHint()
    {
        hintFX.gameObject.SetActive(false);
        hintFX.Stop();
    }
}
