using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public event EventHandler<InventorySlotStateChanged> StateChanged;
    [SerializeField] private ItemStack _state;
    private bool _active;

    public ItemDefinition Item => _state?.Item;

    public ItemStack State
    {
        get => _state;
        set
        {
            _state = value;
            NotifyAboutStateChanged();
        }
    }

    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            NotifyAboutStateChanged();
        }
    }

    public int NumberOfItems
    {
        get => _state.NumberOfItems;
        set
        {
            _state.NumberOfItems = value;
            NotifyAboutStateChanged();
        }
    }

    public void Clear()
    {
        State = null;
    }

    public bool HasItem() => _state?.Item != null;

    private void NotifyAboutStateChanged()
    {
        StateChanged?.Invoke(this, new InventorySlotStateChanged(_state, _active));
    }
}
