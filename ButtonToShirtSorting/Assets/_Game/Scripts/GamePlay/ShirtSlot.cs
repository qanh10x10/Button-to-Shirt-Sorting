using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtSlot : MonoBehaviour
{
    [SerializeField] ButtonInfo slotInfo;
    public SpriteRenderer imgSlot;
    public void SetSlotInfo(ButtonInfo _info)
    {
        slotInfo = _info;
        imgSlot.color = slotInfo.color;
    }
}
