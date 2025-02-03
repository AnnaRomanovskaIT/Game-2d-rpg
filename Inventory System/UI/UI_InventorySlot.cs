using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InventorySlot : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private int _inventorySlotIndex;
    [SerializeField] private Image _slotIcon;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Sprite _activeSlotIcon;
    [SerializeField] private Sprite _idleSlotIcon;
    [SerializeField] private TMP_Text _numberOfItems;
    private UI_Inventory ui_inventory;
    private InventorySlot _slot;
    private bool _isActiveSlot;

    public UI_Inventory ui_Inventory => ui_inventory;
    public Image ItemIcon => _itemIcon;
    public bool HasItem() => _slot.HasItem();

    private void Start()
    {
        AssignSlot(_inventorySlotIndex);
        ui_inventory = GetComponentInParent<UI_Inventory>();
        _isActiveSlot = _slot.Active;
        UpdateViewState(_slot.State);
    }

    public void AssignSlot(int slotIndex)
    {
        if (_slot != null) _slot.StateChanged -= OnStateChanged;
        _inventorySlotIndex = slotIndex;
        if (_inventory == null) _inventory = GetComponentInParent<UI_Inventory>().Inventory;
        _slot = _inventory.Slots[_inventorySlotIndex];
        _slot.StateChanged += OnStateChanged;
    }

    private void UpdateViewState(ItemStack state)
    {
        var item = state?.Item;
        var hasItem = item != null;
        var isStackable = hasItem && item.IsStackable;

        if (_itemIcon != null)
        {
            _itemIcon.enabled = hasItem;

            if (hasItem)
            {
                _itemIcon.sprite = item.iconSprite;
            }
        }

        if (_numberOfItems != null)
        {
            _numberOfItems.enabled = isStackable;

            if (isStackable)
            {
                _numberOfItems.SetText(state.NumberOfItems.ToString());
            }
        }

        if (_slotIcon != null)
        {
            _slotIcon.sprite = _slot.Active ? _activeSlotIcon : _idleSlotIcon;
        }
    }

    public void SwapItems(UI_InventorySlot slot, UI_InventorySlot targetSlot)
    {
        _inventory.SwapItems(slot._slot, targetSlot._slot);
    }

    private void OnStateChanged(object sender, InventorySlotStateChanged args)
    {
        _isActiveSlot = args.Active;
        UpdateViewState(args.NewState);
    }
}