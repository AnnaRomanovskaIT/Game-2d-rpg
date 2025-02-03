using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _size = 10;
    [SerializeField] private List<InventorySlot> _slots;
    //public event EventHandler<HarvestDefinition> HarvestInInventory;
    private int _activeSlotIndex;

    public int Size => _size;

    public List<InventorySlot> Slots => _slots;

    public int ActiveSlotIndex
    {
        get => _activeSlotIndex;
        private set
        {
            _slots[_activeSlotIndex].Active = false;
            _activeSlotIndex = value < 0 ? _size - 1 : value % Size;
            _slots[_activeSlotIndex].Active = true;
        }
    }
    public bool HasItem()
    {
        return _slots[_activeSlotIndex].HasItem();
    }
    public Sprite GetSpriteOfActiveItem()
    {
        return _slots[_activeSlotIndex].Item.iconSprite;
    }
    private void Awake()
    {
        if (_size > 0)
        {
            _slots[0].Active = true;
        }
    }

    private void OnValidate()
    {
        AdjustSize();
    }

    private void AdjustSize()
    {
        _slots ??= new List<InventorySlot> ();
        if (_slots.Count > _size) _slots.RemoveRange(_size, _slots.Count - _size);
        if (_slots.Count < _size) _slots.AddRange(new InventorySlot[_size - _slots.Count]);
    }

    public ItemStack AddItem(ItemStack itemStack)
    {
        var relevantSlot = FindSlot(itemStack.Item, true);
        if(isFull() && relevantSlot == null)
        {
            throw new InventoryException(InventoryOperation.Add, "Inventory is full");
        }
        if (relevantSlot != null)
        {
            relevantSlot.NumberOfItems += itemStack.NumberOfItems;
        }
        else
        {
            relevantSlot = _slots.First(slot => !slot.HasItem());
            relevantSlot.State = itemStack;
            /*if(itemStack.Item is HarvestDefinition harvest)
            { 
                HarvestInInventory?.Invoke(this, harvest);
            }
            */
        }   
        return relevantSlot.State;
    }

    public bool HasItem(ItemStack itemStack, bool checkNumberOfItems = false)
    {
        var itemSlot = FindSlot(itemStack.Item);
        if (itemSlot == null) return false;
        if (!checkNumberOfItems) return true;
        if (itemStack.Item.IsStackable)
        {
            return itemSlot.NumberOfItems >= itemStack.NumberOfItems;
        }
        return _slots.Count(slot => slot.Item == itemStack.Item) >= itemStack.NumberOfItems;
    }

    public ItemStack RemoveItem(int atIndex, bool spawn = false)
    {
        if (!_slots[atIndex].HasItem())
            throw new InventoryException(InventoryOperation.Remove, "Slot is empty");

        if (spawn && TryGetComponent<GameItemSpawner>(out var itemSpawner))
        {
            itemSpawner.SpawnItem(_slots[atIndex].State);
        }

        ClearSlot(atIndex);
        return new ItemStack();
    }

    public ItemStack PlantCrop(int atIndex, bool spawn = false)
    {
        if (_slots[atIndex]==null)
            throw new InventoryException(InventoryOperation.Remove, "Slot is empty");

        if (!_slots[atIndex].HasItem())
            throw new InventoryException(InventoryOperation.Remove, "Slot is empty");

        if (spawn && TryGetComponent<GameItemSpawner>(out var itemSpawner))
        {
            if (_slots[atIndex].State.Item is PlantDefinition)
            {
                PlantDefinition plant = (PlantDefinition)_slots[atIndex].State.Item;
                itemSpawner.SpawnPlant(plant);
            }
        }
        if (_slots[atIndex].State.NumberOfItems == 1)
        {
            ClearSlot(atIndex);
        }
        // If there are more than one item, decrement the number of items by 1
        else if (_slots[atIndex].State.NumberOfItems > 1)
        {
            _slots[atIndex].State.NumberOfItems--;
        }

        return new ItemStack();
    }

    public ItemStack RemoveItem(ItemStack itemStack)
    {
        var itemSlot = FindSlot(itemStack.Item);
        if (itemSlot == null)
            throw new InventoryException(InventoryOperation.Remove, "No Item is in Inventory");
        if (itemSlot.Item.IsStackable && itemSlot.NumberOfItems<itemStack.NumberOfItems)
            throw new InventoryException(InventoryOperation.Remove, "Not enough Items");

        itemSlot.NumberOfItems -= itemStack.NumberOfItems;
        if (itemSlot.Item.IsStackable && itemSlot.NumberOfItems>0)
        {
            return itemSlot.State;
        }

        itemSlot.Clear();
        return new ItemStack();
    }

    public void ClearSlot(int atIndex)
    {
        _slots[atIndex].Clear();
    }

    public bool CanAcceptItem(ItemStack itemStack)
    {
        var SlotWithStackableItem = FindSlot(itemStack.Item, true);
        return !isFull() || SlotWithStackableItem != null;
    }

    public bool isFull()
    {
        return _slots.Count(slot => slot.HasItem()) >= _size;
    }

    private InventorySlot FindSlot(ItemDefinition item, bool onlyStackable=false)
    {
        return _slots.FirstOrDefault(slot => slot.Item == item &&
                                             (item.IsStackable ||
                                             !onlyStackable));
    }

    public int IndexOf(InventorySlot targetSlot)
    {
        int index = _slots.FindIndex(slot => slot == targetSlot);
        return index;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        int index1 = _slots.IndexOf(item1);
        int index2 = _slots.IndexOf(item2);
        if (index1 < 0 || index2 < 0) return;

        InventorySlot temp = _slots[index1];
        _slots[index1] = _slots[index2];
        _slots[index2] = temp;
    }

    public void ActivateSlot(int atIndex)
    {
        ActiveSlotIndex = atIndex;
    }

    public InventorySlot GetActiveSlot()
    {
        return _slots[ActiveSlotIndex];
    }

    public bool IsTool(InventorySlot slot)
    {
        if (slot == null) return false;
        var itemDefinition = slot.State.Item;
        if (itemDefinition is ToolsDefinition toolDefinition)
        {
            return true;
        }
        return false;
    }

    public bool HasToolEquipped()
    {
        return _slots[ActiveSlotIndex].HasItem() && IsTool(_slots[ActiveSlotIndex]);
    }
    public bool HasToolEquipped(ToolsDefinition tool)
    {
        if (_slots[ActiveSlotIndex].HasItem() && IsTool(_slots[ActiveSlotIndex]))
        {
            return _slots[ActiveSlotIndex].State.Item is ToolsDefinition toolDefenition &&
                    toolDefenition == tool;
        }
        return false;
    }
    public bool HasToolEquipped(string toolType)
    {
        if (_slots[ActiveSlotIndex].HasItem() && IsTool(_slots[ActiveSlotIndex]))
        {
            return _slots[ActiveSlotIndex].State.Item is ToolsDefinition toolDefenition &&
                    toolDefenition.type.ToString() == toolType;
        }
        return false;
    }

    public bool IsPlant(InventorySlot slot)
    {
        if (slot == null) return false;
        var itemDefinition = slot.State.Item;
        if (itemDefinition is PlantDefinition plant)
        {
            return true;
        }
        return false;
    }

    public bool HasPlantSeedEquipped()
    {
        return _slots[ActiveSlotIndex].HasItem() && IsPlant(_slots[ActiveSlotIndex]);
    }

    public bool HasHarvestEquipped(InventorySlot slot)
    {
        if (slot == null) return false;
        var itemDefinition = slot.State.Item;
        if (itemDefinition is HarvestDefinition harvest)
        {
            return true;
        }
        return false;
    }
}
