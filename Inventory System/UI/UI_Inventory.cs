using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private List <UI_InventorySlot> _slots;
    [SerializeField] private int _inventorySize;

    public Inventory Inventory => _inventory;

    [ContextMenu("Initialize Inventory")]
    public void InitializeInventoryUI()
    {
        if (_inventory == null || _inventorySlotPrefab == null) return;
        if(_inventorySize==0)
        {
            createUI(_inventory.Size);
        }
        if(_inventorySize>0)
        {
            createUI(_inventorySize);
        }
    }

    private void createUI(int inventory_size)
    {
        _slots = new List<UI_InventorySlot>(inventory_size);
        for (var i = 0; i < inventory_size; i++)
        {
            var uiSlot = Instantiate(_inventorySlotPrefab) as GameObject;
            uiSlot.transform.SetParent(transform, false);
            var uiSlotScript = uiSlot.GetComponent<UI_InventorySlot>();
            uiSlotScript.AssignSlot(i);
            _slots.Add(uiSlotScript);
        }
    }

    [ContextMenu("Delete All Inventory Slots")]
    public void DeleteAllChildren()
    {
        DeleteChildrenRecursive(transform);
    }

    private void DeleteChildrenRecursive(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            DeleteChildrenRecursive(child);
            DestroyImmediate(child.gameObject);
        }
    }
}
