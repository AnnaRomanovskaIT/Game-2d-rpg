using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{
    public List<ItemStack> itemsToShip = new List<ItemStack>();

    public void Add(ItemDefinition item, int number)
    {
        ItemStack itemStack = new ItemStack(item, number);
        itemsToShip.Add(itemStack);
    }

    public void DeleteAll()
    {
        itemsToShip.Clear();
    }
}
