using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtSlot : MonoBehaviour
{
    [SerializeField] ButtonInfo slotInfo;
    public void SetSlotInfo(ButtonInfo _info)
    {
        slotInfo = _info;
        GetComponent<SpriteRenderer>().color = slotInfo.color;
    }
}
