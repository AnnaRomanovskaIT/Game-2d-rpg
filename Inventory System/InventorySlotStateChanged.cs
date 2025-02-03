using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotStateChanged
{
    public ItemStack NewState { get; }
    public bool Active { get; }
    public InventorySlotStateChanged(ItemStack newState, bool active) 
    { 
        NewState = newState;
        Active = active;
    }
}
