using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour
{
    public ButtonInfo buttonInfo;
    public GameObject objPlaced;
    public SpriteRenderer imgBtn;
    public ParticleSystem hintFX;
    private Vector3 originalPos;
    private bool isPlaced = false;
    public bool IsPlaced => isPlaced;
    public void SetButtonInfo(ButtonInfo _info)
    {
        buttonInfo = _info;
        imgBtn.sprite = buttonInfo.sprite;
        objPlaced.SetActive(false);
        originalPos = transform.position;
    }

    public void OnMouseDrag()
    {
        if (!isPlaced)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePosition + Vector3.up, 0.2f);
            imgBtn.sortingOrder = 3;
        }
    }

    public void OnMouseUp()
    {
        if (isPlaced) return;

        LayerMask slotLayer = LayerMask.GetMask("Slot");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.25f, slotLayer);
        List<ShirtSlot> matchingSlots = new List<ShirtSlot>();
        foreach (Collider2D hitCollider in hitColliders)
        {
            ShirtSlot slot = hitCollider.GetComponent<ShirtSlot>();
            if (slot != null && slot.IsMatchingColor(buttonInfo) && !slot.isPlaced)
            {
                matchingSlots.Add(slot);
            }
        }

        if (matchingSlots.Count > 0)
        {
            ShirtSlot bestSlot = matchingSlots.OrderBy(slot => Vector2.Distance(transform.position, slot.transform.position)).First();

            transform.position = bestSlot.transform.position;
            isPlaced = true;
            bestSlot.isPlaced = true;
            objPlaced.SetActive(true);

            GameController.Instance.RemainChecking(bestSlot, this);
            imgBtn.sortingOrder = 0;
            return;
        }
        transform.position = originalPos;
        imgBtn.sortingOrder = 0;
    }
    public void DespawnObj()
    {
        isPlaced = false;
        SimplePool.Despawn(gameObject);
    }
    public void SetAuto(ShirtSlot slot)
    {
        transform.position = slot.transform.position;
        isPlaced = true;
        objPlaced.SetActive(true);

        GameController.Instance.RemainChecking(slot, this);
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
