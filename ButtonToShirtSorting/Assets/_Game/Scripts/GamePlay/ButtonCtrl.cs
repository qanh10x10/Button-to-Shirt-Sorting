using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour
{
    [SerializeField] ButtonInfo buttonInfo;
    public GameObject objPlaced;
    public SpriteRenderer imgBtn;
    private Vector3 originalPos;
    private bool isPlaced = false;
    public bool IsPlaced => isPlaced;
    public void Start()
    {
        originalPos = transform.position;
    }
    public void SetButtonInfo(ButtonInfo _info)
    {
        buttonInfo = _info;
        imgBtn.sprite = buttonInfo.sprite;
        //objPlaced.SetActive(false);
    }

    public void OnMouseDrag()
    {
        if (!isPlaced)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePosition + Vector3.up, 0.2f);
        }
    }

    public void OnMouseUp()
    {
        if (isPlaced) return;

    }
}
