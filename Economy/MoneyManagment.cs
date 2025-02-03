using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManagment : MonoBehaviour
{
    [SerializeField] private Seller seller;
    [SerializeField] private Shop shop;
    [SerializeField] private Money money;
    private int priceForSeason;
    void Start()
    {
        GameManager.instance.worldTime.CurrentDayChanged += OnCurrentDayChanged;
        GameManager.instance.worldTime.SeasonChanged += OnSeasonChanged;
    }
    private void OnDestroy()
    {
        GameManager.instance.worldTime.CurrentDayChanged -= OnCurrentDayChanged;
        GameManager.instance.worldTime.SeasonChanged -= OnSeasonChanged;
    }

    private void OnCurrentDayChanged(object sender, int dayChange)
    {
        if (seller.itemsToShip != null)
        {
            double totalPrice = 0;
            foreach (var item in seller.itemsToShip)
            {
                totalPrice += shop.Price(item);
            }
            seller.DeleteAll();
            int totalPriceInt = (int)totalPrice;
            priceForSeason += totalPriceInt;
            money.Add(totalPriceInt);
        }
    }

    private void OnSeasonChanged(object sender, Season seasonChange)
    {
        int priceForSeasonInt = (int)(priceForSeason*0.003);
        money.Sub(priceForSeasonInt);
        priceForSeason=0;
    }
}
