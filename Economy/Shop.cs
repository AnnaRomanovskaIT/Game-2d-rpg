using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Shop : MonoBehaviour
{
    [SerializeField] private List <ShopSlot> listToSell;
    [SerializeField] private List <ShopSlot> listToBuy;
    public List<ShopSlot> Sell => listToSell;
    public List<ShopSlot> Buy => listToBuy;
    public int FindItemPrice(ItemStack item)
    {
        if(Price(item)>0)
        {
            return Price(item);
        }
        return 0;
    }
    public int Price(ItemStack item)
    {
        var itemDefinition = item.Item;
        var priceForItem=0;
        foreach (var sellItem in listToSell)
        {
            if (sellItem.Item.Item == itemDefinition)
            {
                priceForItem = sellItem.Coins;
                Debug.Log("Item found in shop list: " + priceForItem);
                break;
            }
        }
        if (itemDefinition is HarvestDefinition harvest)
        {
            float rarityMultiplier=1f, stateMultiplier=1f;
            rarityMultiplier = GetRarityMultiplier(harvest.rarity);
            stateMultiplier = GetStateMultiplier(harvest.State);
            float totalPrice = item.NumberOfItems * priceForItem * rarityMultiplier * stateMultiplier;
            Debug.Log("Total final price: " + totalPrice);
            return (int)totalPrice;
        }
        return 0;
    }
    private float GetRarityMultiplier(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Ordinary:
                return 1f;
            case Rarity.Wonderful:
                return 1.4f;
            case Rarity.Incredible:
                return 1.8f;
            default:
                return 0f; // Handle unknown rarity
        }
    }
    private float GetStateMultiplier(StateOfHarvest state)
    {
        switch (state)
        {
            case StateOfHarvest.Rotten:
                return 0.4f;
            case StateOfHarvest.Ordinary:
                return 1f;
            case StateOfHarvest.Incredible:
                return 1.3f;
            default:
                return 0f; // Handle unknown state
        }
    }
}
[System.Serializable]
public class ShopSlot
{
    [SerializeField] private int coins;
    [SerializeField] private ItemStack item;
    ShopSlot(){}
    ShopSlot(int Coins, ItemStack Item)
    {
        coins = Coins;
        item = Item;
    }
    public int Coins { get => coins; }
    public ItemStack Item { get => item; }
}
