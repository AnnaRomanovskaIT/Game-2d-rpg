using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Harvest Definition", fileName = "New Harvest Definition")]
public class HarvestDefinition : ItemDefinition
{
    [SerializeField] private Rarity _rarity;
    [SerializeField] private StateOfHarvest _state;
    [SerializeField] private int _daysAfterHarvest;
    public Rarity rarity { get => _rarity; set => _rarity = value; }
    public StateOfHarvest State { get => _state; set => _state = value; }
    public int DaysAfterHarvest { get => _daysAfterHarvest; set => _daysAfterHarvest = value; }
}

public enum Rarity
{
    Ordinary, Wonderful, Incredible
}
public enum StateOfHarvest
{
    Incredible, Ordinary, Rotten
}