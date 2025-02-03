using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EventAdd : MonoBehaviour
{
    [SerializeField] private GameObject myObject;
    private GameObject mouseObject;
    private Image imgItem;

    void Awake()
    {
        GameObject gameObject = myObject;
        PrepareEvent(gameObject);
    }

    public void PrepareEvent(GameObject obj)
    {
        AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnBeginDrag(obj); });
        AddEvent(obj, EventTriggerType.EndDrag, OnEndDrag);
        AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    private void OnEndDrag(BaseEventData baseEventData)
    {
        PointerEventData eventData = baseEventData as PointerEventData;
        if (eventData != null)
        {
            OnEndDrag(eventData.pointerDrag, eventData);
        }
    }

    public void OnBeginDrag(GameObject obj)
    {
        mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        mouseObject.transform.SetParent(transform.parent);
        mouseObject.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
        UI_InventorySlot uiSlot = obj.GetComponent<UI_InventorySlot>();
        if (uiSlot.HasItem())
        {
            imgItem = mouseObject.AddComponent<Image>();
            imgItem.sprite = uiSlot.ItemIcon.sprite;
            imgItem.raycastTarget = false;
        }
    }

    public void OnDrag(GameObject obj)
    {
        if (mouseObject != null)
        {
            mouseObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnEndDrag(GameObject obj, PointerEventData eventData)
    {
        GameObject targetObject = eventData.pointerEnter;
        UI_InventorySlot targetSlot = targetObject.GetComponent<UI_InventorySlot>();
        UI_InventorySlot uiSlot = obj.GetComponent<UI_InventorySlot>();

        if (targetSlot != null && targetSlot != uiSlot)
        {
            uiSlot.SwapItems(uiSlot, targetSlot);
        }

        Destroy(mouseObject);
        UI_Inventory ui_inventory = uiSlot.ui_Inventory;
        ui_inventory.DeleteAllChildren();
        ui_inventory.InitializeInventoryUI();
        uiSlot = null;
    }
}
