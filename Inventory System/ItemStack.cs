using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack
{
    [SerializeField] private ItemDefinition _item;
    [SerializeField] private int _numberOfItems;

    public bool isStackable => _item != null && _item.IsStackable;
    public ItemDefinition Item => _item;

    public int NumberOfItems
    {
        get => _numberOfItems;
        set
        {
            value = value < 0 ? 0 : value;
            _numberOfItems = isStackable ? value : 1;
        }
    }

    public ItemStack(ItemDefinition item, int numberOfItems)
    {
        _item = item;
        NumberOfItems = numberOfItems;
    }

    public ItemStack(HarvestDefinition item, int numberOfItems)
    {
        _item = item;
        System.Random random = new System.Random();
        int randomIndex = random.Next(3);
        Rarity randomRarity = (Rarity)randomIndex;
        item.rarity = randomRarity;
        NumberOfItems = numberOfItems;
    }

    public ItemStack()
    {

    }
}
